using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford_ShopGolf.Areas.Admin.Models;
using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class VaiTroController : AdminBaseController
    {
        // GET: VaiTro
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyVaiTro, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index()
        {
            return View();
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyVaiTro, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult LoadTable()
        {
            return View(DataProvider.Entities.VaiTroes.ToList());
        }

        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyVaiTro, Action = CommonAdmin.ConstantCommon.Action.Edit)]
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
                    if (idUs > 0)
                    {
                        VaiTro obj = DataProvider.Entities.VaiTroes.Where(p => p.Id == idUs).First();
                        //Tạo đối tượng trả về 
                        VaiTro objReturn = new VaiTro();
                        objReturn.Id = obj.Id;
                        objReturn.TenVaiTro = obj.TenVaiTro;
                        objReturn.MoTa = obj.MoTa;
                        return Json(objReturn, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(new VaiTro(), JsonRequestBehavior.AllowGet);
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
                string vaitro = Request["vaitro"];
                string mota = Request["mota"];
                if (id < 0)//Add
                {
                    if (getObjByVaiTro(vaitro).Id > 0) //Nếu tài khoản đã tồn tại
                    {
                        return Json(new JsonPostBack("Tên vai trò đã tồn tại. Vui lòng nhập tên tên vai trò khác", false), JsonRequestBehavior.AllowGet);
                    }

                    VaiTro obj = new VaiTro();
                    obj.TenVaiTro = vaitro;
                    obj.MoTa = mota;
                    DataProvider.Entities.VaiTroes.Add(obj);
                    DataProvider.Entities.SaveChanges();
                    QuanTri.SaveLog("Thêm mới vai trò ", "Quản lý vai trò", (int)CommonAdmin.ConstantCommon.Action.Add);
                    return Json(new JsonPostBack("Thêm mới thành công", true), JsonRequestBehavior.AllowGet);

                }
                else //Edit
                {
                    VaiTro obj = DataProvider.Entities.VaiTroes.Find(id);
                    if (obj.Id > 0)
                    {
                        //Nếu tên tên vai trò thay đổi
                        if (obj.TenVaiTro != vaitro)
                        {
                            //if tên vai trò đã tồn tại => Erorr
                            if (getObjByVaiTro(vaitro).Id > 0)
                            {
                                return Json(new JsonPostBack("Tên vai trò đã tồn tại. Vui lòng nhập tên tên vai trò khác", false), JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                obj.TenVaiTro = vaitro;
                                obj.MoTa = mota;
                                DataProvider.Entities.SaveChanges();
                                QuanTri.SaveLog("Chỉnh sửa vai trò ", "Quản lý vai trò", (int)CommonAdmin.ConstantCommon.Action.Edit);
                                return Json(new JsonPostBack("Cập nhật thành công", true), JsonRequestBehavior.AllowGet);
                            }
                        }
                        else //Tài khoản không đổi
                        {
                            obj.TenVaiTro = vaitro;
                            obj.MoTa = mota;
                            DataProvider.Entities.SaveChanges();
                            QuanTri.SaveLog("Chỉnh sửa vai trò ", "Quản lý vai trò", (int)CommonAdmin.ConstantCommon.Action.Edit);
                            return Json(new JsonPostBack("Cập nhật thành công", true), JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception ex)
            {

                return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
            }
        }

        public VaiTro getObjByVaiTro(string vt)
        {
            try
            {
                return DataProvider.Entities.VaiTroes.Where(x => String.Compare(vt,x.TenVaiTro,true)==0).First();
            }
            catch (Exception)
            {
                return new VaiTro();
            }
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyVaiTro, Action = CommonAdmin.ConstantCommon.Action.Delete)]
        public JsonResult delete()
        {
            try
            {
                int id = -1;
                int.TryParse(Request["id"], out id);
                if (id > 0)
                {
                    var obj = DataProvider.Entities.VaiTroes.FirstOrDefault(x => x.Id == id);
                    if(obj.NguoiDungs.Count >0 || obj.PhanQuyens.Count>0)
                    {
                        return Json(new JsonPostBack("Không thể xóa vai trò này, vì sẽ ảnh hưởng đến dữ liệu khác", false), JsonRequestBehavior.AllowGet);
                    }
                    DataProvider.Entities.VaiTroes.Remove(obj);
                    QuanTri.SaveLog("Xóa vai trò ", "Quản lý vai trò", (int)CommonAdmin.ConstantCommon.Action.Edit);
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