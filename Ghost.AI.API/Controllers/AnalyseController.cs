using Ghost.AI.API.Models;
using System.Web.Http;

namespace Ghost.AI.API.Controllers
{    
    /// <summary>
    /// The analyse of a game state that may help to decide the next move
    /// </summary>
    public class AnalyseController : ApiController
    {
        // POST: api/analyse/{state}
        public GameAnalysisModel Post(GameStateModel state)
        {
            var analysis = Analyzer.Instance.Analyze(state);

            return analysis;
        }
    }
}
