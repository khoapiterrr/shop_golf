using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Areas.Admin.Models
{
    public class KiemTraQuyenChucNang
    {
        public bool isThem { get; set; }

        public bool isSua { get; set; }

        public bool isXoa { get; set; }

        public bool isXem { get; set; }

        public KiemTraQuyenChucNang()
        {

        }

        private static KiemTraQuyenChucNang _Instance = new KiemTraQuyenChucNang();

        public static KiemTraQuyenChucNang Instance
        {
            get { return KiemTraQuyenChucNang._Instance; }
            set { KiemTraQuyenChucNang._Instance = value; }
        }

        //Kiểm tra quyền: thêm, sửa, xóa
        public void CheckPermission(string formName)
        {
            NguoiDung objUser = null;
            objUser = HttpContext.Current.Session[CommonAdmin.ConstantData.USER_SESSION] as NguoiDung;
            if (objUser != null)
            {
                //Lấy đối tượng chức năng theo tên form
                ChucNang objChucNang = DataProvider.Entities.ChucNangs.Where(p => p.TenForm.Equals(formName)).First();
                //Nếu tồn tại chức năng
                if(objChucNang != null)
                {
                    //Lấy vai trò id
                    int vaiTroId = objUser.VaiTroId.HasValue ? objUser.VaiTroId.Value : 0;

                    int chucNangId = objChucNang.Id;

                    //Lấy quyền của chức năng tương ứng với chức năng, vai trò
                    PhanQuyen objQuyen = DataProvider.Entities.PhanQuyens.Where(p => p.VaiTroId == vaiTroId && p.ChucNangId == chucNangId).First();

                    if(objQuyen != null)
                    {
                        isXem = objQuyen.Xem.HasValue ? objQuyen.Xem.Value : false;
                        isThem = objQuyen.Them.HasValue ? objQuyen.Them.Value : false;
                        isSua = objQuyen.Sua.HasValue ? objQuyen.Sua.Value : false;
                        isXoa = objQuyen.Xoa.HasValue ? objQuyen.Xoa.Value : false;
                    }
                }
            }

        }

        public bool CheckQuyenDaCap(int vaitroId, int idchacnang)
        {
            var lstDaCapQuyen = DataProvider.Entities.PhanQuyens.Where(x => x.VaiTroId == vaitroId).ToList();
            int dem = lstDaCapQuyen.Count;
            for (int i = 0; i < dem; i++)
            {
                if (idchacnang == lstDaCapQuyen[i].ChucNangId)
                {
                    return true;
                }
            }
            return false;
        }

    }


}