using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ghost.AI.API.Models;

namespace Ghost.AI.API.SystemTests
{
    [TestClass]
    public class AnalyserControllerSystemTests
    {
        private readonly string ResourcePath = "/api/analyse";
        private GameStateModel State;        

        [TestMethod]
        public void Analize_ResponseMustNotBeNull()
        {
            State = new GameStateModel("hello");
            var analysis = Utilities.PostData<GameAnalysisModel,GameStateModel>(ResourcePath, State);
            Assert.IsNotNull(analysis);
        }

        [TestMethod]
        public void Analize_AllFieldsMustBeDefined()
        {
            State = new GameStateModel("hello");
            var analysis = Utilities.PostData<GameAnalysisModel, GameStateModel>(ResourcePath, State);
            Assert.IsNotNull(analysis.HasWinner);
            Assert.IsNotNull(analysis.Winner);
            Assert.IsNotNull(analysis.ExpectedWinner);
            Assert.IsNotNull(analysis.ExpectedMaxTurns);
            Assert.IsNotNull(analysis.Explanation);
            Assert.IsNotNull(analysis.Help);
        }

        [TestMethod]
        public void Analize_MustDetectWhenAWordIsCompleted()
        {
            State = new GameStateModel("hello");
            var analysis = Utilities.PostData<GameAnalysisModel, GameStateModel>(ResourcePath, State);
            Assert.IsTrue(analysis.HasWinner);
            Assert.AreEqual(1, analysis.Winner);
            Assert.IsTrue(analysis.Explanation.Contains("exists"));
        }

        [TestMethod]
        public void Analize_MustDetectWhenAWordDoesNotExist()
        {
            State = new GameStateModel("kkkkk");
            var analysis = Utilities.PostData<GameAnalysisModel, GameStateModel>(ResourcePath, State);
            Assert.IsTrue(analysis.HasWinner);
            Assert.AreEqual(1, analysis.Winner);
            Assert.IsTrue(analysis.Explanation.Contains("does not exist"));
        }

        [TestMethod]
        public void Analize_MustDetectWhoIsGoingToWin()
        {
            State = new GameStateModel("hell");
            var analysis = Utilities.PostData<GameAnalysisModel, GameStateModel>(ResourcePath, State);
            Assert.IsFalse(analysis.HasWinner);
            Assert.AreEqual(-1, analysis.Winner);
            Assert.AreEqual(0, analysis.ExpectedWinner);            
        }

        [TestMethod]
        public void Analize_MustGiveAdvicesToWin()
        {
            State = new GameStateModel("hell");
            var analysis = Utilities.PostData<GameAnalysisModel, GameStateModel>(ResourcePath, State);
            Assert.IsTrue(analysis.Help.Contains("win going for"));
        }

    }
}
