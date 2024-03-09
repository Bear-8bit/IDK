using DigitalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalStore.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index()
        {
            var items = db.GameNews.ToList();
            return View(items);
        }
        public ActionResult Details(int id)
        {
            var item = db.GameNews.Find(id);
            return View(item);
        }
    }
}