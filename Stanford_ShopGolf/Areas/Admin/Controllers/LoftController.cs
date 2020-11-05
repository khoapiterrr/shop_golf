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
    public class LoftController : AdminBaseController
    {
        // GET: Loft
        public ActionResult QLLoaiLoft()
        {
            return View();
        }

        public ActionResult LoadTableLoft()
        {
            return View(DataProvider.Entities.Lofts.ToList());
        }

        public JsonResult CreateLoft(Loft obj)
        {
            try
            {
                //add
                DataProvider.Entities.Lofts.Add(obj);
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

        public JsonResult UpdateLoft(Loft obj)
        {
            try
            {
                //update
                var data = DataProvider.Entities.Lofts.Find(obj.Id);
                data.LoftDegress = obj.LoftDegress;
                data.OrderNumber = obj.OrderNumber;
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
                var data = DataProvider.Entities.Lofts.Find(id);
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
                    Data = new Loft
                    {
                        Id = data.Id,
                        OrderNumber = data.OrderNumber,
                        LoftDegress = data.LoftDegress,
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
                var data = DataProvider.Entities.Lofts.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = ConstantData.ResponseMessage.NotFound
                    }, JsonRequestBehavior.AllowGet);
                }
                DataProvider.Entities.Lofts.Remove(data);
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