
using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bot_Application1.Storage
{
    [Serializable]

    public class TiebreakTableEntity : TableEntity
    {
        public TiebreakTableEntity(string groupName)
        {
            this.PartitionKey = "Group";
            this.RowKey = groupName;
        }

        public TiebreakTableEntity() { }

        public int Score { get; set; }
    }
}