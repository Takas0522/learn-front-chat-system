﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FrontChatSystem.Api.Models
{
    public class PostMessageReturn
    {
        [DataMember(Name = "@odata.context")]
        public string Odatacontext { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "replyToId")]
        public object ReplyToId { get; set; }
        [DataMember(Name = "etag")]
        public string Etag { get; set; }
        [DataMember(Name = "messageType")]
        public string MessageType { get; set; }
        [DataMember(Name = "createdDateTime")]
        public DateTime CreatedDateTime { get; set; }
        [DataMember(Name = "lastModifiedDateTime")]
        public object LastModifiedDateTime { get; set; }
        [DataMember(Name = "deleted")]
        public bool Deleted { get; set; }
        [DataMember(Name = "subject")]
        public object Subject { get; set; }
        [DataMember(Name = "summary")]
        public object Summary { get; set; }
        [DataMember(Name = "importance")]
        public string Importance { get; set; }
        [DataMember(Name = "locale")]
        public string Locale { get; set; }
        [DataMember(Name = "policyViolation")]
        public object PolicyViolation { get; set; }
        [DataMember(Name = "from")]
        public From From { get; set; }
        [DataMember(Name = "body")]
        public Body Body { get; set; }
        [DataMember(Name = "attachments")]
        public object[] Attachments { get; set; }
        [DataMember(Name = "mentions")]
        public object[] Mentions { get; set; }
        [DataMember(Name = "reactions")]
        public object[] Reactions { get; set; }
    }

}
