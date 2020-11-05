using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.Models;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using static Stanford_ShopGolf.Areas.Admin.CommonAdmin.ConstantData;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
	public class KhachHangController : AdminBaseController
	{
		// GET: KhachHang
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult _LoadTable()
		{
			return View(DataProvider.Entities.KhachHangs.ToList());
		}
		public ActionResult Add()
		{
			PrepareDDLTinhThanh();
			PrepareDDLLoaiKH();
			return View(new KhachHang());
		}
		[HttpPost]
		public ActionResult Add(KhachHang model)
		{
			if (ModelState.IsValid)
			{
				DataProvider.Entities.KhachHangs.Add(model);
				DataProvider.Entities.SaveChanges();
				QuanTri.SaveLog("Thêm mới khách hàng ", "Quản lý khách hàng", (int)CommonAdmin.ConstantCommon.Action.Add);
				return RedirectToAction("Index", "KhachHang");
			}
			else
			{
				PrepareDDLTinhThanh();
				PrepareDDLLoaiKH();
				return View();
			}
		}
		public ActionResult Edit(int id)
		{
			if (id > 0)
			{
				var model = DataProvider.Entities.KhachHangs.Find(id);
				if (model != null)
				{
					PrepareDDLTinhThanh();
					PrepareDDLLoaiKH();
					return View(model);
				}

			}
			return RedirectToAction("Index", "KhachHang");
		}
		[HttpPost]
		public ActionResult Edit(KhachHang model)
		{
			if (ModelState.IsValid)
			{
				KhachHang obj = DataProvider.Entities.KhachHangs.Find(model.Id);
				if (obj != null)
				{
					
					DataProvider.Entities.Entry(obj).CurrentValues.SetValues(model);

					DataProvider.Entities.SaveChanges();
					QuanTri.SaveLog("Thêm mới khách hàng ", "Quản lý khách hàng", (int)CommonAdmin.ConstantCommon.Action.Add);
					return RedirectToAction("Index", "KhachHang");
				}

			}
			PrepareDDLTinhThanh();
			PrepareDDLLoaiKH();
			return View();

		}
		public JsonResult Delete(int id)
		{
			try
			{

				if (id > 0)
				{
					var obj = DataProvider.Entities.KhachHangs.FirstOrDefault(x => x.Id == id);


					DataProvider.Entities.KhachHangs.Remove(obj);
					QuanTri.SaveLog("Xóa khách hàng ", "Quản lý khách hàng", (int)CommonAdmin.ConstantCommon.Action.Delete);
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
		private void PrepareDDLLoaiKH()
		{
			var lstLoaiKH = DataProvider.Entities.LoaiKhachHangs.Select(p => new SelectListItem
			{
				Value = p.Id.ToString(),
				Text = p.TenLoai
			}).ToList();
			ViewBag.ddlLoaiKH = lstLoaiKH;
		}
		private void PrepareDDLTinhThanh()
		{
			var lstTinhThanh = DataProvider.Entities.TinhThanhs.Select(p => new SelectListItem
			{
				Value = p.Id.ToString(),
				Text = p.TenTinh
			}).ToList();
			ViewBag.ddlTinhThanh = lstTinhThanh;
		}

	}
}