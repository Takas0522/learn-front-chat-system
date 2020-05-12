using System.Threading.Tasks;

namespace FrontChatSystem.Api.Data
{
    public interface IGraphData
    {
        Task<string> GenerateChannel(string channelId);
        Task<string> GetChannelMessages(string chanelId);
        Task<string> GetJoinedTeams();
    }
}