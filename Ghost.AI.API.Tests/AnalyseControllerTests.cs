using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ghost.AI.API.Models;
using Ghost.AI.API.Controllers;

namespace Ghost.AI.API.Tests
{
    [TestClass]
    public class AnalyseControllerTests
    {
        private GameStateModel State;
        private AnalyseController Controller;

        [TestInitialize]
        public void BeforeEachTest()
        {
            Controller = new AnalyseController();

            State = new GameStateModel("hello");
        }

        [TestMethod]
        public void Get_MustNotReturnNull()
        {            
            var analysis = Controller.Post(State);
            Assert.IsNotNull(analysis);
        }

        [TestMethod]
        public void Analize_AllFieldsMustBeDefined()
        {
            State = new GameStateModel("hello");
            var analysis = Controller.Post(State);
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
            var analysis = Controller.Post(State);
            Assert.IsTrue(analysis.HasWinner);
            Assert.AreEqual(1, analysis.Winner);
            Assert.IsTrue(analysis.Explanation.Contains("exists"));
        }

        [TestMethod]
        public void Analize_MustDetectWhenAWordDoesNotExist()
        {
            State = new GameStateModel("kkkkk");
            var analysis = Controller.Post(State);
            Assert.IsTrue(analysis.HasWinner);
            Assert.AreEqual(1, analysis.Winner);
            Assert.IsTrue(analysis.Explanation.Contains("does not exist"));
        }

        [TestMethod]
        public void Analize_MustDetectWhoIsGoingToWin()
        {
            State = new GameStateModel("hell");
            var analysis = Controller.Post(State);
            Assert.IsFalse(analysis.HasWinner);
            Assert.AreEqual(-1, analysis.Winner);
            Assert.AreEqual(0, analysis.ExpectedWinner);
        }

        [TestMethod]
        public void Analize_MustGiveAdvicesToWin()
        {
            State = new GameStateModel("hell");
            var analysis = Controller.Post(State);
            Assert.IsTrue(analysis.Help.Contains("win going for"));
        }
    }
}
