using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MessageApi.Models;
using MessageApi.Services;
using Microsoft.Extensions.Logging;

namespace MessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly DataService _dataService;
        private readonly ILogger<MessageController> _logger;

        public MessageController(DataService dataService, ILogger<MessageController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            return Ok(_dataService.GetMessages());
        }

        [HttpPost]
        public IActionResult AddMessage([FromBody] MessageDto messageDto, [FromHeader(Name = "UserId")] string userId)
        {
            _logger.LogInformation("Received UserId: {UserId}", userId);

            if (string.IsNullOrEmpty(userId) || User.Identity.Name != userId)
            {
                _logger.LogWarning("Unauthorized request: UserId mismatch or missing");
                return Unauthorized();
            }

            if (messageDto == null || string.IsNullOrEmpty(messageDto.Text))
            {
                _logger.LogWarning("Bad request: Message is null or empty");
                return BadRequest("Message text is required");
            }

            var message = new Message
            {
                UserId = userId,
                Text = messageDto.Text,
                Date = messageDto.Date
            };

            _dataService.AddMessage(message);
            return Ok(message);
        }
    }
}
