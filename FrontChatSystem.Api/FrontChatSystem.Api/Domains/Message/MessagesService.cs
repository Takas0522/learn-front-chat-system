using FrontChatSystem.Api.Data;
using FrontChatSystem.Api.Models;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Domains
{
    public class MessagesService : IMessagesService
    {
        private readonly IGraphData _data;
        public MessagesService(IGraphData data)
        {
            _data = data;
        }

        public async Task<MessageWithReply> GetChanelMessages(string chanelId, string messageId)
        {
             Models.Message data = await _data.GetChannelMessages(chanelId, messageId);
            ReplyMessage replyData = await _data.GetReplyMessages(chanelId, messageId);
            
            var hostData = new HostMessage { CreatedDateTime = data.CreatedDateTime, HostUserId = data.From.User.Id };
            IEnumerable<ReplyMessageData> reply = replyData.Value.Select(s => {
                return new ReplyMessageData {
                    CreatedDateTime = s.CreatedDateTime,
                    Content = s.Body.Content,
                    UserId = s.From.User.Id
                };
            });

            return new MessageWithReply { Message = hostData, ReplyMessage = reply };
        }

        public async Task<string> PostMessages(string chanelId, string message)
        {
            var messagebody = new ChannelMessageBody
            {
                Content = message
            };
            var postmessage = new PostChannelMessage
            {
                Body = messagebody
            };
            postmessage.EncodeContentString();
            PostMessageReturn retContent = await _data.PostChannelMessages(chanelId, postmessage);
            return retContent.Id;
        }

        public async Task ReplyMessages(string chanelId, string messageId, string reply)
        {
            var messagebody = new ChannelMessageBody
            {
                Content = reply
            };
            var postmessage = new PostChannelMessage
            {
                Body = messagebody
            };
            postmessage.EncodeContentString();
            await _data.ReplyChannelMessages(chanelId, messageId, postmessage);
        }
    }
}
