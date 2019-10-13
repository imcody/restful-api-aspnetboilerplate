using Newtonsoft.Json;
using System;

namespace ResponsibleSystem.Common.CosmosDb.Domain
{
    public abstract class FullAuditedDocument : AuditedDocument
    {
        [JsonProperty(PropertyName = "isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty(PropertyName = "deleterUserId")]
        public long? DeleterUserId { get; set; }

        [JsonProperty(PropertyName = "deletionTime")]
        public DateTime? DeletionTime { get; set; }
    }
}