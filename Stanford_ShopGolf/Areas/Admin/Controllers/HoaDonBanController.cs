using Stanford.ShopGolf.Business;
using Stanford_ShopGolf.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class HoaDonBanController : AdminBaseController
    {
        // GET: HoaDonBan
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyHoaDonBan, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _LoadTable()
        {
            DataProvider.Entities = new Stanford.ShopGolf.Business.Entity.ShopGolfEntities();
            return PartialView(DataProvider.Entities.HoaDonBans.ToList());
        }
    }
}