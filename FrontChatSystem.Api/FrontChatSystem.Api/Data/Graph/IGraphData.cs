using FrontChatSystem.Api.Models;
using Microsoft.Graph;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Data
{
    public interface IGraphData
    {
        Task<string> GenerateChannel();
        Task<Channel> GetChannel();
        Task<Models.Message> GetChannelMessages(string messageId);
        Task<string> GetJoinedTeams();
        Task<ReplyMessage> GetReplyMessages(string messageId);
        Task<PostMessageReturn> PostChannelMessages(PostChannelMessage message);
        Task ReplyChannelMessages(string messageId, PostChannelMessage message);
        Task<SubscriptionResponse> SetSubscription();
    }
}