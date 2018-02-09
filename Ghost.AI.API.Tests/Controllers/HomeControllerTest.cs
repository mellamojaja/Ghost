using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ghost.AI.API;
using Ghost.AI.API.Controllers;

namespace Ghost.AI.API.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Disponer
            HomeController controller = new HomeController();

            // Actuar
            ViewResult result = controller.Index() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
