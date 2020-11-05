using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford.ShopGolf.Business.Entity;
using Stanford.ShopGolf.Business;
using Stanford_ShopGolf.Areas.Admin.Models;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class ChuDeController : AdminBaseController
    {
        // GET: ChuDe
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyChuDe, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index()
        {
            LoadViewBag();
            return View();
        }

        /// <summary>
        /// Hàm lấy danh sách dữ liệu chủ đề lên danh sách
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadTable()
        {
            //ShopGolfEntities Entities = new ShopGolfEntities();

            return View(DataProvider.Entities.ChuDes.ToList());
        }

        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyVaiTro, Action = CommonAdmin.ConstantCommon.Action.Edit)]
        /// <summary>
        /// Hàm lấy thông tin để đưa lên modal sửa
        /// Author              Date            Comments
        /// DangBQ          27/09/19        Tạo mới
        /// </summary>
        /// <param name="id">Id của chủ đề cần sửa</param>
        /// <returns>Đối tượng chủ đề lấy được</returns>
        public ActionResult LayChiTietById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    int chuDeId = -1;
                    int.TryParse(id, out chuDeId);

                    if (chuDeId > 0)
                    {
                        ChuDe objChuDe = DataProvider.Entities.ChuDes.Where(p => p.Id == chuDeId).First();

                        //Tạo đối tượng trả về
                        /*VaiTro objReturn = new VaiTro();
                        objReturn.Id = objChuDe.Id;
                        objReturn.TenVaiTro = obj.TenVaiTro;
                        objReturn.MoTa = obj.MoTa;*/

                        return Json(objChuDe, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new VaiTro(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonPostBack("Có lỗi xảy ra. Chi tiết: " + ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Hàm thêm mới hoặc cập nhật chủ đề
        /// Author              Date            Comments
        /// DangBQ          27/09/19        Tạo mới
        /// </summary>
        /// <returns></returns>
        public JsonResult AddOrUpdate()
        {
            try
            {
                bool isInsert = true;
                // Lấy thông tin từ giao diện
                int id = -1;
                int.TryParse(Request["hId"], out id);
                string tenChuDe = Request["tenChuDe"];
                string maCha = Request["maCha"];
                string avatar = Request["avatar"];
                int chuDeChaId = 0;
                int.TryParse(maCha, out chuDeChaId);

                ChuDe objChuDe = null;

                if (id > 0)//Lấy thông tin chủ đề nếu là sửa
                {
                    //Lấy chủ đề sửa
                    objChuDe = DataProvider.Entities.ChuDes.Where(c => c.Id == id).First();
                }

                //Nếu chủ đề khác null
                if (objChuDe != null)
                {
                    isInsert = false;
                    //Kiểm tra trùng tên
                    if (kiemTraTrungTen(tenChuDe, objChuDe.TenChuDe))
                    {
                        return Json(new JsonPostBack("Tên chủ đề đã tồn tại. Vui lòng nhập tên tên chủ đề khác", false), JsonRequestBehavior.AllowGet);
                    }
                }
                else//Nếu là thêm mới
                {
                    //Kiểm tra trùng tên
                    if (kiemTraTrungTen(tenChuDe, string.Empty))
                    {
                        return Json(new JsonPostBack("Tên chủ đề đã tồn tại. Vui lòng nhập tên tên chủ đề khác", false), JsonRequestBehavior.AllowGet);
                    }
                    //Khởi tạo
                    objChuDe = new ChuDe();
                }

                //Gán thông tin vào đối tượng chủ đề
                objChuDe.TenChuDe = tenChuDe;
                objChuDe.MaCha = chuDeChaId;

                var fileImg = Request.Files["fileImg"];
                //Nếu ảnh có chọn
                if (fileImg.ContentLength > 0)
                {
                    // Kiểm tra file Image đã tồn tại trên server hay chưa
                    string filePath = "~/Content/Admin/images/ChuDe/" + avatar;
                    string Path = HttpContext.Server.MapPath(filePath);
                    if (!System.IO.File.Exists(Path))
                    {
                        fileImg.SaveAs(Server.MapPath("~/Content/Admin/images/ChuDe/" + fileImg.FileName));
                    }
                }
                else
                {
                    avatar = Request["avatar"];
                }

                objChuDe.Avatar = avatar;
                //Nếu là thêm mới
                if (isInsert)
                {
                    DataProvider.Entities.Entry(objChuDe).State = System.Data.Entity.EntityState.Added;
                    DataProvider.Entities.ChuDes.Add(objChuDe);
                    //Lưu sự thay đổi vào db
                    DataProvider.Entities.SaveChanges();

                    QuanTri.SaveLog("Thêm mới chủ đề", "Quản lý chủ đề", (int)CommonAdmin.ConstantCommon.Action.Add);
                    return Json(new JsonPostBack("Thêm mới thành công", true), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //Lưu sự thay đổi vào db
                    DataProvider.Entities.SaveChanges();

                    QuanTri.SaveLog("Chỉnh sửa chủ đề", "Quản lý chủ đề", (int)CommonAdmin.ConstantCommon.Action.Edit);
                    return Json(new JsonPostBack("Cập nhật thành công", true), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý. Chi tiết: " + ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Hàm kiểm tra tên chủ đề có tồn tại ko
        /// Author              Date            Comments
        /// DangBQ          27/09/19        Tạo mới
        /// </summary>
        /// <param name="tenCD"></param>
        /// <returns></returns>
        public bool kiemTraTrungTen(string tenMoi, string tenCu)
        {
            try
            {
                return DataProvider.Entities.ChuDes.Where(x => x.TenChuDe.Equals(tenMoi) && tenMoi != tenCu).Count() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm xử lý xóa thông tin chủ đề
        /// Author              Date            Comments
        /// DangBQ          27/09/19        Tạo mới
        /// </summary>
        /// <returns></returns>
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyVaiTro, Action = CommonAdmin.ConstantCommon.Action.Delete)]
        public JsonResult delete()
        {
            try
            {
                int id = -1;
                int.TryParse(Request["id"], out id);
                if (id > 0)
                {
                    var obj = DataProvider.Entities.ChuDes.FirstOrDefault(x => x.Id == id);
                    if (obj.SanPhams.Count > 0)
                    {
                        return Json(new JsonPostBack("Không thể xóa chủ đề này này, vì sẽ ảnh hưởng đến dữ liệu khác", false), JsonRequestBehavior.AllowGet);
                    }
                    DataProvider.Entities.ChuDes.Remove(obj);
                    QuanTri.SaveLog("Xóa chủ đề", "Quản lý vai trò", (int)CommonAdmin.ConstantCommon.Action.Edit);
                    DataProvider.Entities.SaveChanges();

                    return Json(new JsonPostBack("Xóa thành công", true), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
            }
        }
    }
}