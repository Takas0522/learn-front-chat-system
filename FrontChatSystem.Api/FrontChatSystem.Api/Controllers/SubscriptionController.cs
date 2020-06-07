using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontChatSystem.Api.Domains;
using FrontChatSystem.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontChatSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMessagesService _service;
        public SubscriptionController
        (
            IMessagesService service
        )
        {
            _service = service;
        }

        [HttpPost]
        public async Task<SubscriptionResponse> SetSubscription()
        {
            SubscriptionResponse res = await _service.SetSubscription();
            return res;
        }
    }
}