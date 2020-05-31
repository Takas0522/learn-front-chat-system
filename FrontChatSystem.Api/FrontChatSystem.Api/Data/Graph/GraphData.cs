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
using FrontChatSystem.Api.Models;
using Utf8Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Security;

namespace FrontChatSystem.Api.Data
{
    public class GraphData : IGraphData
    {
        private readonly string _teamsId;
        private readonly GraphServiceClient _client;
        private AuthenticationContext _authContext;
        private readonly Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential _credential;
        private readonly HttpClient _httpClient;
        private readonly string _anonymousUserId;
        private readonly string _anonymousUserPassword;
        private readonly string _tenantId;
        private readonly string _applicationId;
        private readonly string _secret;

        public GraphData(IConfiguration config)
        {
            _teamsId = config["teamsId"];

            _applicationId = config["GraphSettings:applicationId"];
            _tenantId = config["GraphSettings:tenantId"];
            _secret = config["GraphSettings:secret"];
            _anonymousUserId = config["AnonymousUser:Id"];
            _anonymousUserPassword = config["AnonymousUser:Password"];

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(_applicationId)
                .WithTenantId(_tenantId)
                .WithClientSecret(_secret)
                .Build();

            ClientCredentialProvider authProvide = new ClientCredentialProvider(confidentialClientApplication);
            _client = new GraphServiceClient(authProvide);

            _credential = new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(_applicationId, _secret);

            _authContext = new AuthenticationContext($"https://login.microsoftonline.com/{_tenantId}/");
            

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

        public async Task<Channel> GetChannel(string channelId)
        {
            try
            {
                var res = await _client.Teams[_teamsId].Channels[channelId].Request().GetAsync();
                return res;
            }
            catch (ServiceException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        public async Task<Models.Message> GetChannelMessages(string chanelId, string messageId)
        {
            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult res = await _authContext.AcquireTokenAsync("https://graph.microsoft.com", _credential);
            string accessToken = res.AccessToken;
            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://graph.microsoft.com/beta/teams/{_teamsId}/channels/{chanelId}/messages/{messageId}"),
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var reqRes = await _httpClient.SendAsync(req);
            string message = await reqRes.Content.ReadAsStringAsync();
            Models.Message retContent = JsonSerializer.Deserialize<Models.Message>(message);
            return retContent;
        }

        public async Task<ReplyMessage> GetReplyMessages(string chanelId, string messageId)
        {
            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult res = await _authContext.AcquireTokenAsync("https://graph.microsoft.com", _credential);
            string accessToken = res.AccessToken;
            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://graph.microsoft.com/beta/teams/{_teamsId}/channels/{chanelId}/messages/{messageId}/replies"),
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var reqRes = await _httpClient.SendAsync(req);
            string replymessage = await reqRes.Content.ReadAsStringAsync();
            ReplyMessage retContent = JsonSerializer.Deserialize<ReplyMessage>(replymessage);
            return retContent;
        }

        public async Task<PostMessageReturn> PostChannelMessages(string chanelId, PostChannelMessage message)
        {
            string accessToken = await AquireTokenUserContext();
            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://graph.microsoft.com/beta/teams/{_teamsId}/channels/{chanelId}/messages/"),
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string json = JsonSerializer.ToJsonString<PostChannelMessage>(message);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            req.Content = content;

            var reqRes = await _httpClient.SendAsync(req);
            if (reqRes.IsSuccessStatusCode)
            {
                string contentString = await reqRes.Content.ReadAsStringAsync();
                PostMessageReturn retContent = JsonSerializer.Deserialize<PostMessageReturn>(contentString);
                return retContent;
            }
            else
            {
                throw new Exception("Failed Post Message");
            }
        }

        public async Task ReplyChannelMessages(string chanelId, string messageId, PostChannelMessage message)
        {
            string accessToken = await AquireTokenUserContext();
            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://graph.microsoft.com/beta/teams/{_teamsId}/channels/{chanelId}/messages/{messageId}/replies"),
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string json = JsonSerializer.ToJsonString<PostChannelMessage>(message);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            req.Content = content;

            var reqRes = await _httpClient.SendAsync(req);
            if (reqRes.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                throw new Exception("Failed Post Message");
            }
        }

        private async Task<string> AquireTokenUserContext()
        {
            string tokenEndpoint = $"https://login.microsoftonline.com/{_tenantId}/oauth2/token";
            var body = $"resource=https://graph.microsoft.com&client_id={_applicationId}&client_secret={_secret}&grant_type=password&username={_anonymousUserId}&password={_anonymousUserPassword}";
            var stringContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

            var result = await _httpClient.PostAsync(tokenEndpoint, stringContent).ContinueWith<string>((response) =>
            {
                return response.Result.Content.ReadAsStringAsync().Result;
            });
            JObject jobject = JObject.Parse(result);
            var token = jobject["access_token"].Value<string>();
            return token;
        }
    }
}
