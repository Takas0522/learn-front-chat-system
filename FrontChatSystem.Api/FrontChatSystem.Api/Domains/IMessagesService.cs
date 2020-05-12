using System.Threading.Tasks;

namespace FrontChatSystem.Api.Domains
{
    public interface IMessagesService
    {
        Task<string> GenerateChannels();
        Task<string> GetChanelMessages(string chanelId);
    }
}