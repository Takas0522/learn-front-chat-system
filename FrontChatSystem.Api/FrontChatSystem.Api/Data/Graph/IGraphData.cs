using FrontChatSystem.Api.Models;
using Microsoft.Graph;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Data
{
    public interface IGraphData
    {
        Task<string> GenerateChannel(string channelId);
        Task<Channel> GetChannel(string channelId);
        Task<string> GetChannelMessages(string chanelId);
        Task<string> GetJoinedTeams();
        Task<PostMessageReturn> PostChannelMessages(string chanelId, PostChannelMessage message);
        Task ReplyChannelMessages(string chanelId, string messageId, PostChannelMessage message);
    }
}