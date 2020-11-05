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
    public class WidthController : AdminBaseController
    {
        // GET: Width
        public ActionResult QLWidth()
        {
            return View();
        }

        public ActionResult LoadTableWidth()
        {
            return View(DataProvider.Entities.Widths.ToList());
        }

        public JsonResult CreateWidth(Width obj)
        {
            try
            {
                //add
                DataProvider.Entities.Widths.Add(obj);
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

        public JsonResult UpdateWidth(Width obj)
        {
            try
            {
                //update
                var data = DataProvider.Entities.Widths.Find(obj.Id);
                data.SapXep = obj.SapXep;
                data.WidthName = obj.WidthName;
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
                var data = DataProvider.Entities.Widths.Find(id);
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
                    Data = new Width
                    {
                        Id = data.Id,
                        SapXep = data.SapXep,
                        WidthName = data.WidthName,
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
                var data = DataProvider.Entities.Widths.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = ConstantData.ResponseMessage.NotFound
                    }, JsonRequestBehavior.AllowGet);
                }
                DataProvider.Entities.Widths.Remove(data);
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