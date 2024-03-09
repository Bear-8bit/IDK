using DigitalStore.Controllers;
using DigitalStore.Models;
using DigitalStore.Models.EF;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Publisher, Marketing Partner")]
    public class ContractController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Contract
        public ActionResult Index(int? page)
        {
            IEnumerable<Contract> items = db.Contracts.OrderByDescending(x => x.Id);
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
            ViewBag.ContractCate = new SelectList(db.ContractCategories.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(Contract model)
        {
            if (ModelState.IsValid)
            {
                
                Random rd = new Random();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Contract_Code = "HĐ" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                model.NameSideA = "Admin Website";
                db.Contracts.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractCate = new SelectList(db.ContractCategories.ToList(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Contracts.Find(id);
            if (item != null)
            {
                db.Contracts.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ContractCate = new SelectList(db.ContractCategories.ToList(), "Id", "Name");
            var item = db.Contracts.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Contract model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.Contracts.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}