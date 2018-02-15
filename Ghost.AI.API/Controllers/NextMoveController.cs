using Ghost.AI.API.Models;
using System.Web.Http;

namespace Ghost.AI.API.Controllers
{
    /// <summary>
    /// The suggested next move
    /// </summary>
    public class NextMoveController : ApiController
    {
        // POST: api/nextmove/{state}
        public GameStateModel Post(GameStateModel state)
        {
            if (state == null)
                return null;

            var newState = Analyzer.Instance.NextMove(state);

            return newState;
        }
    }
}
