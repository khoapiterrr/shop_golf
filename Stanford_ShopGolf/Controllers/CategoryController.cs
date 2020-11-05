using Stanford_ShopGolf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Controllers
{
    public class CategoryController : Controller
    {
        private static DemoService demoService;
        private readonly CategoryServive _categoryService;

        public CategoryController()
        {
            demoService = new DemoService();
            _categoryService = new CategoryServive();
        }

        // GET: Category
        public ActionResult Index(int id, int? filter, int? type, int? page, int? sort)
        {
            var model = _categoryService.GetProduct(id, filter, type, page, sort);
            //Load more
            if (Request.IsAjaxRequest())
            {
                return PartialView("_LoadMoreProduct", model.Products);
            }
            return View(model);
        }
    }
}