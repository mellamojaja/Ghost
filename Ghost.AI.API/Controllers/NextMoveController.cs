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
            // Do some logic 
            var result = new GameStateModel
            {
                Word = state.Word + "1"
            };    
            return result;
        }
    }
}
