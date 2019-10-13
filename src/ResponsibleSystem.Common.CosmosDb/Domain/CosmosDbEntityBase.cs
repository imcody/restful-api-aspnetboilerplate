using Newtonsoft.Json;
using System;

namespace ResponsibleSystem.Common.CosmosDb.Domain
{
    public abstract class CosmosDbEntityBase : ICosmosDbEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public virtual string Entity => GetType().Name;

        public DateTime CreateDate { get; set; }

        public CosmosDbEntityBase()
        {
            CreateDate = DateTime.UtcNow;
        }
    }
}