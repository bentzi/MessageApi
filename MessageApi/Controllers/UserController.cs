using Microsoft.AspNetCore.Mvc;
using MessageApi.Services;

namespace MessageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataService _dataService;

        public UserController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{id}")]
        public IActionResult CheckUser(string id)
        {
            if (_dataService.CheckUser(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
