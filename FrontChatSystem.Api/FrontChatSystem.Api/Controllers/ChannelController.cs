using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontChatSystem.Api.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace FrontChatSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private IChannelService _channelService;
        public ChannelController(
            IChannelService channelService
        )
        {
            _channelService = channelService;
        }

        [HttpGet]
        public async Task<ActionResult<Channel>> GetChannel()
        {
            Channel channel = await _channelService.GetChannels();
            if (channel != null)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> GenerateChannelInfo()
        {
            string genChannelId = await _channelService.GenerateChannels();
            return genChannelId;
        }
    }
}