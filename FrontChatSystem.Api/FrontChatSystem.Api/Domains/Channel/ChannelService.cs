using FrontChatSystem.Api.Data;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Domains
{
    public class ChannelService : IChannelService
    {
        private readonly IGraphData _data;
        public ChannelService(IGraphData data)
        {
            _data = data;
        }
        public async Task<Channel> GetChannels(string channelId)
        {
            Channel chanel = await _data.GetChannel(channelId);
            return chanel;
        }
        public async Task<string> GenerateChannels()
        {
            Guid guidValue = Guid.NewGuid();
            string guidSt = guidValue.ToString();
            string chanelId = await _data.GenerateChannel(guidSt);

            return chanelId;
        }

    }
}
