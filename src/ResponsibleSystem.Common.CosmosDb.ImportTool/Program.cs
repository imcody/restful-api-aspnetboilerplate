using ResponsibleSystem.Common.CosmosDb.ImportTool.Commands;
using ResponsibleSystem.Common.CosmosDb.ImportTool.Helpers;
using ResponsibleSystem.Common.CosmosDb.ImportTool.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ResponsibleSystem.Common.CosmosDb.ImportTool
{
    class Program
    {
        static IDocumentDbCommand[] commands = new IDocumentDbCommand[]
        {
            new ImportCommand(),
            new ExportCommand()
        };

        static Prompter prompter = Prompter.Default;
        static Settings settings;
        static CosmosDbCredential credential;
        static DocumentClient client;

        static void Main(string[] args)
        {
            try
            {
                settings = Settings.CreateFromFile("appsettings.json");
                ConnectToDatabase();
                AskForAndExecuteCommands();
            }
            catch (Exception ex)
            {
                prompter.WriteLine();
                prompter.Error(ex.Message);
                Exception inner = ex.InnerException;
                while (inner != null)
                {
                    prompter.WriteLine();
                    prompter.Error(" --- Inner exception:");
                    prompter.Error(ex.InnerException.Message);
                    inner = inner.InnerException;
                }
            }
            PromptExit();
        }

        private static void ConnectToDatabase()
        {
            client?.Dispose();
            client = null;

            credential = AskForCredentials(settings);
            client = new DocumentClient(new Uri(credential.Endpoint), credential.MasterKey, new ConnectionPolicy
            {
                EnableEndpointDiscovery = false
            });
            prompter.WriteLine("Opening conection to database...");
            client.OpenAsync().Wait();
            prompter.WriteLine("Connection opened.");
            CheckDatabase(client, credential.DatabaseId);
        }

        private static void AskForAndExecuteCommands()
        {
            int numberOfNonCommandOptions = 2;
            do
            {
                prompter.Info("What you want to do ?");
                prompter.WriteLine("     0: Exit");
                prompter.WriteLine("     1: Connect to other database");
                for (int i = 0; i < commands.Length; i++)
                {
                    int optionNumber = i + numberOfNonCommandOptions;
                    prompter.WriteLine(string.Format("{0,6}: {1}", optionNumber, commands[i].CommandDescription));
                }
                int choosed = prompter.ReadInt("No of command: ", 0, commands.Length - 1 + numberOfNonCommandOptions);

                if (choosed == 0)
                {
                    break;
                }
                else if (choosed == 1)
                {
                    ConnectToDatabase();
                }
                else
                {
                    int optionNumber = choosed - numberOfNonCommandOptions;
                    commands[optionNumber].Execute(client, credential);
                }
            } while (true);
        }

        private static void CheckDatabase(DocumentClient client, string databaseId)
        {
            try
            {
                Task<ResourceResponse<Database>> ReadDatabaseTask = client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));
                ReadDatabaseTask.Wait();
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Database doesn't exists");
                }
                else
                {
                    throw new AggregateException("Error connecting to database: ", ex);
                }
            }

        }

        private static CosmosDbCredential AskForCredentials(Settings settings)
        {
            prompter.Info("Which database you want to connect to:");
            int credentialsCount = settings.CosmosDbCredentials.Count();
            for (int i = 0; i < credentialsCount; i++)
            {
                CosmosDbCredential current = settings.CosmosDbCredentials.ElementAt(i);
                prompter.WriteLine("{0,6}: {1}", i, current.Name);
            }

            int choosed = prompter.ReadInt("No of setting: ", 0, credentialsCount - 1);

            return settings.CosmosDbCredentials.ElementAt(choosed);
        }

        private static void PromptExit()
        {
            client?.Dispose();
            prompter.Close();
            Console.WriteLine("Click any key to end...");
            Console.ReadKey();
        }
    }
}
