using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Areas.Admin.Models
{
    public class NhatKyModel
    {
        public int Id { get; set; }
        
        public string TenNhatKy { get; set; }
        
        public string MoTa { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTaoId { get; set; }

        public string TenNguoiTao { get; set; }

        public string TenChucNang { get; set; }
        
        public string HanhDong { get; set; }
        
        public string DiaChiIP { get; set; }
        
    }
}