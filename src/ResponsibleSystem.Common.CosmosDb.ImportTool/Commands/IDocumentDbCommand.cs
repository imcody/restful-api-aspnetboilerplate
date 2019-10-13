using ResponsibleSystem.Common.CosmosDb.ImportTool.Models;
using Microsoft.Azure.Documents.Client;

namespace ResponsibleSystem.Common.CosmosDb.ImportTool.Commands
{
    interface IDocumentDbCommand
    {
        string CommandDescription { get; }
        void Execute(DocumentClient client, CosmosDbCredential credentials);
    }
}
