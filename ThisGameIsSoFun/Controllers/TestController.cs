using Microsoft.AspNetCore.Mvc;
using ThisGameIsSoFun.Services;

namespace ThisGameIsSoFun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMongoTestService _testService;
        public TestController(IMongoTestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        [Route("Test1")]
        public async Task<IActionResult> Test111()
        {
            return Ok("Get done");
        }

        [HttpGet]
        [Route("GetAllStudentsMongo")]
        public async Task<IActionResult> GetAllStudentsMongo1()
        {
            var students = await _testService.GetAllStudents();
            return Ok(students);
        }

        [HttpPut]
        [Route("InsertStudentsMongo")]
        public async Task<IActionResult> InsertStudentsMongo1()
        {
            await _testService.InsertStudents();
            return Ok("Get done");
        }
    }
}
