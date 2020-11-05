using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
namespace Stanford_ShopGolf.Areas.Admin.Models
{
    public class QuanTri
    {
        /// <summary>
        /// Hàm lấy địa chỉ ip của User
        /// </summary>
        /// <returns></returns>
        public static string getIpAccess()
        {
            try
            {
                IPHostEntry host;
                string localIP = "?";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString().Equals("InterNetwork"))
                    {
                        localIP = ip.ToString();
                    }
                }
                return localIP;
            }
            catch (Exception)
            {
                return "Không tìm thấy địa chỉ IP";
            }
        }

        /// <summary>
        /// Hàm mã hóa mật khẩu bằng thuật toán md5
        /// </summary>
        /// <param name="pwd"> password</param>
        /// <returns>password đã được mã hóa</returns>
        public static string ToMD5(string pwd)
        {
            
            string result = string.Empty;
            byte[] buffer = Encoding.UTF8.GetBytes(pwd);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (var item in buffer)
            {
                result += item.ToString("x2");
            }
            return result;
        }

        /// <summary>
        /// Lưu nhật kí hệ thống
        /// </summary>
        /// <param name="tenNhatKi"></param>
        /// <param name="MoTa"></param>
        /// <param name="TenChuNang"></param>
        /// <param name="HanhDong"></param>
        public static void SaveLog(string tenNhatKi, string TenChuNang, int hanhDong)
        {
            NhatKyHeThong obj = new NhatKyHeThong();
            try
            {
                var objUser = HttpContext.Current.Session[CommonAdmin.ConstantData.USER_SESSION] as NguoiDung;
                obj.MoTa = tenNhatKi;
                obj.NgayTao = DateTime.Now;
                obj.NguoiDungId = objUser.Id;
                obj.FormName = TenChuNang;
                obj.HanhDongId = hanhDong;
                obj.DiaChiMayTinh = QuanTri.getIpAccess();

                DataProvider.Entities.NhatKyHeThongs.Add(obj);
                DataProvider.Entities.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
        }



    }
}