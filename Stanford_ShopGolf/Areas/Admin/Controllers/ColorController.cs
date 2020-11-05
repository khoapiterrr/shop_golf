using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using static Stanford_ShopGolf.Areas.Admin.CommonAdmin.ConstantData;
using Stanford_ShopGolf.Areas.Admin.Models;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class ColorController : AdminBaseController
    {
        // GET: Color
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyCoLor, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _LoadTable()
        {
            return View(DataProvider.Entities.Colors.OrderByDescending(p => p.Id).ToList());
        }

        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyVaiTro, Action = CommonAdmin.ConstantCommon.Action.Edit)]
        /// <summary>
        /// hàm lấy thông tin để đưa lên modal sửa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetObjToEdit(int id)
        {
            try
            {
                if (id > 0)
                {
                    Color obj = DataProvider.Entities.Colors.Where(p => p.Id == id).First();
                    //Tạo đối tượng trả về
                    Color objReturn = new Color
                    {
                        Id = obj.Id,
                        ColorName = obj.ColorName,
                        ColorCode = obj.ColorCode
                    };

                    return Json(new JsonResponse
                    {
                        Message = ResponseMessage.SuccessGet,
                        Success = true,
                        Data = objReturn
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new VaiTro(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.Error,
                    Success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Add(Color model)
        {
            try
            {
                DataProvider.Entities.Colors.Add(model);
                DataProvider.Entities.SaveChanges();
                QuanTri.SaveLog("Thêm mới màu sắc ", "Quản lý màu sắc", (int)CommonAdmin.ConstantCommon.Action.Add);
                //return Json(new JsonPostBack("Thêm mới thành công", true), JsonRequestBehavior.AllowGet);
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.SuccessCreate,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.Error,
                    Success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public virtual JsonResult Edit(Color model)
        {
            try
            {
                Color obj = DataProvider.Entities.Colors.Find(model.Id);
                if (obj.Id > 0)
                {
                    obj.ColorCode = model.ColorCode;
                    obj.ColorName = model.ColorName;
                    DataProvider.Entities.SaveChanges();
                    QuanTri.SaveLog("Chỉnh sửa vai trò ", "Quản lý màu sắc", (int)CommonAdmin.ConstantCommon.Action.Edit);
                    //return Json(new JsonPostBack("Cập nhật thành công", true), JsonRequestBehavior.AllowGet);
                    return Json(new JsonResponse
                    {
                        Message = ResponseMessage.SuccessUpdate,
                        Success = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
                    return Json(new JsonResponse
                    {
                        Message = ResponseMessage.Error,
                        Success = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                //return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.Error,
                    Success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyVaiTro, Action = CommonAdmin.ConstantCommon.Action.Delete)]
        public JsonResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var obj = DataProvider.Entities.Colors.FirstOrDefault(x => x.Id == id);

                    if (obj.SanPham_Color.Count > 0)
                    {
                        return Json(new JsonPostBack("Không thể xóa màu này, vì sẽ ảnh hưởng đến dữ liệu khác", false), JsonRequestBehavior.AllowGet);
                    }
                    DataProvider.Entities.Colors.Remove(obj);
                    QuanTri.SaveLog("Xóa màu sắc ", "Quản lý màu sắc", (int)CommonAdmin.ConstantCommon.Action.Delete);
                    DataProvider.Entities.SaveChanges();

                    //return Json(new JsonPostBack("Xóa thành công", true), JsonRequestBehavior.AllowGet);
                    return Json(new JsonResponse
                    {
                        Message = ResponseMessage.SuccessDelete,
                        Success = true
                    }, JsonRequestBehavior.AllowGet);
                }
                //return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.Error,
                    Success = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                //return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.Error,
                    Success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}