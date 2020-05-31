using Microsoft.Graph;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Domains
{
    public interface IChannelService
    {
        Task<string> GenerateChannels();
        Task<Channel> GetChannels(string channelId);
    }
}