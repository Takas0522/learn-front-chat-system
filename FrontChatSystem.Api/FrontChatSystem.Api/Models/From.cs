using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Models
{
    public class From
    {
        [DataMember(Name = "application")]
        public object Application { get; set; }
        [DataMember(Name = "device")]
        public object Device { get; set; }
        [DataMember(Name = "conversation")]
        public object Conversation { get; set; }
        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}
