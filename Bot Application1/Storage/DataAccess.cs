﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Application1.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace Bot_Application1.Storage
{
    [Serializable]
    public class DataAccess: IDataAccess
    {
        private readonly IStorageManager _storageManager;

        public DataAccess(IStorageManager storageManager)
        {
            _storageManager = storageManager;
        }

        public async Task StoreEntity(ITableEntity entity, string tableName)
        {
            await _storageManager.StoreEntity(entity, tableName);
        }

        public IEnumerable<Group> GetGroupsWithScores()
        {
            var result = new List<Group>();
            var groups = _storageManager.GetAllGroupsNames();

            foreach (var g in groups)
            {
                var tableEntity = _storageManager.GetScoresByGroup(g);
                var group = new Group {
                    Name = g ,
                    QuestionScores = tableEntity.Select(t => 
                    new QuestionScore
                    {
                        Question = t.RowKey,
                        Score = t.Score
                    }),
                    TotalScore = tableEntity.Sum(t => t.Score)
                };


                result.Add(group);
            }
            return result;
        }
    }
}