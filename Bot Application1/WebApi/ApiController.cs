using System.Collections.Generic;
using System.Web.Http;
using Bot_Application1.Models;
using Bot_Application1.Storage;

namespace Bot_Application1.WebApi
{
    [RoutePrefix("api")]
    public class ApiController : System.Web.Http.ApiController
    {
        private readonly IDataAccess _dataAccess;

        public ApiController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [Route("GetGroups")]
        [HttpGet]
        public IEnumerable<Group> GetAllGroups()
        {
            return _dataAccess.GetGroupsWithScores();
        }

        [Route("AuthenticationAdmin")]
        [HttpPost]
        public bool Processlogin(string mail, string password)
        {
            return _dataAccess.login(mail, password);
        }
    }
}