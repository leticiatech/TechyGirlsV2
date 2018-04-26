using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bot_Application1.Storage
{
    public interface IStorageManager
    {
        Task StoreEntity(ITableEntity entity, string tableName);
        List<string> GetAllGroupsNames();
        List<GroupTableEntity> GetScoresByGroup(string groupName);
        List<TiebreakTableEntity> GetTieBreakGroups();
        UserTableEntity GetCredentials();
    }
}
