using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.Models;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class ChuongTrinhUuDaiController : AdminBaseController
    {
        // GET: ChuongTrinhUuDai
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.ChuongTrinhUuDai, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadTable()
        {
            var data = DataProvider.Entities.ChuongTrinhUuDais.ToList();
            var lstData = new List<CTUDViewModel>();
            foreach (var x in data)
            {
                lstData.Add(new CTUDViewModel
                {
                    Id = x.Id,
                    MoTa = x.MoTa,
                    NguoiTao = (int)x.NguoiTaoId,
                    TenUuDai = x.TenChuongTrinh,
                    Time = ((DateTime)x.TuNgay).ToString("MM/dd/yyyy") + " ---- " + ((DateTime)x.DenNgay).ToString("MM/dd/yyyy")
                });
            }
            return View(lstData);
        }

        [HttpPost]
        public ActionResult CreateCTUDSP(ChuongTinhUuDaiCreateDto obj)
        {
            try
            {
                foreach (var item in obj.lstProductCode)
                {
                    var objSanPham = DataProvider.Entities.SanPhams.Find(item);
                    var data = new ChuongTrinhUuDai_SanPham
                    {
                        Id = new DateTimeOffset().ToUnixTimeMilliseconds(),
                        ChuongTrinhId = obj.CTUDCode,
                        SanPhamId = item,
                    };
                    //Giam tien
                    if (obj.Type == 1)
                    {
                        data.GiaGiam = double.Parse(obj.Discount);
                        objSanPham.GiaGiam = objSanPham.GiaBan - data.GiaGiam;
                        objSanPham.PhanTramGiam = (objSanPham.GiaGiam / objSanPham.GiaBan * 100);
                    }
                    //Giảm theo %
                    else if (obj.Type == 2)
                    {
                        data.PhanTramGiam = double.Parse(obj.Discount);
                        objSanPham.GiaGiam = objSanPham.GiaBan * (1 - data.PhanTramGiam / 100);
                        objSanPham.PhanTramGiam = (objSanPham.GiaGiam / objSanPham.GiaBan * 100);
                    }
                    DataProvider.Entities.ChuongTrinhUuDai_SanPham.Add(data);
                }
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = ConstantData.ResponseMessage.SuccessCreate
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse()
                {
                    Success = false,
                    Message = ConstantData.ResponseMessage.Error
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Create(ChuongTrinhUuDai obj)
        {
            try
            {
                //add
                var userLogin = Session[ConstantData.USER_SESSION] as NguoiDung;
                obj.NguoiTaoId = userLogin.Id;
                obj.NgayTao = DateTime.Now;
                DataProvider.Entities.ChuongTrinhUuDais.Add(obj);
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = ConstantData.ResponseMessage.SuccessCreate
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonResponse()
                {
                    Success = false,
                    Message = "Có lỗi xảy ra trong quá trình xử lý."
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Update(ChuongTrinhUuDai obj)
        {
            try
            {
                //update
                var data = DataProvider.Entities.ChuongTrinhUuDais.Find(obj.Id);
                data.TenChuongTrinh = obj.TenChuongTrinh;
                data.MoTa = obj.MoTa;
                data.TuNgay = obj.TuNgay;
                data.DenNgay = obj.DenNgay;
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = ConstantData.ResponseMessage.SuccessUpdate
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonResponse()
                {
                    Success = false,
                    Message = "Có lỗi xảy ra trong quá trình xử lý."
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LoadTableFlex()
        {
            return View(DataProvider.Entities.Flexes.ToList());
        }

        public JsonResult GetObjToEdit(int id)
        {
            try
            {
                var data = DataProvider.Entities.ChuongTrinhUuDais.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = "Không tìm thấy đối tượng này."
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = ConstantData.ResponseMessage.SuccessGet,
                    Data = new CTUDViewModel
                    {
                        Id = data.Id,
                        TuNgay = ((DateTime)data.TuNgay).ToString("MM/dd/yyyy"),
                        MoTa = data.MoTa,
                        TenUuDai = data.TenChuongTrinh,
                        DenNgay = ((DateTime)data.DenNgay).ToString("MM/dd/yyyy"),
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonResponse()
                {
                    Success = false,
                    Message = "Có lỗi xảy ra trong quá trình xử lý."
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult delete(int id)
        {
            try
            {
                var data = DataProvider.Entities.ChuongTrinhUuDais.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = "Không tìm thấy đối tượng cần xóa."
                    }, JsonRequestBehavior.AllowGet);
                }
                DataProvider.Entities.ChuongTrinhUuDais.Remove(data);
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = "Xóa thành công kích thước này."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonResponse()
                {
                    Success = false,
                    Message = "Không thể xóa đối tượng này. Vì sẽ ảnh hưởng đến dữ liệu khác."
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}