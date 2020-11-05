using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using Stanford_ShopGolf.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class FlexController : AdminBaseController
    {
        // GET: Flex
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyFlex, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult QLFlex()
        {
            return View();
        }

        public JsonResult CreateFlex(Flex obj)
        {
            try
            {
                //add
                DataProvider.Entities.Flexes.Add(obj);
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = "Thêm mới thành công flex."
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

        public JsonResult UpdateFlex(Flex obj)
        {
            try
            {
                //update
                var data = DataProvider.Entities.Flexes.Find(obj.Id);
                data.FlexName = obj.FlexName;
                data.OrderNumber = obj.OrderNumber;
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = "Cập nhật thành công flex."
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
                var data = DataProvider.Entities.Flexes.Find(id);
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
                    Message = "lấy thành công flex này.",
                    Data = new Flex
                    {
                        Id = data.Id,
                        OrderNumber = data.OrderNumber,
                        FlexName = data.FlexName,
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
                var data = DataProvider.Entities.Flexes.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = "Không tìm thấy đối tượng cần xóa."
                    }, JsonRequestBehavior.AllowGet);
                }
                DataProvider.Entities.Flexes.Remove(data);
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