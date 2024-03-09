using DigitalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalStore.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop() 
        {
            var item = db.Menus.OrderBy(x => x.Id).ToList();
            return PartialView("_MenuTop", item);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.Genre_ID = id;
            }
            var items = db.GameGenres.ToList();
            return PartialView("_MenuLeft", items);
        }
        public ActionResult MenuGameGenre()
        {
            var items = db.GameGenres.ToList();
            return PartialView("_MenuGameGenre", items);
        }

        public ActionResult MenuArrivals()
        {
            var items = db.GameGenres.Take(5).ToList();
            return PartialView("_MenuArrivals", items);
        }
    }
}