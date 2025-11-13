using Microsoft.AspNetCore.Mvc;

namespace ThisGameIsSoFun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        [HttpGet]
        [Route("Test1")]
        public async Task<IActionResult> Test111()
        {
            return Ok("Get done");
        }
    }
}
