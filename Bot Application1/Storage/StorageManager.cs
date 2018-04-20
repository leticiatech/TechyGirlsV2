using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bot_Application1.Storage
{
    public class StorageManager
    {
        public async Task StoreEntity(ITableEntity entity)
        {
            //CloudStorageAccount
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //CloudTableClient
            var tableClient = storageAccount.CreateCloudTableClient();

            //CloudTable
            var table = tableClient.GetTableReference("GroupScore");
            table.CreateIfNotExists();

            //TableOperation
            var insertOperation = TableOperation.Insert(entity);

            await table.ExecuteAsync(insertOperation);

        }

        public List<GroupTableEntity> GetScoresByGroup(string groupName)
        {
            //CloudStorageAccount
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //CloudTableClient
            var tableClient = storageAccount.CreateCloudTableClient();

            //CloudTable
            var table = tableClient.GetTableReference("GroupScore");
            table.CreateIfNotExists();

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<GroupTableEntity> query = new TableQuery<GroupTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, groupName));

            return table.ExecuteQuery(query).ToList();
        }
    }
}
