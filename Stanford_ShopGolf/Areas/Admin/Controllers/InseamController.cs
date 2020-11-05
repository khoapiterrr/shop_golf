using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.Models;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Stanford_ShopGolf.Areas.Admin.CommonAdmin.ConstantData;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class InseamController : AdminBaseController
    {
        // GET: Inseam

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _LoadTable()
        {
            return View(DataProvider.Entities.Inseams.OrderByDescending(p => p.Id).ToList());
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
                    Inseam obj = DataProvider.Entities.Inseams.Where(p => p.Id == id).First();
                    //Tạo đối tượng trả về
                    Inseam objReturn = new Inseam
                    {
                        Id = obj.Id,
                        InseamName = obj.InseamName,
                        OrderNumber = obj.OrderNumber
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
        public JsonResult Add(Inseam model)
        {
            try
            {
                DataProvider.Entities.Inseams.Add(model);
                DataProvider.Entities.SaveChanges();
                QuanTri.SaveLog("Thêm mới đường may ", "Quản lý đường may", (int)CommonAdmin.ConstantCommon.Action.Add);
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

        public virtual JsonResult Edit(Inseam model)
        {
            try
            {
                Inseam obj = DataProvider.Entities.Inseams.Find(model.Id);
                if (obj.Id > 0)
                {
                    obj.OrderNumber = model.OrderNumber;
                    obj.InseamName = model.InseamName;
                    DataProvider.Entities.SaveChanges();
                    QuanTri.SaveLog("Chỉnh sửa đường may ", "Quản lý đường may", (int)CommonAdmin.ConstantCommon.Action.Edit);
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
                    var obj = DataProvider.Entities.Inseams.FirstOrDefault(x => x.Id == id);

                    DataProvider.Entities.Inseams.Remove(obj);
                    QuanTri.SaveLog("Xóa đường may ", "Quản lý đường may", (int)CommonAdmin.ConstantCommon.Action.Delete);
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