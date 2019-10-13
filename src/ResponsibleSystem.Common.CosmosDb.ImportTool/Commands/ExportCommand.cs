using ResponsibleSystem.Common.CosmosDb.ImportTool.Helpers;
using ResponsibleSystem.Common.CosmosDb.ImportTool.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ResponsibleSystem.Common.CosmosDb.ImportTool.Commands
{
    public class ExportCommand : IDocumentDbCommand
    {
        public string CommandDescription => "Export data from file to database";

        private const string DEFAULT_COLLECTION_ID = "ResponsibleSystemCosmosDb";
        private const string DEFAULT_FILE_NAME = "backup.json";

        private readonly Prompter prompter;

        public ExportCommand()
        {
            prompter = Prompter.Default;
        }

        public void Execute(DocumentClient client, CosmosDbCredential credentials)
        {
            if (credentials.IsReadonly)
            {
                throw new Exception("You cannot export data to readonly database");
            }

            string inputFile = AskForInputFile();
            string collectionName = AskForCollectionName();
            CreateCollectionIfNotExist(client, credentials.DatabaseId, collectionName);
            JArray data = DeserializeFile(inputFile);
            InsertOrUpdateToDatabase(client, credentials.DatabaseId, collectionName, data);
        }

        private string AskForCollectionName()
        {
            prompter.Info($"Enter destination collection name [{DEFAULT_COLLECTION_ID}]:");
            string collectionName = prompter.ReadLine();
            return string.IsNullOrWhiteSpace(collectionName) ? DEFAULT_COLLECTION_ID : collectionName;
        }

        private string AskForInputFile()
        {
            do
            {
                prompter.Info($"Input file name [{DEFAULT_FILE_NAME}]: ");
                string fileName = prompter.ReadLine();

                fileName = string.IsNullOrWhiteSpace(fileName) ? DEFAULT_FILE_NAME : fileName.Contains("/") ? fileName : $"_backups/{fileName}";

                if (File.Exists(fileName))
                {
                    return fileName;
                }
                else
                {
                    prompter.Error("File does not exist.");
                }

            } while (true);
        }

        private JArray DeserializeFile(string inputFile)
        {
            string json = File.ReadAllText(inputFile);
            return (JArray)JsonConvert.DeserializeObject(json);
        }

        private void InsertOrUpdateToDatabase(DocumentClient client, string databaseId, string collectionId, JArray data)
        {
            prompter.WriteLine("Export start. This may take a while.");
            foreach (var entity in data)
            {
                client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), entity).Wait();
            }
            prompter.WriteLine("Export finished.");
        }

        private void CreateCollectionIfNotExist(DocumentClient client, string databaseId, string collectionName)
        {
            var documentCollection = new DocumentCollection()
            {
                Id = collectionName
            };
            client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseId), documentCollection).Wait();
        }
    }
}
