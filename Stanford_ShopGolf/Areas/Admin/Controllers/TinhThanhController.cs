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
    public class TinhThanhController : AdminBaseController
    {
        // GET: TinhThanh
        public ActionResult QLTinhThanh()
        {
            ViewBag.ddlQuocGia = DataProvider.Entities.QuocGias.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.TenQuocGia
            }).ToList();
            return View();
        }

        public ActionResult LoadTableTinhThanh()
        {
            return View(DataProvider.Entities.TinhThanhs.ToList());
        }

        public JsonResult Create(TinhThanh obj)
        {
            try
            {
                //add
                DataProvider.Entities.TinhThanhs.Add(obj);
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

        public JsonResult Update(TinhThanh obj)
        {
            try
            {
                //update
                var data = DataProvider.Entities.TinhThanhs.Find(obj.Id);
                data.MaBuuDien = obj.MaBuuDien;
                data.MaTinh = obj.MaTinh;
                data.QuocGiaId = obj.QuocGiaId;
                data.SapXep = obj.SapXep;
                data.TenTinh = obj.TenTinh;
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
                var data = DataProvider.Entities.TinhThanhs.Find(id);
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
                    Data = new TinhThanh
                    {
                        Id = data.Id,
                        MaBuuDien = data.MaBuuDien,
                        MaTinh = data.MaTinh,
                        QuocGiaId = data.QuocGiaId,
                        SapXep = data.SapXep,
                        TenTinh = data.TenTinh
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
                var data = DataProvider.Entities.TinhThanhs.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = ConstantData.ResponseMessage.NotFound
                    }, JsonRequestBehavior.AllowGet);
                }
                DataProvider.Entities.TinhThanhs.Remove(data);
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