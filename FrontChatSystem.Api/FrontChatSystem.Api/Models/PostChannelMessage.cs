using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace FrontChatSystem.Api.Models
{
    public class PostChannelMessage
    {
        [DataMember(Name = "body")]
        public ChannelMessageBody Body { get; set; }

        public void EncodeContentString()
        {
            Body.Content = HttpUtility.JavaScriptStringEncode(Body.Content);
        }
    }

    public class ChannelMessageBody
    {
        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
