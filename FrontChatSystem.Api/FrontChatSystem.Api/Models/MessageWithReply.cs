using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Models
{
    public class MessageWithReply
    {
        public HostMessage Message { get; set; }
        public IEnumerable<ReplyMessageData> ReplyMessage { get; set; }
    }

    public class HostMessage
    {
        public DateTime CreatedDateTime { get; set; }
        public string HostUserId { get; set; }
    }

    public class ReplyMessageData
    {
        public DateTime CreatedDateTime { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}
