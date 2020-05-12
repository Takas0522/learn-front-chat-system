using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FrontChatSystem.Api.Data
{
    public class GraphData : IGraphData
    {
        private readonly string _teamsId;
        private readonly GraphServiceClient _client;
        private AuthenticationContext _authContext;
        private readonly Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential _credential;
        private readonly HttpClient _httpClient;

        public GraphData(IConfiguration config)
        {
            _teamsId = config["teamsId"];

            string applicationId = config["GraphSettings:applicationId"];
            string tenantId = config["GraphSettings:tenantId"];
            string secret = config["GraphSettings:secret"];

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(applicationId)
                .WithTenantId(tenantId)
                .WithClientSecret(secret)
                .Build();
            ClientCredentialProvider authProvide = new ClientCredentialProvider(confidentialClientApplication);
            _client = new GraphServiceClient(authProvide);

            _credential = new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(applicationId, secret);
            _authContext = new AuthenticationContext($"https://login.microsoftonline.com/{tenantId}/");

            _httpClient = new HttpClient();
        }

        public async Task<string> GetJoinedTeams()
        {
            var chatData = await _client.Teams[_teamsId].Channels.Request().GetAsync();
            return "a";
        }

        public async Task<string> GenerateChannel(string channelId)
        {
            var channel = new Channel
            {
                DisplayName = $"問い合わせ({channelId})",
                Description = "テスト作成"
            };
            var res = await _client.Teams[_teamsId].Channels.Request().AddAsync(channel);
            return res.Id;
        }

        public async Task<string> GetChannelMessages(string chanelId)
        {
            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult res = await _authContext.AcquireTokenAsync("https://graph.microsoft.com", _credential);
            string accessToken = res.AccessToken;
            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://graph.microsoft.com/beta/teams/{_teamsId}/channels/{chanelId}/messages/")
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var reqRes = await _httpClient.SendAsync(req);
            return await reqRes.Content.ReadAsStringAsync();
        }

    }
}
