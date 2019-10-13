using Newtonsoft.Json;

namespace ResponsibleSystem.Common.CosmosDb.Domain
{
    public abstract class BaseDocument
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}