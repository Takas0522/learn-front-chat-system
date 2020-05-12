using FrontChatSystem.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Domains
{
    public class MessagesService : IMessagesService
    {
        private readonly IGraphData _data;
        public MessagesService(IGraphData data)
        {
            _data = data;
        }

        public async Task<string> GenerateChannels()
        {
            Guid guidValue = Guid.NewGuid();
            string guidSt = guidValue.ToString();
            string chanelId = await _data.GenerateChannel(guidSt);

            return chanelId;
        }

        public async Task<string> GetChanelMessages(string chanelId)
        {
            return await _data.GetChannelMessages(chanelId);
        }
    }
}
