using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Models
{

    public class SubscriptionResponse
    {
        [DataMember(Name = "@odata.context")]
        public string Odatacontext { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "resource")]
        public string Resource { get; set; }
        [DataMember(Name = "applicationId")]
        public string ApplicationId { get; set; }
        [DataMember(Name = "changeType")]
        public string ChangeType { get; set; }
        [DataMember(Name = "clientState")]
        public string ClientState { get; set; }
        [DataMember(Name = "notificationUrl")]
        public string NotificationUrl { get; set; }
        [DataMember(Name = "expirationDateTime")]
        public DateTime ExpirationDateTime { get; set; }
        [DataMember(Name = "creatorId")]
        public string CreatorId { get; set; }
        [DataMember(Name = "latestSupportedTlsVersion")]
        public string LatestSupportedTlsVersion { get; set; }
    }
}
