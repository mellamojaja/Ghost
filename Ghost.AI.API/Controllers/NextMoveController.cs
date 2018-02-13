using Ghost.AI.API.Models;
using System.Web.Http;

namespace Ghost.AI.API.Controllers
{
    public class NextMoveController : ApiController
    {
        // GET: api/NextMove/{state}
        public string Get(string state)
        {
            // Do some logic 
            var result = state + "1";
            return result;
        }
    }
}
