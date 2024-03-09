using DigitalStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalStore.Areas.Admin.Controllers
{
    public class RevenueController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Revenue
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails
                        on o.Id equals od.OrderId
                        join p in db.Games
                        on od.GameId equals p.Id
                        select new
                        {
                            CreateDate = o.CreatedDate,
                            Price = od.Price,
                            Quantity = od.Quantity,
                            OriginalPrice = p.OriginalPrice
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate < endDate);
            }
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreateDate)).Select(x => new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y =>  y.Price),
                TotalSell = x.Sum(y =>  y.OriginalPrice),
            }).Select(x => new
            {
                Date = x.Date,
                DoanhThu = x.TotalBuy,
                LoiNhuan = x.TotalSell,
            });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}