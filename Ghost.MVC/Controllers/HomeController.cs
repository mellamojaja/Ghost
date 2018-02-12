using Ghost.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ghost.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var playerName = new PlayerNameModel();

            return View(playerName);
        }        
    }
}