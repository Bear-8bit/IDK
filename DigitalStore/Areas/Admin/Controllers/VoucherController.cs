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
    public class VoucherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Game
        public ActionResult Index(int? page)
        {
            IEnumerable<Voucher> items = db.Vouchers.OrderByDescending(x => x.Id);
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
            ViewBag.VoucherCategory = new SelectList(db.VoucherCategories.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(Voucher model)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[10];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            if (ModelState.IsValid)
            {
                model.VoucherCode = finalString;
                model.StartDate = DateTime.Now;
                db.Vouchers.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VoucherCategory = new SelectList(db.VoucherCategories.ToList(), "Id", "Name");
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.VoucherCategory = new SelectList(db.VoucherCategories.ToList(), "Id", "Name");
            var item = db.Vouchers.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Voucher model)
        {
            if (ModelState.IsValid)
            {
                db.Vouchers.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Vouchers.Find(id);
            if (item != null)
            {
                db.Vouchers.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}