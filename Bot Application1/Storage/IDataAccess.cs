using System.Collections.Generic;
using Bot_Application1.Models;

namespace Bot_Application1.Storage
{
    public interface IDataAccess
    {
        IEnumerable<Group> GetGroupsWithScores();
    }
}
