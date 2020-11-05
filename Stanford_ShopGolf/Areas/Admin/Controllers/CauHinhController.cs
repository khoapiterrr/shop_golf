using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class CauHinhController : Controller
    {
        // GET: Admin/CauHinh
        public ActionResult Index()
        {
            CauHinh objCH = DataProvider.Entities.CauHinhs.FirstOrDefault();

            if(objCH != null)
            {
                return View(objCH);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(int? id, CauHinh objCauHinh)
        {
            CauHinh objCH = null;
            bool isInsert = true;
            if(id.HasValue && id.Value > 0)
            {
                objCH = DataProvider.Entities.CauHinhs.Where(c => c.Id == id).FirstOrDefault();
            }

            if(objCH != null)
            {
                isInsert = false;
            }
            else
            {
                objCH = new CauHinh();
            }

            if(isInsert)
            {
                DataProvider.Entities.CauHinhs.Add(objCauHinh);
            }
            else
            {
                DataProvider.Entities.Entry(objCH).CurrentValues.SetValues(objCauHinh);
            }

            //Lưu thông tin thay đổi
            DataProvider.Entities.SaveChanges();
            return View();
        }
    }
}