using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResponsibleSystem.Common.Config;

namespace ResponsibleSystem.Common.Azure.Storage.MessageQueues
{
    public class MessageQueueService : IMessageQueueService
    {
        protected CloudQueue Queue;

        public MessageQueueService(IConfigFactory<MessageQueueServiceConfig> configFactory)
        {
            var config = configFactory.GetConfig();
            ChangeQueue(config).Wait();
        }

        public async Task ChangeQueue(MessageQueueServiceConfig config)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config.AzureWebJobsStorage);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            Queue = queueClient.GetQueueReference(config.MessageQueueName);

            await Queue.CreateIfNotExistsAsync();
        }

        public async Task EnqueueMessage(object message)
        {
            await Queue.AddMessageAsync(
                new CloudQueueMessage(JsonConvert.SerializeObject(message)));
        }

        public async Task<CloudQueueMessage> GetMessage()
        {
            return await Queue.GetMessageAsync();
        }

        public async Task<IEnumerable<CloudQueueMessage>> GetMessages(int messageCount)
        {
            return await Queue.GetMessagesAsync(messageCount);
        }
    }
}
