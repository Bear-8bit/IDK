using DigitalStore.Models;
using DigitalStore.Models.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace DigitalStore.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult _Review(int gameId)
        {
            ViewBag.GameId =  gameId;
            var item = new Review();
            if(User.Identity.IsAuthenticated) 
            {
                var UserStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var UserManager = new UserManager<ApplicationUser>(UserStore);
                var user = UserManager.FindByName(User.Identity.Name);
                if (user != null) 
                {
                    item.Email = user.Email;
                    item.FullName = user.FullName;
                    item.UserName = user.UserName; 

                }
                return PartialView(item);
            }
            return PartialView();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostReview(Review req) 
        {
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid)
            {
                req.CreateDate = DateTime.Now;
                _db.Reviews.Add(req);
                _db.SaveChanges();
                return RedirectToAction("GameGenre", "Games");
            }
            return Json(code);
        }

        [AllowAnonymous]
        public ActionResult _Load_Review(int gameId) 
        {
            var item = _db.Reviews.Where(x => x.GameId == gameId).OrderByDescending(x => x.Id).ToList();
            ViewBag.Count = item.Count;
            return PartialView(item);
        }
    }
}