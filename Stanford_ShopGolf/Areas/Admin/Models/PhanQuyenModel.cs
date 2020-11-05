using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Areas.Admin.Models
{
    public class PhanQuyenModel
    {
        public int Id { get; set; }
        public string TenChucNang { get; set; }
        public bool CapChucNang { get; set; }
        public bool Xem { get; set; }

        public bool Them { get; set; }

        public bool Sua { get; set; }

        public bool Xoa { get; set; }

        public bool BaoCao { get; set; }

    }
}