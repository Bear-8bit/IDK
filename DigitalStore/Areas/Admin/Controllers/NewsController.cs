using DigitalStore.Models;
using DigitalStore.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalStore.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<GameNews> items = db.GameNews.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Alias.Contains(SearchText) || x.Title.Contains(SearchText)).ToList();
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Add()
        {
            ViewBag.Game = new SelectList(db.Games.ToList(), "Id", "Name");
            ViewBag.Publisher = new SelectList(db.Publishers.ToList(), "Id", "Name");
            ViewBag.NewsCategory = new SelectList(db.NewsCategories.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(GameNews model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = DigitalStore.Models.Common.Filter.FilterChar(model.Title);
                db.GameNews.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Game = new SelectList(db.Games.ToList(), "Id", "Name");
            ViewBag.Publisher = new SelectList(db.Publishers.ToList(), "Id", "Name");
            ViewBag.NewsCategory = new SelectList(db.NewsCategories.ToList(), "Id", "Name");
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Game = new SelectList(db.Games.ToList(), "Id", "Name");
            ViewBag.Publisher = new SelectList(db.Publishers.ToList(), "Id", "Name");
            ViewBag.NewsCategory = new SelectList(db.NewsCategories.ToList(), "Id", "Name");
            var item = db.GameNews.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(GameNews model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = DigitalStore.Models.Common.Filter.FilterChar(model.Title);
                db.GameNews.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.GameNews.Find(id);
            if (item != null)
            {
                db.GameNews.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.GameNews.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.GameNews.Find(Convert.ToInt32(item));
                        db.GameNews.Remove(obj);
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}