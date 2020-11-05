﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using System.Text;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using Stanford_ShopGolf.Areas.Admin.Models;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class PhanQuyenController : AdminBaseController
    {
        
        // GET: PhanQuyen
        //public ActionResult Index()
        //{
        //    LoadViewBag();
        //    return View();
        //}
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyphanQuyen, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult LoadTable()
        {
            
            return View(DataProvider.Entities.PhanQuyens.ToList());
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyphanQuyen, Action = CommonAdmin.ConstantCommon.Action.View)]
        public ActionResult Index(string ddlVaiTro)
        {
            //Load selected list vai trò
            LoadViewBag();
            //Kiểm tra vai trò
            if(string.IsNullOrEmpty(ddlVaiTro))
            {
                return View();
            }
            int vaitroId = -1;
            int.TryParse(ddlVaiTro, out vaitroId);
            List<PhanQuyenModel> lst = new List<PhanQuyenModel>();
            if (vaitroId >0)
            {
                //Lấy tất cả chức năng
                var lstChucNang = DataProvider.Entities.ChucNangs.ToList();
                
                foreach (var item in lstChucNang)
                {
                    PhanQuyenModel obj = new PhanQuyenModel();
                    if (CheckQuyenDaCap(vaitroId, item.Id))  //chức năng đã đc cấp
                    {
                        obj.Id = item.Id;
                        obj.TenChucNang = item.TenChucNang;
                        obj.CapChucNang = true;
                        lst.Add(obj);
                    }
                    else //chức năng chưa được cấp
                    {
                        obj.Id = item.Id;
                        obj.TenChucNang = item.TenChucNang;
                        obj.CapChucNang = false;
                        lst.Add(obj);
                    }
                    
                }
                                
            }
            
            return View(lst);
        }
        /// <summary>
        /// Dựa vào vai trò, lấy hành động xem, xóa ,sửa,.. khi load trang
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult getActionByVaiTro(string id)
        {
            int vaitroId = -1;
            int.TryParse(id, out vaitroId);
            if (vaitroId > 0)
            {
                //Lấy tất cả chức năng
                var obj = DataProvider.Entities.PhanQuyens.Where(x => x.VaiTroId == vaitroId).First();
                return Json(new { error = false, xem=obj.Xem, sua = obj.Sua,xoa=obj.Xoa,them=obj.Them,baocao=obj.BaoCao }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get obj phân quyền đưa lên modal edit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vaitroid"></param>
        /// <returns></returns>
        public ActionResult GetObjToEdit(string id,string vaitroid)
        {
            try
            {
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(vaitroid))
                {
                    int idcn = -1; //id phân quyền
                    int idvaitro = -1;//id vai trò
                    int.TryParse(id, out idcn);
                    int.TryParse(vaitroid, out idvaitro);
                    if (idcn > 0 && idvaitro>0)
                    {
                        PhanQuyen obj = DataProvider.Entities.PhanQuyens.Where(p => p.VaiTroId == idvaitro && p.ChucNangId == idcn).First();
                        //Tạo đối tượng trả về 
                        PhanQuyen objReturn = new PhanQuyen();
                        string tenvaitro = obj.VaiTro.TenVaiTro;
                        return Json(new { tenvaitro = obj.VaiTro.TenVaiTro,chucnangthem = obj.Them, chucnangsua = obj.Sua, chucnangxoa = obj.Xoa,chucnangbaocao = obj.BaoCao,chucnangxem=obj.Xem }, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(new VaiTro(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new JsonPostBack("Có lỗi xảy ra", false), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Kiểm tra xem chức nang đã được cấp quyền chưa
        /// </summary>
        /// <param name="vaitroId"> vai trò Id</param>
        /// <param name="idchacnang"> chức năng cần kiểm tra</param>
        /// <returns></returns>
        private bool CheckQuyenDaCap(int vaitroId,int idchacnang)
        {
            var lstDaCapQuyen = DataProvider.Entities.PhanQuyens.Where(x => x.VaiTroId == vaitroId).ToList();
            int dem = lstDaCapQuyen.Count;
            for (int i = 0; i < dem; i++)
            {
                if(idchacnang == lstDaCapQuyen[i].ChucNangId)
                {
                    return true;
                }
            }
            return false;
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyphanQuyen, Action = CommonAdmin.ConstantCommon.Action.Delete)]
        /// <summary>
        /// Xóa tất cả phần quyền theo chức năng
        /// </summary>
        /// <param name="vaitroid"> chức năng id</param>
        /// <returns></returns>
        private bool DeleteAllByVaiTroId(int vaitroid)
        {
            try
            {
                var lstObj = DataProvider.Entities.PhanQuyens.Where(x => x.VaiTroId == vaitroid);
                DataProvider.Entities.PhanQuyens.RemoveRange(lstObj);
                DataProvider.Entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyphanQuyen, Action = CommonAdmin.ConstantCommon.Action.Add)]
        /// <summary>
        /// Thêm tất cả các chức năng 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public JsonResult AddAll(string id)
        {
            int vaitroId = -1;
            bool xem = Convert.ToBoolean(Request["xem"]);
            bool xoa = Convert.ToBoolean(Request["xoa"]);
            bool sua = Convert.ToBoolean(Request["sua"]);
            bool them = Convert.ToBoolean(Request["them"]);
            bool baocao = Convert.ToBoolean(Request["baocao"]);
            try
            {
                int.TryParse(id, out vaitroId);
                if (vaitroId > 0)
                {
                    DeleteAllByVaiTroId(vaitroId);

                    var lstChucNang = DataProvider.Entities.ChucNangs.ToList();
                    foreach (var item in lstChucNang)
                    {
                        PhanQuyen obj = new PhanQuyen();
                        obj.ChucNangId = item.Id;
                        obj.VaiTroId = vaitroId;
                        obj.Xem = xem;
                        obj.Sua = sua;
                        obj.Xoa = xoa;
                        obj.Them = them;
                        obj.BaoCao = baocao;
                        DataProvider.Entities.PhanQuyens.Add(obj);
                    }
                    DataProvider.Entities.SaveChanges();
                    QuanTri.SaveLog("Cấp quyền và chức năng", "Phân quyền", (int)CommonAdmin.ConstantCommon.Action.Add);
                    return Json(new JsonPostBack("Xóa thành công", true), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new JsonPostBack("Xóa thành công", false), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonPostBack("Xóa thành công", false), JsonRequestBehavior.AllowGet);
        }

        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyphanQuyen, Action = CommonAdmin.ConstantCommon.Action.Add)]
        /// <summary>
        /// Thêm mới các chức năng theo Id truyền vào
        /// </summary>
        /// <param name="id">vài trò id</param>
        /// <param name="itemId"> list chức năng id</param>
        /// <returns></returns>
        public JsonResult Add(string id, string itemId)
        {
            int vaitroId = -1;
            bool xem = Convert.ToBoolean(Request["xem"]);
            bool xoa = Convert.ToBoolean(Request["xoa"]);
            bool sua = Convert.ToBoolean(Request["sua"]);
            bool them = Convert.ToBoolean(Request["them"]);
            bool baocao = Convert.ToBoolean(Request["baocao"]);
            string[] lstIdChucNang;
            if (!string.IsNullOrEmpty(itemId))
            {
                lstIdChucNang = itemId.Split('-');
            }
            else
            {
                return Json(new JsonPostBack("Xóa thành công", false), JsonRequestBehavior.AllowGet);
            }
            try
            {
                int.TryParse(id, out vaitroId);
                if (vaitroId > 0)
                {
                    //Thêm mới quyền
                    foreach (var item in lstIdChucNang)
                    {
                        PhanQuyen obj = new PhanQuyen();
                        obj.ChucNangId = int.Parse(item);
                        obj.VaiTroId = vaitroId;
                        obj.Xem = xem;
                        obj.Sua = sua;
                        obj.Xoa = xoa;
                        obj.Them = them;
                        obj.BaoCao = baocao;
                        DataProvider.Entities.PhanQuyens.Add(obj);
                    }
                    DataProvider.Entities.SaveChanges();
                    QuanTri.SaveLog("Cấp quyền và chức năng", "Phân quyền", (int)CommonAdmin.ConstantCommon.Action.Add);
                    return Json(new JsonPostBack("Xóa thành công", true), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new JsonPostBack("Xóa thành công", false), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonPostBack("Xóa thành công", false), JsonRequestBehavior.AllowGet);
        }
        [KiemTraQuyen(PermissionName = CommonAdmin.ConstantData.ChucNangPhanMem.QuanLyphanQuyen, Action = CommonAdmin.ConstantCommon.Action.Delete)]
        /// <summary>
        /// Xóa quền theo list chức năng Id
        /// </summary>
        /// <param name="id"> vai trò id</param>
        /// <param name="itemId"> list chức năng Id</param>
        /// <returns></returns>
        public JsonResult Delete(string id, string itemId)
        {
            int vaitroId = -1;
            
            string[] lstIdChucNang;
            if (!string.IsNullOrEmpty(itemId))
            {
                lstIdChucNang = itemId.Split('-');
            }
            else
            {
                return Json(new JsonPostBack("Xóa thành công", false), JsonRequestBehavior.AllowGet);
            }
            try
            {
                int.TryParse(id, out vaitroId);
                if (vaitroId > 0)
                {

                    foreach (var item in lstIdChucNang)
                    {
                        int Idchucnang = int.Parse(item);
                        PhanQuyen obj = DataProvider.Entities.PhanQuyens.Where(x => x.ChucNangId == Idchucnang && x.VaiTroId == vaitroId).First();
                        DataProvider.Entities.PhanQuyens.Remove(obj);
                        
                    }
                    DataProvider.Entities.SaveChanges();
                    QuanTri.SaveLog("Cấp quyền và chức năng", "Phân quyền", (int)CommonAdmin.ConstantCommon.Action.Add);
                    return Json(new JsonPostBack("Xóa thành công", true), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new JsonPostBack("Xóa thất bại", false), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonPostBack("Xóa thất bại", false), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteAll(string id)
        {
            int itemId = -1;
            try
            {
                int.TryParse(id, out itemId);
                if (itemId > 0)
                {
                    DeleteAllByVaiTroId(itemId);
                    QuanTri.SaveLog("Xóa quyền và chức năng", "Phân quyền", (int)CommonAdmin.ConstantCommon.Action.Delete);
                    return Json(new JsonPostBack("Xóa thành công", true), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new JsonPostBack("Xóa thành công", false), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonPostBack("Xóa thành công", false), JsonRequestBehavior.AllowGet);
        }
        public void LoadViewBag()
        {
            var lstVaiTro = DataProvider.Entities.VaiTroes.ToList();
            ViewBag.ddlVaiTro = new SelectList(lstVaiTro, "Id", "TenVaiTro");

            List<ChucNang> lstchucnang = DataProvider.Entities.ChucNangs.ToList();
            //Láy chức năng
            StringBuilder sbd = new StringBuilder();
            for (int i = 0; i < lstchucnang.Count; i++)
            {
                sbd.Append("<option value="+ lstchucnang[i].Id+ ">" + lstchucnang[i].TenChucNang + "</option>");
            }

            ViewBag.ddlchucnang = sbd.ToString();
        }
       
       
        //public ActionResult danhsachquyen(string ddlVaiTro)
        //{
        //    int vaitroId = -1;
        //    int.TryParse(ddlVaiTro, out vaitroId);
        //    if(vaitroId > 0)
        //    {
        //        var lstPhanDaCap = DataProvider.Entities.PhanQuyens.ToList();
        //        var lstChucNang = DataProvider.Entities.ChucNangs.ToList();
        //        var lstObj = (from i in lstPhanDaCap
        //                      join j in lstChucNang on i.VaiTroId equals vaitroId
        //                      select new PhanQuyenModel()
        //                      {
        //                          CapChucNang = i.Id == j.Id ? "daduoccap" : "chuaduoccap",
        //                          Id = j.Id,
        //                          TenChucNang = j.TenChucNang

        //                      }).ToList();

        //        return RedirectToAction(lstObj);
        //    }

        //    return View();
        //}
        public JsonResult SaveQuyen()
        {
            try
            {
                int vaitroid = -1;
                int chucnangid = -1;
                if (!string.IsNullOrEmpty(Request["idvt"]))
                {
                    int.TryParse(Request["idvt"], out vaitroid);
                }
                if (!string.IsNullOrEmpty(Request["idcn"]))
                {
                    int.TryParse(Request["idcn"], out chucnangid);
                }
                bool xem = Convert.ToBoolean(Request["xem"]);
                bool xoa = Convert.ToBoolean(Request["xoa"]);
                bool sua = Convert.ToBoolean(Request["sua"]);
                bool them = Convert.ToBoolean(Request["them"]);
                bool baocao = Convert.ToBoolean(Request["baocao"]);
                if(vaitroid>= 0 && chucnangid > 0)
                {
                    var obj = DataProvider.Entities.PhanQuyens.Where(p => p.VaiTroId == vaitroid && p.ChucNangId == chucnangid).First();
                    obj.Xem = xem;
                    obj.Sua = sua;
                    obj.Xoa = xoa;
                    obj.BaoCao = baocao;
                    obj.Them = them;
                    DataProvider.Entities.SaveChanges();
                    return Json(new JsonPostBack("Cập nhật thành công", true), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new JsonPostBack("Xảy ra lỗi trong quá trình xử lý", false), JsonRequestBehavior.AllowGet);
            }
        }

        public List<PhanQuyen> getObjByChucNang(string cn)
        {
            try
            {
                int id = -1;
                int.TryParse(cn, out id);
                if(id>0)
                {
                    return DataProvider.Entities.PhanQuyens.Where(x => x.ChucNangId == id).ToList();
                }
                return new List<PhanQuyen>();
            }
            catch (Exception)
            {
                return new List<PhanQuyen>();
            }
        }


    }
}