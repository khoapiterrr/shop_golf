using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.Models;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Stanford_ShopGolf.Areas.Admin.CommonAdmin.ConstantData;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
	public class MenuAdminController : AdminBaseController
	{
		// GET: Menu
		public ActionResult Index()
		{
			LoadViewBag();

            LoadViewMenuBag();

            return View();
		}
		public void LoadViewMenuBag()
		{
			var lstMenu = Service.GetListParentMenu("...", 0);

			ViewBag.ddlMenu = new SelectList(lstMenu, "Id", "TenItem");
		}

        /// <summary>
        /// Hàm lấy tên chủ đề theo id
        /// </summary>
        /// <param name="chuDeId"></param>
        /// <returns></returns>
        public static string LayTenChuDe(int chuDeId)
        {
            string tenChuDe = "";
            ChuDe objCag = DataProvider.Entities.ChuDes.Find(chuDeId);
            if(objCag != null)
            {
                tenChuDe = objCag.TenChuDe;
            }
            return tenChuDe;
        }
		public ActionResult _LoadTable()
		{
            LoadViewBag();
            LoadViewMenuBag();
            return PartialView(DataProvider.Entities.Menus.OrderByDescending(p => p.Id).ToList());
		}

		public ActionResult GetObjToEdit(int id)
		{
			try
			{
				if (id > 0)
				{


					Menu obj = DataProvider.Entities.Menus.Where(p => p.Id == id).First();
					//Tạo đối tượng trả về 
					Menu objReturn = new Menu
					{
						Id = obj.Id,
						TenItem = obj.TenItem,
						SapXep = obj.SapXep,
						ParentId = obj.ParentId,
						MoTa = obj.MoTa,
                        HeadTitle = obj.HeadTitle,
                        ImagePath = obj.ImagePath,
						Link = obj.Link,
                        ChuDeId = obj.ChuDeId
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
		public JsonResult Add(Menu model, HttpPostedFileBase file)
		{
			try
			{
                if(model.ParentId==null || !model.ParentId.HasValue)
                {
                    model.ParentId = 0;
                }

                if (model.SapXep == null || !model.SapXep.HasValue)
                {
                    model.SapXep = 0;
                }

                if (file!= null && file.ContentLength>0)
				{
					model.ImagePath = file.FileName;
					//tải file về
					file.SaveAs(Server.MapPath("~/Content/Images/Menu/" + model.ImagePath));
				}

                DataProvider.Entities.Menus.Add(model);
				DataProvider.Entities.SaveChanges();
				QuanTri.SaveLog("Thêm mới menu ", "Quản lý menu", (int)CommonAdmin.ConstantCommon.Action.Add);
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
		public virtual JsonResult Edit(Menu model, HttpPostedFileBase file)
		{
			try
			{
				Menu obj = DataProvider.Entities.Menus.Find(model.Id);
				if (obj.Id > 0)
				{
                    if (model.ParentId == null || !model.ParentId.HasValue)
                    {
                        model.ParentId = 0;
                    }
                    if (model.SapXep == null || !model.SapXep.HasValue)
                    {
                        model.SapXep = 0;
                    }
                    obj.TenItem = model.TenItem;
					obj.MoTa = model.MoTa;
					obj.ParentId = model.ParentId;
					obj.SapXep	 = model.SapXep;
					obj.Link = model.Link;
					obj.HeadTitle = model.HeadTitle;
                    obj.ChuDeId = model.ChuDeId;

					if (file != null && file.ContentLength > 0)
					{
						obj.ImagePath = file.FileName;
						//tải file về
						file.SaveAs(Server.MapPath("~/Content/Images/Menu/" + obj.ImagePath));
					}
					DataProvider.Entities.SaveChanges();
					QuanTri.SaveLog("Chỉnh sửa menu ", "Quản lý menu", (int)CommonAdmin.ConstantCommon.Action.Edit);
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
			catch (Exception ex)
			{

				//return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
				return Json(new JsonResponse
				{
					Message = ResponseMessage.Error + ". Chi tiết: " + ex.Message,
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
					var obj = DataProvider.Entities.Menus.FirstOrDefault(x => x.Id == id);

					
					DataProvider.Entities.Menus.Remove(obj);
					QuanTri.SaveLog("Xóa menu ", "Quản lý menu", (int)CommonAdmin.ConstantCommon.Action.Delete);
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
				return Json(new JsonResponse
				{
					Message = ResponseMessage.Error,
					Success = false
				}, JsonRequestBehavior.AllowGet);
			}
		}
	}
}