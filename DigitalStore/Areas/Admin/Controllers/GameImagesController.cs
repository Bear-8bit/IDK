using DigitalStore.Models;
using DigitalStore.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Publisher")]
    public class GameImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.GameId = id;
            var items = db.GameImages.Where(x => x.GameID == id).ToList();
            return View(items);
        }

        [HttpPost]
        public ActionResult AddImage(int gameId, string url)
        {
            db.GameImages.Add(new GameImage
            {
                GameID = gameId,
                Image = url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.GameImages.Find(id);
            db.GameImages.Remove(item);
            db.SaveChanges();
            return Json(new { sucess = true });
        }
    }
}