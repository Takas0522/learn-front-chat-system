using System.Threading.Tasks;

namespace FrontChatSystem.Api.Domains
{
    public interface IMessagesService
    {
        Task<string> GetChanelMessages(string chanelId);
        Task<string> PostMessages(string chanelId, string message);
        Task ReplyMessages(string chanelId, string messageId, string reply);
    }
}