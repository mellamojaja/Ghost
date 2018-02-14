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
        public void Get_WinnerPropertyMustBeFilled()
        {            
            var analysis = Controller.Post(State);
            Assert.AreEqual(-1, analysis.Winner);
        }
    }
}
