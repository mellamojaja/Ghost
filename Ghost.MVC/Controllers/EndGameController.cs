using Ghost.MVC.Models;
using System.Web.Mvc;


namespace Ghost.MVC.Controllers
{
    public class EndGameController : Controller
    {            
        public ActionResult Results()
        {
            if (Request.Params["command"] != null && Request.Params["command"].Equals("PlayAgain"))
            {
                return RedirectToAction("Reset", "Game");
            }

            return View(Game);
        }

        private GamePlayModel Game
        {
            get { return Session["game"] as GamePlayModel; }
            set { Session["game"] = value; }
        }

    }
}