using DigitalStore.Models;
using DigitalStore.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Xml.Schema;

namespace DigitalStore.Controllers
{
    public class WishListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: WishList
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if(page == null)
            {
                page = 1;
            }
            IEnumerable<Wishlist> item = db.Wishlist.Where(x => x.UserName == User.Identity.Name).OrderByDescending(x => x.CreatedDate);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(item);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostWishlist(int GameId)
        {
            if(Request.IsAuthenticated == false) 
            {
                return Json(new { Success = false, Message = "Bạn chưa đăng nhập" });
            }
            var checkItem = db.Wishlist.FirstOrDefault(x => x.GameId == GameId && x.UserName == User.Identity.Name);
            if(checkItem != null) 
            {
                return Json(new { Success = false, Message = "Sản phẩm đã được yêu thích" });
            }
            var item = new Wishlist();
            item.GameId = GameId;
            item.UserName = User.Identity.Name;
            item.CreatedDate = DateTime.Now;
            db.Wishlist.Add(item);
            db.SaveChanges();
            return Json(new {Success = true});
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostDeleteWishlist(int GameId)
        {
            var checkItem = db.Wishlist.FirstOrDefault(x => x.GameId == GameId && x.UserName == User.Identity.Name);
           
            if (checkItem != null)
            {
                db.Wishlist.Remove(checkItem);
                db.SaveChanges();
                return Json(new { Success = true, Message = "Xóa thành công" });
            }
            return Json(new { Success = false, Message = "Xóa thất bại" });
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}