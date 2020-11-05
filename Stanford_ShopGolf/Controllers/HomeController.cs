using Stanford.ShopGolf.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Lấy menu top 1 thôi
            var model = DataProvider.Entities.Sliders.Where(p => p.Level == 2 && p.DaDuyet == true);// demoService.GetSlideShows();
            return View(model.OrderBy(p => p.SapXep).ToList());
        }
    }
}
