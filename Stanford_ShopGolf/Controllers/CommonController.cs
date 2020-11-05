using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Services;
using Stanford_ShopGolf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Controllers
{
    public class CommonController : Controller
    {
        private readonly DemoService demoService;
        public CommonController() :base()
        {
            demoService = new DemoService();
        }


        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult TopNav()
        {
            IEnumerable<MenuViewModel> model = demoService.GetTopNavModels();
            return PartialView(model);
        }

        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult BreadCrumb(int ? id)
        {
            return PartialView();
        }

    }
}