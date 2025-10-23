namespace GarageBuddy.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api;
    using Services.Data.Contracts;

    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IAiReceptionistService aiReceptionistService;

        public ChatController(IAiReceptionistService aiReceptionistService)
        {
            this.aiReceptionistService = aiReceptionistService;
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] ChatMessage chatMessage)
        {
            if (chatMessage == null || string.IsNullOrWhiteSpace(chatMessage.Message))
            {
                return BadRequest("Message cannot be empty.");
            }

            var response = await this.aiReceptionistService.GetResponseAsync(chatMessage.Message);

            return Ok(new { response });
        }
    }
}
