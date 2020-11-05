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
    public class QuocGiaController : AdminBaseController
    {
        // GET: QuocGia
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyQuocGia, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult QLQuocGia()
        {
            return View();
        }

        public ActionResult LoadTableQG()
        {
            return View(DataProvider.Entities.QuocGias.ToList());
        }

        public JsonResult Create(QuocGia obj)
        {
            try
            {
                //add
                DataProvider.Entities.QuocGias.Add(obj);
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

        public JsonResult Update(QuocGia obj)
        {
            try
            {
                //update
                var data = DataProvider.Entities.QuocGias.Find(obj.Id);
                data.MaQuocGia = obj.MaQuocGia;
                data.TenQuocGia = obj.TenQuocGia;
                data.SapXep = obj.SapXep;
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
                    Message = ConstantData.ResponseMessage.Error
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetObjToEdit(int id)
        {
            try
            {
                var data = DataProvider.Entities.QuocGias.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = ConstantData.ResponseMessage.NotFound
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = ConstantData.ResponseMessage.SuccessGet,
                    Data = new QuocGia
                    {
                        Id = data.Id,
                        TenQuocGia = data.TenQuocGia,
                        MaQuocGia = data.MaQuocGia,
                        SapXep = data.SapXep
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonResponse()
                {
                    Success = false,
                    Message = ConstantData.ResponseMessage.Error
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult delete(int id)
        {
            try
            {
                var data = DataProvider.Entities.QuocGias.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = ConstantData.ResponseMessage.NotFound
                    }, JsonRequestBehavior.AllowGet);
                }
                DataProvider.Entities.QuocGias.Remove(data);
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Message = ConstantData.ResponseMessage.SuccessDelete
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonResponse()
                {
                    Success = false,
                    Message = ConstantData.ResponseMessage.ErrorDelete
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}