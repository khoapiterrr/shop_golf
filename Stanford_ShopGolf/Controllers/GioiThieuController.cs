using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Controllers
{
    public class GioiThieuController : Controller
    {
        // GET: GioiThieu
        public ActionResult Index()
        {
            CauHinh objCH = DataProvider.Entities.CauHinhs.FirstOrDefault();

            string strGioiThieu = "";
            if(objCH != null)
            {
                strGioiThieu = objCH.GioiThieu;
            }

            ViewBag.GioiThieu = strGioiThieu;
            return View();
        }
    }
}