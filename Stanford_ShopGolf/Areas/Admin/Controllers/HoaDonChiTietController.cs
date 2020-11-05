using Stanford.ShopGolf.Business;
using Stanford_ShopGolf.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class HoaDonChiTietController : AdminBaseController
    {
        // GET: HoaDonChiTiet
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyHoaDonChiTiet, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _LoadTable(int hoadonId)
        {
            return View(DataProvider.Entities.HoaDonChiTiets.Where(p => p.HoaDonId == hoadonId).ToList());
        }
    }
}