using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Areas.Admin.Models
{
    public class CTUDViewModel
    {
        public int Id { get; set; }
        public string TenUuDai { get; set; }
        public string Time { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public int NguoiTao { get; set; }
        public string MoTa { get; set; }
    }
}