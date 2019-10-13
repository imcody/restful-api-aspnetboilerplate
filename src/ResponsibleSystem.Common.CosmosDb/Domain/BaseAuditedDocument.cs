using Newtonsoft.Json;
using System;

namespace ResponsibleSystem.Common.CosmosDb.Domain
{
    public abstract class BaseAuditedDocument : BaseDocument
    {
        [JsonProperty(PropertyName = "createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty(PropertyName = "creatorUserId")]
        public long? CreatorUserId { get; set; }
    }
}