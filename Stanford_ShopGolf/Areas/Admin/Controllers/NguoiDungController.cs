using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.Models;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class NguoiDungController : AdminBaseController
    {
        // GET: NguoiDung
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyNguoiDung, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index()
        {
            LoadViewBag();
            return View();
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyNguoiDung, Action = CommonAdmin.ConstantCommon.Action.View)]
        /// <summary>
        /// Author     Date        comment
        /// KhoaLT    5/4/2019      load dữ liệu người dùng
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadTable()
        {

            var lstObj = DataProvider.Entities.NguoiDungs.ToList();
            return View(lstObj);
        }
        /// <summary>
        /// Tạo Viewbag selectlist
        /// </summary>
        public void LoadViewBag()
        {
            var lstVaiTro = DataProvider.Entities.VaiTroes.ToList();
            ViewBag.ddlVaiTro = new SelectList(lstVaiTro, "Id", "TenVaiTro");
        }


        /// <summary>
        /// Hàm Lấy thông tin người dùng bằng username
        /// Author          Date            Comments
        /// KhoaLT        5/5/2019        Tạo mới
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public NguoiDung getObjByUser(string user)
        {
            try
            {
                return DataProvider.Entities.NguoiDungs.Where(x => x.TenDangNhap.Equals(user)).First();
            }
            catch (Exception)
            {
                return new NguoiDung();
            }
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyNguoiDung, Action = CommonAdmin.ConstantCommon.Action.Delete)]
        public JsonResult delete()
        {
            try
            {
                int id = -1;
                int.TryParse(Request["id"], out id);
                if (id > 0)
                {
                    var obj = DataProvider.Entities.NguoiDungs.FirstOrDefault(x => x.Id == id);
                    
                    DataProvider.Entities.NguoiDungs.Remove(obj);
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
        public JsonResult CapNhatThongTin()
        {
            int id = int.Parse(Request["id"]);
            string emdil = Request["email"];
            string sdt = Request["sdt"];
            string diaChi = Request["diachi"];
            string name = Request["name"];
            if (id >0)
            {
                try
                {
                    var obj = DataProvider.Entities.NguoiDungs.Find(id);
                    if (obj != null)
                    {
                        obj.Email = emdil;
                        obj.DiaChi = diaChi;
                        obj.DienThoai = sdt;
                        obj.HoTen = name;
                        DataProvider.Entities.SaveChanges();
                        return Json(new JsonPostBack("Cập nhật thành công", true), JsonRequestBehavior.AllowGet);

                    }
                }
                catch (Exception ex)
                {
                    return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetObjToUpdateProfile(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    int idUs = -1;
                    int.TryParse(id, out idUs);
                    if (idUs > 0)
                    {
                        NguoiDung obj = DataProvider.Entities.NguoiDungs.Where(p => p.Id == idUs).First();
                        //Tạo đối tượng trả về 
                        NguoiDung objReturn = new NguoiDung();
                        objReturn.Id = obj.Id;
                        
                        objReturn.HoTen = obj.HoTen;
                        objReturn.DiaChi = obj.DiaChi;
                        objReturn.Email = obj.Email;
                        objReturn.DienThoai = obj.DienThoai;
                        return Json(objReturn, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(new NguoiDung(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Add()
        {
            try
            {
                int id = int.Parse(Request["isInsert"]);
                string username = Request["username"];
                string pwd = Request["pwd"];
                string emdil = Request["email"];
                string sdt = Request["sdt"];
                string diaChi = Request["diachi"];

                int gate = 0;
                if (!string.IsNullOrEmpty(Request["gate"]))
                {
                    gate = Convert.ToInt32(Request["gate"]);
                }
                int vaitroud = 0;
                if (!string.IsNullOrEmpty(Request["vaitroid"]))
                {
                    vaitroud = Convert.ToInt32(Request["vaitroid"]);
                }
                string name = Request["name"];
                if (id < 0)//Add
                {
                    if (getObjByUser(username).Id > 0) //Nếu tài khoản đã tồn tại
                    {
                        return Json(new JsonPostBack("Tên tài khoản đã tồn tại. Vui lòng nhập tên tài khoản khác", false), JsonRequestBehavior.AllowGet);
                    }

                    NguoiDung obj = new NguoiDung();
                    obj.TenDangNhap = username;
                    obj.NgayTao = DateTime.Now;
                    obj.MatKhau = QuanTri.ToMD5(pwd);
                    obj.Email = emdil;
                    obj.DienThoai = sdt;
                    obj.DiaChi = diaChi;
                    obj.VaiTroId = vaitroud;
                    obj.HoTen = name;
                    DataProvider.Entities.NguoiDungs.Add(obj);
                    DataProvider.Entities.SaveChanges();
                    return Json(new JsonPostBack("Thêm mới thành công", true), JsonRequestBehavior.AllowGet);

                }
                else //Edit
                {
                    NguoiDung obj = DataProvider.Entities.NguoiDungs.Find(id);
                    if (obj.Id > 0)
                    {
                        //Nếu tên tài khoản thay đổi
                        if (obj.TenDangNhap != username)
                        {
                            //if username đã tồn tại => Erorr
                            if (getObjByUser(username).Id > 0)
                            {
                                return Json(new JsonPostBack("Tên tài khoản đã tồn tại. Vui lòng nhập tên tài khoản khác", false), JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                //Kiểm tra nếu mật khẩu thay đổi thì mã hóa md5
                                if (!(string.Compare(pwd, "Coder!(((23") == 0))
                                {
                                    obj.MatKhau = QuanTri.ToMD5(pwd);
                                }
                                obj.TenDangNhap = username;
                                obj.Email = emdil;
                                obj.DienThoai = sdt;
                                obj.DiaChi = diaChi;
                                obj.VaiTroId = vaitroud;
                                obj.HoTen = name;
                                DataProvider.Entities.SaveChanges();
                                return Json(new JsonPostBack("Cập nhật thành công", true), JsonRequestBehavior.AllowGet);
                            }
                        }
                        else //Tài khoản không đổi
                        {
                            if (!(string.Compare(pwd, "Coder!(((23") == 0))
                            {
                                obj.MatKhau = QuanTri.ToMD5(pwd);
                            }
                            obj.TenDangNhap = username;
                            obj.Email = emdil;
                            obj.DienThoai = sdt;
                            obj.DiaChi = diaChi;
                            obj.VaiTroId = vaitroud;
                            obj.HoTen = name;

                            DataProvider.Entities.SaveChanges();

                            return Json(new JsonPostBack("Cập nhật thành công", true), JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
                    }
                    
                }


            }
            catch(Exception ex)
            {

                return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý. Chi tiết: " + ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyNguoiDung, Action = CommonAdmin.ConstantCommon.Action.Edit)]
        /// <summary>
        /// hàm lấy thông tin để đưa lên modal sửa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetObjToEdit(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    int idUs = -1;
                    int.TryParse(id, out idUs);
                    if(idUs>0)
                    {
                        NguoiDung obj = DataProvider.Entities.NguoiDungs.Where(p => p.Id == idUs).First();
                        //Tạo đối tượng trả về 
                        NguoiDung objReturn = new NguoiDung();
                        objReturn.Id = obj.Id;
                        objReturn.TenDangNhap = obj.TenDangNhap;
                        objReturn.MatKhau = obj.MatKhau;
                        objReturn.HoTen = obj.HoTen;
                        objReturn.DiaChi = obj.DiaChi;
                        objReturn.Email = obj.Email;
                        objReturn.DienThoai = obj.DienThoai;
                        objReturn.VaiTroId = obj.VaiTroId;
                        return Json(objReturn, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(new NguoiDung(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
            }
        }
    }
}