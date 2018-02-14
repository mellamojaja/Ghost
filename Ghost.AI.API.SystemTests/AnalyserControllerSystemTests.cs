using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ghost.AI.API.Models;

namespace Ghost.AI.API.SystemTests
{
    [TestClass]
    public class AnalyserControllerSystemTests
    {
        private readonly string ResourcePath = "/api/analyse";
        private GameStateModel State;

        [TestInitialize]
        public void BeforeEachTest()
        {
            State = new GameStateModel("hello");
        }

        [TestMethod]
        public void Analize_MustNotReturnNullResponse()
        {            
            var analysis = Utilities.PostData<GameAnalysisModel,GameStateModel>(ResourcePath, State);
            Assert.IsNotNull(analysis);
        }

        [TestMethod]
        public void Analize_ResponseWinnerFieldMustBeFilled()
        {           
            var analysis = Utilities.PostData<GameAnalysisModel, GameStateModel>(ResourcePath, State);
            Assert.AreEqual(-1, analysis.Winner);
        }
    }
}
