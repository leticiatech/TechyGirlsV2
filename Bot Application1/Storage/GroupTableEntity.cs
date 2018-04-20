using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bot_Application1.Storage
{
    [Serializable]
    public class GroupTableEntity : TableEntity
    {
        public GroupTableEntity(string groupName, string questionId)
        {
            this.PartitionKey = groupName;
            this.RowKey = questionId;
        }

        public GroupTableEntity() { }

        public int Score { get; set; }
    }
}