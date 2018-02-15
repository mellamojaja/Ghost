using Ghost.MVC.Models;
using System.Web.Mvc;

namespace Ghost.MVC.Controllers
{
    public class HomeController : Controller
    {                
        public ActionResult Start()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Start(PlayerNameModel playerName)
        {            

            if (! string.IsNullOrEmpty(playerName.Name))
            {
                // Start game
                return RedirectToAction("create", "game", playerName);
            }

            return View();
        }        

    }
}