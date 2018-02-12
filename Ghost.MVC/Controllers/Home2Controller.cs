using Ghost.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ghost.MVC.Controllers
{
    

    public class Home2Controller : Controller
    {
        private PlayerNameModel PlayerName;
        
        public ActionResult Start()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Start(PlayerNameModel playerName)
        {
            PlayerName = playerName;

            if (! string.IsNullOrEmpty(PlayerName.Name))
            {
                // Start game
                return RedirectToAction("create", "game", PlayerName);
            }

            return View();
        }        

    }
}