using ResponsibleSystem.Common.CosmosDb.ImportTool.Helpers;
using ResponsibleSystem.Common.CosmosDb.ImportTool.Models;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ResponsibleSystem.Common.CosmosDb.ImportTool.Commands
{
    class ImportCommand : IDocumentDbCommand
    {
        public string CommandDescription => "Import data from database to file";

        private const string DEFAULT_COLLECTION_ID = "ResponsibleSystemCosmosDb";
        private string SUFFIX;
        private string DEFAULT_FILE_NAME;
        private Prompter prompter;

        private string[] allCollections = {
            "TreatmentCase",
            "HfeaForm",
            "ClinicForm",
            "Mip",
            "MdcContent",
            "MdcUserProfile"
        };

        public ImportCommand()
        {
            prompter = Prompter.Default;
            SUFFIX = "-" + DateTime.Now.ToString("yyyy_MM_dd-HH_mm") + ".json";
            DEFAULT_FILE_NAME = "_backups/backup" + SUFFIX;
        }

        public void Execute(DocumentClient client, CosmosDbCredential credentials)
        {
            string outputFileName = AskForOutputFileName();

            IEnumerable<string> collectionsToFetch = AskForCollections();

            FetchDataAndSaveToFile(client, credentials.DatabaseId, outputFileName, collectionsToFetch);
        }

        private string AskForOutputFileName()
        {
            do
            {
                prompter.Info($"Output file name [{DEFAULT_FILE_NAME}]: ");
                string fileName = prompter.ReadLine();

                fileName = string.IsNullOrWhiteSpace(fileName) ? DEFAULT_FILE_NAME : fileName;

                if (!fileName.EndsWith(".json"))
                {
                    fileName = fileName + SUFFIX;
                }

                if (File.Exists(fileName))
                {
                    bool shouldOverride = prompter.ReadYN("File exists do you want to override it ?", defaultValue: true);

                    if (shouldOverride)
                    {
                        return fileName;
                    }
                }
                else
                {
                    return fileName;
                }

            } while (true);
        }

        private IEnumerable<string> AskForCollections()
        {
            bool shouldDownloadWholeDatabase = prompter.ReadYN("Do you want to download whole database ? ", defaultValue: true);

            if (shouldDownloadWholeDatabase)
            {
                return new List<string>();
            }

            var choosedCollections = new List<string>();
            List<int> allowedAnswers = Enumerable.Range(0, allCollections.Length).ToList();

            prompter.Info("Which collections you want to import ?");
            do
            {
                prompter.Info("Leave empty to end");

                for (int i = 0; i < allCollections.Length; i++)
                {
                    string currentCollectionName = allCollections[i];

                    if (choosedCollections.Contains(currentCollectionName)) continue;
                    prompter.WriteLine(string.Format("{0,6}: {1}", i, currentCollectionName));
                }

                int? choosedValue = prompter.ReadIntOrEmpty(allowedAnswers.ToArray());
                if (!choosedValue.HasValue)
                {
                    break;
                }

                allowedAnswers.Remove(choosedValue.Value);
                choosedCollections.Add(allCollections[choosedValue.Value]);

            } while (allowedAnswers.Any());

            return choosedCollections;
        }

        private void FetchDataAndSaveToFile(DocumentClient client, string databaseId, string outputFileName, IEnumerable<string> collectionsToFetch)
        {
            string collectionName = AskForCollectionName();
            prompter.Info("Fetching data from database. This may take a while.");
            using (Stream fromDbStream = GetCollectionStream(client, databaseId, collectionName, collectionsToFetch))
            {
                SaveToFile(outputFileName, fromDbStream);
                prompter.Info($"Wrote {fromDbStream.Length} bytes to file ");
            }
        }

        private string AskForCollectionName()
        {
            prompter.Info($"Enter source collection name [{DEFAULT_COLLECTION_ID}]:");
            string collectionName = prompter.ReadLine();
            return string.IsNullOrWhiteSpace(collectionName) ? DEFAULT_COLLECTION_ID : collectionName;
        }

        private Stream GetCollectionStream(DocumentClient client, string databaseId, string collectionName, IEnumerable<string> collectionsToFetch)
        {
            string query = BuildQuery(collectionsToFetch);

            List<dynamic> documents = client.CreateDocumentQuery(
                    UriFactory.CreateDocumentCollectionUri(databaseId, collectionName),
                    query,
                    new FeedOptions { MaxItemCount = -1 })
                .ToList();

            return SerializeAndWriteToStream(documents);
        }

        private string BuildQuery(IEnumerable<string> collectionsToFetch)
        {
            StringBuilder query = new StringBuilder("SELECT * FROM c");

            if (collectionsToFetch != null && collectionsToFetch.Any())
            {
                string delimiter = " WHERE ";

                foreach (var item in collectionsToFetch)
                {
                    query.Append(delimiter).Append("c.Entity = '").Append(item).Append('\'');
                    delimiter = " OR ";
                }
            }

            return query.ToString();
        }

        private static void SaveToFile(string outputFileName, Stream fromDbStream)
        {
            using (Stream outputStream = File.Open(outputFileName, FileMode.Create))
            {
                fromDbStream.CopyTo(outputStream);
            }
        }

        private static Stream SerializeAndWriteToStream(List<dynamic> documents)
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);

            streamWriter.Write('[');
            foreach (var item in documents)
            {
                string json = JsonConvert.SerializeObject(item, Formatting.Indented);
                streamWriter.Write(json);
                streamWriter.Write(',');
            }
            streamWriter.BaseStream.Seek(-1, SeekOrigin.Current);
            streamWriter.Write(']');

            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
