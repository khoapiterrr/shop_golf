using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : AdminBaseController
    {
        // GET: LoaiSanPham
        public ActionResult QLLoaiSP()
        {
            return View();
        }

        public ActionResult LoadTableLoaiSanPham()
        {
            return View(DataProvider.Entities.LoaiSanPhams.ToList());
        }

        public JsonResult CreateLoaiSP(LoaiSanPham obj)
        {
            try
            {
                //add
                DataProvider.Entities.LoaiSanPhams.Add(obj);
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
                    Message = ConstantData.ResponseMessage.Error
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateLoaiSP(LoaiSanPham obj)
        {
            try
            {
                //update
                var data = DataProvider.Entities.LoaiSanPhams.Find(obj.Id);
                data.TenLoai = obj.TenLoai;
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
                var data = DataProvider.Entities.LoaiSanPhams.Find(id);
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
                    Data = new LoaiSanPham
                    {
                        Id = data.Id,
                        SapXep = data.SapXep,
                        TenLoai = data.TenLoai,
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
                var data = DataProvider.Entities.LoaiSanPhams.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = ConstantData.ResponseMessage.NotFound
                    }, JsonRequestBehavior.AllowGet);
                }
                DataProvider.Entities.LoaiSanPhams.Remove(data);
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