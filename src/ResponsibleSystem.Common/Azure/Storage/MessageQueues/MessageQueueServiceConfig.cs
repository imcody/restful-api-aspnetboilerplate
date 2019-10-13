using ResponsibleSystem.Common.Config;

namespace ResponsibleSystem.Common.Azure.Storage.MessageQueues
{
    public class MessageQueueServiceConfig : IConfig
    {
        public string AzureWebJobsStorage { get; set; }
        public string MessageQueueName { get; set; }
    }
}
