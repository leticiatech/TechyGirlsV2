using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bot_Application1.Storage
{
    [Serializable]
    public class UserTableEntity : TableEntity
    {
        public UserTableEntity(string credentials, string userMail)
        {
            this.PartitionKey = credentials;
            this.RowKey = userMail;
        }

        public UserTableEntity() { }

        public string Password { get; set; }
    }
}