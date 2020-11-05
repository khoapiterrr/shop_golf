using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Areas.Admin.CommonAdmin
{
    public class ConstantData
    {
        public static int PAGE_SIZE = 3;

        public static class ChucNangPhanMem
        {
            public const string QuanLyVaiTro = "QuanLyVaiTro";
            public const string AnhSanPham = "AnhSanPham";
            public const string QuanLyCoLor = "QuanLyCoLor";
            public const string QuanLyphanQuyen = "QuanLyphanQuyen";
            public const string QuanLyNhatKyHeThong = "QuanLyNhatKyHeThong";
            public const string QuanLyNguoiDung = "QuanLyNguoiDung";
            public const string QuanLySanPham = "QuanLySanPham";
            public const string QuanLyThuongHieu = "QuanLyThuongHieu";
            public const string QuanLyChuDe = "QuanLyChuDe";
            public const string ChuongTrinhUuDai = "ChuongTrinhUuDai";

            //public const string QuanLyMau = "QuanLyMau";
            public const string QuanLyFlex = "QuanLyFlex";

            public const string QuanLyHoaDonBan = "QuanLyHoaDonBan";
            public const string QuanLyHoaDonChiTiet = "QuanLyHoaDonChiTiet";
            public const string QuanLyInseam = "QuanLyInseam";
            public const string QuanLyKhachHang = "QuanLyKhachHang";
            public const string QuanLyLengthSize = "QuanLyLengthSize";
            public const string LoaiKhachHang = "LoaiKhachHang";
            public const string LoaiSanPham = "LoaiSanPham";
            public const string QuanLyLoft = "QuanLyLoft";
            public const string QuanLyQuocGia = "QuanLyQuocGia";
            public const string QuanLyShaft = "QuanLyShaft";
        }

        //Biến lưu session đối tượng người dùng
        public const string USER_SESSION = "USER_SESSION";

        public const string USER_COOKIES = "USER_COOKIES";
        public const string USERNAME_COOKIES = "USERNAME_COOKIES";
        public const string PASSWORD_COOKIES = "PASSWORD_COOKIES";
        public const string REMEMBERME_COOKIES = "REMEMBERME_COOKIES";

        public static class ResponseMessage
        {
            public const string Error = "Có lỗi xảy ra trong quá trình xử lý";
            public const string ErrorDelete = "Không thể xóa đối tượng này. Vì sẽ ảnh hưởng đến dữ liệu khác";
            public const string SuccessCreate = "Thêm mới thành công";
            public const string SuccessUpdate = "Cập nhật thành công";
            public const string SuccessDelete = "Xóa thành công";
            public const string SuccessGet = "Lấy dữ liệu thành công";
            public const string NotFound = "Không tìm thấy dữ liệu";
            public const string SuccessFile = "Upfile thành công";
            public const string ErrorFile = "Upfile thất bại";
        }
    }
}