using DigitalStore.Models;
using DigitalStore.Models.EF;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DigitalStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Publisher")]
    public class GameController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Game
        public ActionResult Index(string SearchText, int? page)
        {
            IEnumerable<Game> items = db.Games.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Alias.Contains(SearchText) || x.Name.Contains(SearchText)).ToList();
            }
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            ViewBag.Publisher = new SelectList(db.Publishers.ToList(), "Id", "Name");
            ViewBag.GameGenre = new SelectList(db.GameGenres.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(Game model, Publisher model2, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid) 
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.GameImage.Add(new GameImage
                            {
                                GameID = model.Id,
                                Image = Images[i],
                                IsDefault = true
                            });
                        }
                        else
                        {
                            model.GameImage.Add(new GameImage
                            {
                                GameID = model.Id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.Alias = DigitalStore.Models.Common.Filter.FilterChar(model.Name);
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.Games.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Publisher = new SelectList(db.Publishers.ToList(), "Id", "Name");
            ViewBag.GameGenre = new SelectList(db.GameGenres.ToList(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Games.Find(id);
            if (item != null)
            {
                db.Games.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Publisher = new SelectList(db.Publishers.ToList(), "Id", "Name");
            ViewBag.GameGenre = new SelectList(db.GameGenres.ToList(), "Id", "Name");
            var item = db.Games.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Game model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = DigitalStore.Models.Common.Filter.FilterChar(model.Name);
                db.Games.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Games.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsNew(int id)
        {
            var item = db.Games.Find(id);
            if (item != null)
            {
                item.IsNew = !item.IsNew;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsNew = item.IsNew });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsFeatured(int id)
        {
            var item = db.Games.Find(id);
            if (item != null)
            {
                item.IsFeatured = !item.IsFeatured;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsFeatured = item.IsFeatured });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Games.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.Games.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsSale = item.IsSale });
            }
            return Json(new { success = false });
        }
    }
}