using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontChatSystem.Api.Data;
using FrontChatSystem.Api.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrontChatSystem.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMessagesService _service;
        private readonly IGraphData _data;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGraphData data, IMessagesService service)
        {
            _logger = logger;
            _data = data;
            _service = service;

        }

        [HttpGet]
        public async Task<string> Get()
        {
            string chanelId = await _service.GenerateChannels();
            string chanelData = await _service.GetChanelMessages(chanelId);
            return await _data.GetJoinedTeams();

        }
    }
}
