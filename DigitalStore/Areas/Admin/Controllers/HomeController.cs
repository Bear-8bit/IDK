using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin, Publisher, Marketing Partner")]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}