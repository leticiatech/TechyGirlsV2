using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using Bot_Application1.Models;
using Bot_Application1.Storage;

namespace Bot_Application1.WebApi
{
    [Authorize]
    [RoutePrefix("api")]
    public class ApiController : System.Web.Http.ApiController
    {
        private readonly IDataAccess _dataAccess;

        public ApiController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [AllowAnonymous]
        [Route("GetGroups")]
        [HttpGet]
        public IEnumerable<Group> GetAllGroups()
        {
            return _dataAccess.GetGroupsWithScores();
        }

        [AllowAnonymous]
        [Route("AuthenticationAdmin")]
        [HttpPost]
        public bool Processlogin([FromBody] User user)
        {
            return _dataAccess.login(user.mail, user.password);
        }
    }
}