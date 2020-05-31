using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontChatSystem.Api.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontChatSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessagesService _service;
        public MessageController
        (
            IMessagesService service
        )
        {
            _service = service;
        }

        [HttpGet]
        public async Task<string> GetMessages(string channelId)
        {
            string messageIId = await _service.GetChanelMessages(channelId);
            return messageIId;
        }

        [HttpPost]
        public async Task<string> GenerateMessage([FromForm]string channelId, [FromForm]string message)
        {
            string messageIId = await _service.PostMessages(channelId, message);
            return messageIId;
        }

    }
}