using Newtonsoft.Json;
using System;

namespace ResponsibleSystem.Common.CosmosDb.Domain
{
    public abstract class AuditedDocument : BaseAuditedDocument
    {
        [JsonProperty(PropertyName = "lastModificationTime")]
        public DateTime? LastModificationTime { get; set; }

        [JsonProperty(PropertyName = "lastModifierUserId")]
        public long? LastModifierUserId { get; set; }
    }
}