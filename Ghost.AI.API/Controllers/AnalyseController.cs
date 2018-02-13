using Ghost.AI.API.Models;
using System.Web.Http;

namespace Ghost.AI.API.Controllers
{
    public class AnalyseController : ApiController
    {
        // GET: api/GhostAnalyser/foobar
        public GameAnalysisModel Get(GameStateModel state)
        {
            // Do some logic 
            var analysis = new GameAnalysisModel();
            return analysis;
        }

        


    }
}
