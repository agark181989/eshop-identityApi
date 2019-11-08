using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Helper
{
    public class StorageAccountHelper
    {
        private string storageConectionString;
        private CloudStorageAccount storageAccount;
        private CloudQueueClient queueClient;

        public string StorageConnectionString
        {
            get { return storageConectionString; }
            set
            {
                this.storageConectionString = value;
                storageAccount = CloudStorageAccount.Parse(this.storageConectionString);

            }
        }

        public async Task SendMessageAsync(string messageText, string queueName)
        {
            queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();


            CloudQueueMessage queueMessage = new CloudQueueMessage(messageText);
            await queue.AddMessageAsync(queueMessage);
        }
    }
}
