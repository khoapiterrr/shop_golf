using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class HomeAdminController : AdminBaseController
    {
        public ActionResult Index()
        {
            //Lấy số lượng người dùng
            ViewBag.SoNguoiDung = DataProvider.Entities.NguoiDungs.Count();

            //Thống kê số lượng thương hiệu trong hệ thống
            ViewBag.SoLuongThuongHieu = DataProvider.Entities.Brands.Count();

            //Thống kê số hóa đơn
            ViewBag.SoHoaDon = DataProvider.Entities.HoaDonBans.Count();

            //Thống kê số lượng khách hàng
            ViewBag.SoKhachHang = DataProvider.Entities.KhachHangs.Count();

            //Thống kê số lượng hàng
            ViewBag.SoLuongHang = DataProvider.Entities.SanPhams.Count();

            //Thống kê số lượng đơn vị bán
            ViewBag.SoLuongDVBan = 0;

            //Thống kê số lượng đơn vị mua
            ViewBag.SoLuongDVMua = 0;

            return View();
        }

        public ActionResult table()
        {
            return View();
        }
    }
}