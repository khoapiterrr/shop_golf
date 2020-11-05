using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Stanford_ShopGolf.Areas.Admin.CommonAdmin.ConstantData;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class SliderController : AdminBaseController
    {
        // GET: Slider
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _LoadTable()
        {
            return View(DataProvider.Entities.Sliders.ToList());
        }

        [HttpPost]
        public JsonResult Create(Slider obj)
        {
            try
            {
                //add
                DataProvider.Entities.Sliders.Add(obj);
                obj.DaDuyet = false;
                obj.NgayTao = DateTime.Now;
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

        public JsonResult Update(Slider obj)
        {
            try
            {
                //update
                var data = DataProvider.Entities.Sliders.Find(obj.Id);
                data.AnhDaiDien = obj.AnhDaiDien ?? data.AnhDaiDien;
                data.SapXep = obj.SapXep;
                data.MoTa = obj.MoTa;
                data.TenSlider = obj.TenSlider;
                data.Link = obj.Link;
                data.KieuHienThi = obj.KieuHienThi;
                data.Level = obj.Level;
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
                var data = DataProvider.Entities.Sliders.Find(id);
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
                    Data = new Slider
                    {
                        Id = data.Id,
                        SapXep = data.SapXep,
                        TenSlider = data.TenSlider,
                        MoTa = data.MoTa,
                        AnhDaiDien = string.IsNullOrEmpty(data.AnhDaiDien) ? null : data.AnhDaiDien,
                        Link = data.Link,
                        KieuHienThi = data.KieuHienThi,
                        Level = data.Level
                        
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

        public JsonResult ChangeActive(int id)
        {
            try
            {
                var data = DataProvider.Entities.Sliders.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = ConstantData.ResponseMessage.NotFound
                    }, JsonRequestBehavior.AllowGet);
                }
                data.DaDuyet = !data.DaDuyet;
                DataProvider.Entities.SaveChanges();
                return Json(new JsonResponse()
                {
                    Success = true,
                    Data = new { duyet = data.DaDuyet },
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
		public JsonResult DuyetAll(int? trangthai, List<int> lstChecked)
		{
			try
			{
				if (lstChecked != null && lstChecked.Count > 0)
				{
					var lstSP = DataProvider.Entities.Sliders.Where(p => lstChecked.Contains(p.Id)).ToList();
					if (trangthai == 1)
					{
						foreach (var item in lstSP)
						{
							item.DaDuyet = true;
							
						}
					}
					else if (trangthai == 2)
					{
						foreach (var item in lstSP)
						{
							item.DaDuyet = false;						}
					}
					DataProvider.Entities.SaveChanges();
				}
				return Json(new JsonResponse
				{
					Message = ResponseMessage.SuccessGet,
					Success = true
				}, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new JsonResponse
				{
					Message = ResponseMessage.Error,
					Success = false
				}, JsonRequestBehavior.AllowGet);
			}
		}
        public JsonResult delete(int id)
        {
            try
            {
                var data = DataProvider.Entities.Sliders.Find(id);
                if (data == null)
                {
                    return Json(new JsonResponse()
                    {
                        Success = false,
                        Message = ConstantData.ResponseMessage.NotFound
                    }, JsonRequestBehavior.AllowGet);
                }
                DataProvider.Entities.Sliders.Remove(data);
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