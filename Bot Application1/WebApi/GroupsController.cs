using System.Collections.Generic;
using System.Web.Http;
using Bot_Application1.Models;
using Bot_Application1.Storage;

namespace Bot_Application1.WebApi
{
    [RoutePrefix("api/scores")]
    public class GroupsController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public GroupsController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [Route("GetGroups")]
        [HttpGet]
        public IEnumerable<Group> GetAllGroups()
        {
            return _dataAccess.GetGroupsWithScores();
        }
    }
}