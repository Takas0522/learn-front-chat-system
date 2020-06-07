using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Models
{

    public class SubscriptionPost
    {
        [DataMember(Name = "changeType")]
        public string ChangeType { get; set; }
        [DataMember(Name = "notificationUrl")]
        public string NotificationUrl { get; set; }
        [DataMember(Name = "resource")]
        public string Resource { get; set; }
        [DataMember(Name = "expirationDateTime")]
        public DateTime ExpirationDateTime { get; set; }
    }

}
