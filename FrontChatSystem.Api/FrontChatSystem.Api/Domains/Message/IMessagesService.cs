using FrontChatSystem.Api.Models;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Domains
{
    public interface IMessagesService
    {
        Task<MessageWithReply> GetChanelMessages(string chanelId, string messageId);
        Task<string> PostMessages(string chanelId, string message);
        Task ReplyMessages(string chanelId, string messageId, string reply);
    }
}