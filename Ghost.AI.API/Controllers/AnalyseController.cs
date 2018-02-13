using Ghost.AI.API.Models;
using System.Web.Http;

namespace Ghost.AI.API.Controllers
{    
    public class AnalyseController : ApiController
    {
        // GET: api/analise/{state}
        public GameAnalysisModel Get(GameStateModel state)
        {
            // Do some logic 
            var analysis = new GameAnalysisModel
            {
                Help = state.Word
            };
            return analysis;
        }
        public GameAnalysisModel Post(GameStateModel state)
        {
            // Do some logic 
            var analysis = new GameAnalysisModel
            {
                Help = state.Word
            };
            return analysis;
        }
    }
}
