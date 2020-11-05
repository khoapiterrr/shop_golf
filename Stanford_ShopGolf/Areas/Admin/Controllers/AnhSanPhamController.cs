using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.Models;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Stanford_ShopGolf.Areas.Admin.CommonAdmin.ConstantData;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class AnhSanPhamController : AdminBaseController
    {
        // GET: AnhSanPham
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.AnhSanPham, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _LoadList(int SPID)
        {
            return PartialView(DataProvider.Entities.AnhSanPhams.Where(p => p.SanPhamId == SPID).OrderByDescending(p => p.Id).ToList());
        }

        public JsonResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var obj = DataProvider.Entities.AnhSanPhams.FirstOrDefault(x => x.Id == id);

                    DataProvider.Entities.AnhSanPhams.Remove(obj);
                    QuanTri.SaveLog("Xóa ảnh ", "Quản lý ảnh", (int)CommonAdmin.ConstantCommon.Action.Delete);
                    DataProvider.Entities.SaveChanges();
                    //xóa ảnh trên server
                    var path = Server.MapPath("~/Content/Images/SanPham/AnhSP/" + obj.Avatar);
                    FileInfo fileInfo = new FileInfo(path);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }

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

        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase[] fUploads, int idSP)
        {
            try
            {
                foreach (var item in fUploads)
                {
                    var objAnhSanPham = new AnhSanPham();

                    if (item != null && item.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(item.FileName);

                        item.SaveAs(Server.MapPath("~/Content/Images/SanPham/AnhSP/" + fileName));

                        objAnhSanPham.Avatar = fileName;
                        objAnhSanPham.TenAnh = fileName;
                        objAnhSanPham.SanPhamId = idSP;
                        DataProvider.Entities.AnhSanPhams.Add(objAnhSanPham);
                    }
                }

                DataProvider.Entities.SaveChanges();

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
    }
}