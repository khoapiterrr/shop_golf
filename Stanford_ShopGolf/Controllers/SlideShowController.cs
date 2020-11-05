using Stanford.ShopGolf.Business;
using Stanford_ShopGolf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Controllers
{
    public class SlideShowController : Controller
    {
        //private readonly DemoService demoService;

        public SlideShowController()
        {
            //demoService = new DemoService();
        }

        // GET: SlideShow
        public ActionResult Main()
        {
            //Lấy menu top 1 thôi
            var model =  DataProvider.Entities.Sliders.Where(p=>p.Level==1);// demoService.GetSlideShows();
            return PartialView(model.OrderBy(p=>p.SapXep).ToList());
        }
    }
}