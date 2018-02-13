using Ghost.AI.API.Models;
using System.Web.Http;

namespace Ghost.AI.API.Controllers
{
    public class NextMoveController : ApiController
    {
        // GET: api/NextMove/foobar
        public GameStateModel Get(GameStateModel state)
        {
            // Do some logic 
            var result = new GameStateModel();
            return result;
        }
    }
}
