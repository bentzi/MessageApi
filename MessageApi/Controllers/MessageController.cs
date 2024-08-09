using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MessageApi.Models;
using MessageApi.Services;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

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
        public IActionResult AddMessage([FromBody] MessageDto messageDto, [FromHeader(Name = "UserId")] string userId, [FromHeader(Name = "Signature")] string clientSignature)
        {
            // Secret key based on the user ID
            string secretKey = GetServerSecretKey(userId);
            if (string.IsNullOrEmpty(secretKey))
            {
                _logger.LogWarning("Invalid UserId: {UserId}", userId);
                return Unauthorized();
            }

            // Recreate the signature on the server
            string data = $"{messageDto.Text}|{userId}|{messageDto.Timestamp}";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                string computedSignature = BitConverter.ToString(computedHash).Replace("-", "").ToLower();

                // Compare the server-generated signature with the client-provided signature
                if (computedSignature != clientSignature)
                {
                    _logger.LogWarning("Invalid signature for UserId: {UserId}", userId);
                    return Unauthorized();
                }
            }

            // Additional check for replay attacks: Verify timestamp freshness
            if (messageDto.Timestamp < DateTimeOffset.UtcNow.AddMinutes(-5).ToUnixTimeMilliseconds())
            {
                _logger.LogWarning("Stale message for UserId: {UserId}", userId);
                return Unauthorized();
            }

            // Proceed to add the message if all checks pass
            var message = new Message
            {
                UserId = userId,
                Text = messageDto.Text,
                Date = DateTimeOffset.FromUnixTimeMilliseconds(messageDto.Timestamp).UtcDateTime
            };

            _dataService.AddMessage(message);
            return Ok(message);
        }

        private string GetServerSecretKey(string userId)
        {
            // In a real application, retrieve this securely, e.g., from a database or secure storage
            if (userId == "1234") return "1234SecretKey";
            if (userId == "5678") return "5678SecretKey";
            return null;
        }
    }
}
