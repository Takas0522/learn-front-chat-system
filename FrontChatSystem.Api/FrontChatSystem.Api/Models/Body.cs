using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Models
{
    public class Body
    {
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }
        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
