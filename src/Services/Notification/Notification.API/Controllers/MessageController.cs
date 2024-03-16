using Microsoft.AspNetCore.Mvc;
using Notification.API.DTOs;
using Notification.API.Entities;
using Notification.API.Repositories;
using System.Net;

namespace Notification.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Message>>> Get()
        { 
            var messages = await _messageRepository.GetAll();
            return Ok(messages);
        }

        [HttpGet("GetMessagesByUser/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByUser(string userId)
        {
            var messages = await _messageRepository.GetMessagesByUser(userId);
            if(messages == null)
            {
                return NotFound();
            }
            return Ok(messages);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Create([FromBody] CreateMessage message)
        {
            var mess = new Message
            {
                IsDelete = false,
                BusinessName = message.BusinessName,
                CreatedDate = DateTime.Now,
                FullName = message.FullName,
                IsSeen = false,
                Title = message.Title,
                Type = message.Type,
                UserId = message.UserId
            };
            await _messageRepository.CreateMessage(mess);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Message message)
        {
            return Ok(await _messageRepository.UpdateMessage(message));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteMessage")]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMessageById(string id)
        {
            return Ok(await _messageRepository.DeteleMessage(id));
        }
    }
}
