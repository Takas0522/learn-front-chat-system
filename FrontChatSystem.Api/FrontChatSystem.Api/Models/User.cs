using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Models
{
    public class User
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "userIdentityType")]
        public string UserIdentityType { get; set; }
    }
}
