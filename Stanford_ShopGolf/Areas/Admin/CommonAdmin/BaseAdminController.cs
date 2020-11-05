using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Areas.Admin.CommonAdmin
{
    public class BaseAdminController : Controller
    {
        // GET: Admin/BaseAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}