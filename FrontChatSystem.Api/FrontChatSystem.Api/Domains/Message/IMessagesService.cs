using FrontChatSystem.Api.Models;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Domains
{
    public interface IMessagesService
    {
        Task<MessageWithReply> GetChanelMessages(string messageId);
        Task<string> PostMessages(string message);
        Task ReplyMessages(string messageId, string reply);
    }
}