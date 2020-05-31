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
        public async Task<Channel> GetChannels()
        {
            Channel chanel = await _data.GetChannel();
            return chanel;
        }
        public async Task<string> GenerateChannels()
        {
            string chanelId = await _data.GenerateChannel();
            return chanelId;
        }

    }
}
