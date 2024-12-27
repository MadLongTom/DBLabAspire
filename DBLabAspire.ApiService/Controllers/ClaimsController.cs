using DBLabAspire.ApiService.Models;
using DBLabAspire.ApiService.Models.View;
using DBLabAspire.ApiService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBLabAspire.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Claims
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Claims>>> GetClaims()
        {
            return await _context.Claims.ToListAsync();
        }

        // GET: api/Claims/5
        [HttpGet]
        public async Task<ActionResult<Claims>> GetClaims(string studentId, string courseId)
        {
            var claims = await _context.Claims.FindAsync(studentId, courseId);

            if (claims == null)
            {
                return NotFound();
            }

            return claims;
        }

        // PUT: api/Claims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutClaims(string studentId, string courseId, Claims claims)
        {
            if (studentId != claims.StudentId.Trim() && courseId != claims.CourseId.Trim())
            {
                return BadRequest();
            }

            _context.Entry(claims).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimsExists(claims.StudentId, claims.CourseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Claims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Claims>> PostClaims(Claims claims)
        {
            _context.Claims.Add(claims);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClaimsExists(claims.StudentId, claims.CourseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClaims", new { id = claims.StudentId }, claims);
        }

        // DELETE: api/Claims/5
        [HttpDelete]
        public async Task<IActionResult> DeleteClaims(string studentId, string courseId)
        {
            var claims = await _context.Claims.FindAsync(studentId, courseId);
            if (claims == null)
            {
                return NotFound();
            }

            _context.Claims.Remove(claims);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetAverageGradeCS")]
        public async ValueTask<ActionResult<double>> GetAverageGradeCS()
        {
            return _context.Claims.Where(c => c.Student.Major == "CS").Average(c => c.Grade) ?? 0;
        }

        [HttpGet("GetMultiClaimsStudentCount")]
        public async ValueTask<ActionResult<int>> GetMultiClaimsStudentCount()
        {
            return _context.Claims.GroupBy(c => c.StudentId).Count(g => g.Count() > 1);
        }

        [HttpGet("GetMultiClaimsStudents")]
        public async ValueTask<ActionResult<IEnumerable<Student?>>> GetMultiClaimsStudents()
        {
            return await _context.Claims.GroupBy(c => c.Student).Where(g => g.Count() > 1).Select(g => g.Key).ToListAsync();
        }

        [HttpGet("GetNoneClaimedCourse")]
        public async ValueTask<ActionResult<IEnumerable<Course>>> GetNoneClaimedCourse()
        {
            return await _context.Courses.Where(c => !_context.Claims.Any(cl => cl.CourseId == c.Id)).ToListAsync();
        }

        [HttpGet("GetStudentsWhichNotClaimedCourse1")]
        public async ValueTask<ActionResult<IEnumerable<Student>>> GetStudentsWhichNotClaimedCourse1()
        {
            return await _context.Students.Where(s => !_context.Claims.Any(cl => cl.CourseId != "1")).ToListAsync();
        }

        [HttpGet("GetStudentCountWhichNotClaimedCourse1")]
        public async ValueTask<ActionResult<int>> GetStudentCountWhichNotClaimedCourse1()
        {
            return _context.Students.Count(s => !_context.Claims.Any(cl => cl.CourseId != "1"));
        }

        [HttpGet("GetStudentsClaimedCourse1And2")]
        public async ValueTask<ActionResult<IEnumerable<Student>>> GetStudentsClaimedCourse1And2()
        {
            var students = await _context.Students
                .Where(s => _context.Claims.Any(cl => cl.StudentId == s.Id && cl.CourseId == "1") &&
                            _context.Claims.Any(cl => cl.StudentId == s.Id && cl.CourseId == "2"))
                .ToListAsync();

            return students;
        }

        [HttpGet("GetStudentsWithGradesAbove80")]
        public async ValueTask<ActionResult<IEnumerable<Student>>> GetStudentsWithGradesAbove80()
        {
            var students = await _context.Students
                .Where(s => _context.Claims
                    .Where(cl => cl.StudentId == s.Id)
                    .All(cl => cl.Grade >= 80))
                .ToListAsync();

            return students;
        }

        [HttpGet("GetAverageClaimsPerStudent")]
        public async ValueTask<ActionResult<double>> GetAverageClaimsPerStudent()
        {
            var averageClaimsPerStudent = await _context.Students
                .Select(s => _context.Claims.Count(cl => cl.StudentId == s.Id))
                .AverageAsync();

            return averageClaimsPerStudent;
        }

        [HttpGet("GetAverageClaimsPerDepartment")]
        public async ValueTask<ActionResult<IEnumerable<object>>> GetAverageClaimsPerDepartment()
        {
            var averageClaimsPerDepartment = await _context.Students
                .GroupBy(s => s.Major)
                .Select(g => new
                {
                    Department = g.Key,
                    AverageClaims = g.Average(s => _context.Claims.Count(cl => cl.StudentId == s.Id))
                })
                .ToListAsync();

            return averageClaimsPerDepartment;
        }

        [HttpGet("GetCourseStatistics")]
        public async ValueTask<ActionResult<IEnumerable<object>>> GetCourseStatistics()
        {
            var courseStatistics = await _context.Courses
                .Select(c => new
                {
                    CourseId = c.Id,
                    CourseName = c.Name,
                    StudentCount = _context.Claims.Count(cl => cl.CourseId == c.Id),
                    HighestGrade = _context.Claims.Where(cl => cl.CourseId == c.Id).Max(cl => (short?)cl.Grade) ?? 0,
                    AverageGrade = _context.Claims.Where(cl => cl.CourseId == c.Id).Average(cl => (double?)cl.Grade) ?? 0,
                    LowestGrade = _context.Claims.Where(cl => cl.CourseId == c.Id).Min(cl => (short?)cl.Grade) ?? 0
                })
                .ToListAsync();

            return courseStatistics;
        }

        [HttpGet("GetStudentsWithAverageAbove75AndNoFailingGrades")]
        public async ValueTask<ActionResult<IEnumerable<Student>>> GetStudentsWithAverageAbove75AndNoFailingGrades()
        {
            var students = await _context.Students
                .Where(s => _context.Claims
                    .Where(cl => cl.StudentId == s.Id)
                    .Average(cl => (double?)cl.Grade) > 75 &&
                    !_context.Claims
                        .Where(cl => cl.StudentId == s.Id)
                        .Any(cl => cl.Grade < 60))
                .ToListAsync();

            return students;
        }

        [HttpGet("GetHighestAverageGradeCourse")]
        public async Task<ActionResult<HighestAverageGradeCourse>> GetHighestAverageGradeCourse()
        {
            var course = await _context.HighestAverageGradeCourses.FirstOrDefaultAsync();
            return course;
        }

        [HttpGet("GetHighestAverageGradeCourseClaims")]
        public async Task<ActionResult<IEnumerable<HighestAverageGradeCourseClaims>>> GetHighestAverageGradeCourseClaims()
        {
            var claims = await _context.HighestAverageGradeCourseClaims.ToListAsync();
            return claims;
        }

        [HttpGet("GetHighestAverageGradeStudentDepartment")]
        public async Task<ActionResult<HighestAverageGradeStudentDepartment>> GetHighestAverageGradeStudentDepartment()
        {
            var department = await _context.HighestAverageGradeStudentDepartments.FirstOrDefaultAsync();
            return department;
        }

        private bool ClaimsExists(string studentId, string courseId)
        {
            return _context.Claims.Any(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}
