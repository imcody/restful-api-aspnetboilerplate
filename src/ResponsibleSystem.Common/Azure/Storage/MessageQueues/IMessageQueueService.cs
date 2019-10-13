using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResponsibleSystem.Common.Azure.Storage.MessageQueues
{
    public interface IMessageQueueService
    {
        Task ChangeQueue(MessageQueueServiceConfig config);
        Task EnqueueMessage(object message);
        Task<CloudQueueMessage> GetMessage();
        Task<IEnumerable<CloudQueueMessage>> GetMessages(int messageCount);
    }
}
