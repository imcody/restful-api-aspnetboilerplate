using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ResponsibleSystem.Common.CosmosDb.ImportTool.Models
{
    public class Settings
    {
        [JsonProperty("CosmoDb")]
        public IEnumerable<CosmosDbCredential> CosmosDbCredentials { get; set; }

        internal static Settings CreateFromFile(string jsonFilePath)
        {
            string json = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<Settings>(json);
        }
    }
}
