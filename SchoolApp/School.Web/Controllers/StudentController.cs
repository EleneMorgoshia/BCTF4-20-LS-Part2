using Microsoft.AspNetCore.Mvc;
using SchoolApplication.Services.Interfaces;
using SchoolDomain.Models.DTOs;
using SchoolDomain.Models.Entities;

namespace School.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        //IActionResult აბრუნებს http responseს 
        [HttpGet] //read
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var students = await _studentService.GetAllAsync(cancellationToken);
            return Ok(students); // http status codes and Icollection of students will be returned
        }

        [HttpGet("{id:int}")] //read
        public async Task<IActionResult> GetStudent(int id, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(id, cancellationToken);
            return Ok(student);
        }

        [HttpPost] //create
        //აქ createstudentDto-ს რატომ არ ვატანეთ?
        public async Task<ActionResult<StudentDTO>> AddStudent(CeateStudentDTO createStudentDTO, CancellationToken cancellationToken)
        {
            var student = await _studentService.AddStudentAsync(createStudentDTO, cancellationToken);
            //აქ რა ხდება ვაფშე???
            return CreatedAtAction(//ანუ ეს აწყობს მისამართს, დამატების შემთხვევაში როგორ უნდა მიწვდეს ამ ობიექტს?
                nameof(GetStudent), // ამ მისამართზე შემიძლია ნახვა უკვე შექმნილი სტუდენტის
                new {id = student.Id},
                student
                );//201
        }

        [HttpPut("id")] //update
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDTO updateStudentDTO, CancellationToken cancellationToken)
        {
            await _studentService.UpdateStudentAsync(id, updateStudentDTO, cancellationToken);
            return NoContent(); //204
        }

        [HttpDelete("{id:int}")] //delete
        public async Task<IActionResult> DeleteStudent(int id, CancellationToken cancellationToken)
        {
            await _studentService.DeleteStudentAsnc(id, cancellationToken);
            return NoContent(); //204

        }
    }
}


//აქ ვალიდაციებს არ ვაკეთბ საერთოდ??