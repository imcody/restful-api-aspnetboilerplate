namespace ResponsibleSystem.Common.CosmosDb.ImportTool.Models
{
    public class CosmosDbCredential
    {
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public string MasterKey { get; set; }
        public string DatabaseId { get; set; }
        public bool IsReadonly { get; set; }
    }
}