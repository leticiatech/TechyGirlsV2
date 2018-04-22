using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bot_Application1.Storage
{
    public class StorageManager: IStorageManager
    {
        public async Task StoreEntity(ITableEntity entity, string tableName)
        {
            //CloudStorageAccount
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //CloudTableClient
            var tableClient = storageAccount.CreateCloudTableClient();

            //CloudTable
            var table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();

            //TableOperation
            var insertOperation = TableOperation.Insert(entity);

            await table.ExecuteAsync(insertOperation);

        }

        public List<string> GetAllGroupsNames()
        {
            //CloudStorageAccount
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //CloudTableClient
            var tableClient = storageAccount.CreateCloudTableClient();

            //CloudTable
            var table = tableClient.GetTableReference("Group");
            table.CreateIfNotExists();

            // Construct the query operation for all entities where PartitionKey="Name".
            var query = new TableQuery<GroupTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Name"));

            return table.ExecuteQuery(query).Select(n => n.RowKey).ToList();

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

            // Construct the query operation for all entities where PartitionKey="groupName".
            var query = new TableQuery<GroupTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, groupName));

            return table.ExecuteQuery(query).ToList();
        }
    }
}
