using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class SizeController : AdminBaseController
    {
        // GET: Size
        public ActionResult QLSize()
        {
            return View();
        }

        public ActionResult LoadDataSize()
        {
            return View(DataProvider.Entities.Sizes.ToList());
        }

        public JsonResult CreateSize(Size obj)
        {
            try
            {
                //add
                DataProvider.Entities.Sizes.Add(obj);
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = "Thêm mới thành công kích thước."
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

        public JsonResult UpdateSize(Size obj)
        {
            try
            {
                //update
                var data = DataProvider.Entities.Sizes.Find(obj.Id);
                data.SizeName = obj.SizeName;
                data.SapXep = obj.SapXep;
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = "Cập nhật thành công kích thước."
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

        public JsonResult GetObjToEdit(int id)
        {
            try
            {
                var data = DataProvider.Entities.Sizes.Find(id);
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
                    Message = "Lấy thành công kích thước này.",
                    Data = new Size
                    {
                        Id = data.Id,
                        SapXep = data.SapXep,
                        SizeName = data.SizeName
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
                var data = DataProvider.Entities.Sizes.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = "Không tìm thấy đối tượng cần xóa."
                    }, JsonRequestBehavior.AllowGet);
                }
                DataProvider.Entities.Sizes.Remove(data);
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