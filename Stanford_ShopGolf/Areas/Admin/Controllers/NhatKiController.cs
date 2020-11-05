using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.Models;
namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class NhatKiController : AdminBaseController
    {
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyNhatKyHeThong, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadTable()
        {
            var lstNguoiDung = DataProvider.Entities.NguoiDungs.ToList();
            var lstNhatKy = DataProvider.Entities.NhatKyHeThongs.ToList();
            var lstObj = (from nk in lstNhatKy
                          join u in lstNguoiDung on nk.NguoiDungId equals u.Id
                          into user
                          from us in user.DefaultIfEmpty()
                          select new NhatKyModel()
                          {
                              Id = nk.Id,
                              TenNhatKy = nk.MoTa,
                              NgayTao = nk.NgayTao,
                              NguoiTaoId = nk.NguoiDungId,
                              TenNguoiTao = us!= null ? us.HoTen : "",
                              TenChucNang = nk.FormName,
                              HanhDong = nk.HanhDong.TenHanhDong,
                              DiaChiIP = nk.DiaChiMayTinh
                          }).ToList();
            return View(lstObj);
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyNhatKyHeThong, Action = CommonAdmin.ConstantCommon.Action.Delete)]
        public JsonResult deleteAll()
        {
            try
            {
                string allNK = Request["id"];
                string[] lstId = allNK.Split('-');
                NhatKyHeThong objTmp = null;
                foreach (var item in lstId)
                {
                    int id = -1;
                    int.TryParse(item, out id);
                    if (id>0)
                    {
                        objTmp = DataProvider.Entities.NhatKyHeThongs.Find(id);
                    }
                    if (objTmp != null)
                    {
                        DataProvider.Entities.NhatKyHeThongs.Remove(objTmp);
                    }
                }
                DataProvider.Entities.SaveChanges();
                return Json(new JsonPostBack("Xóa thành công", true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
            }
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyNhatKyHeThong, Action = CommonAdmin.ConstantCommon.Action.Delete)]
        public JsonResult delete()
        {
            try
            {
                int id = -1;
                int.TryParse(Request["id"], out id);
                if (id > 0)
                {
                    var obj = DataProvider.Entities.NhatKyHeThongs.FirstOrDefault(x => x.Id == id);
                    //-------------------------------------------------------------------------------
                    DataProvider.Entities.NhatKyHeThongs.Remove(obj);
                    DataProvider.Entities.SaveChanges();

                    return Json(new JsonPostBack("Xóa thành công", true), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
            }
        }

    }
}