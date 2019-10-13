using System.Collections.Generic;

namespace ResponsibleSystem.Common.CosmosDb.Services
{
    public class CosmosDbSqlProperty
    {
        public string Alias { get; set; }
        public IList<CosmosDbSqlProperty> InnerProps { get; set; }
    }
}