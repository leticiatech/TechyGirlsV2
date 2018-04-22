using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot_Application1.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bot_Application1.Storage
{
    public interface IDataAccess
    {
        Task StoreEntity(ITableEntity entity, string tableName);
        IEnumerable<Group> GetGroupsWithScores();
    }
}
