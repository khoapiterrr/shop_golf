using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Areas.Admin.Models
{
    public class ChuongTinhUuDaiCreateDto
    {
        public List<int> lstProductCode { get; set; }
        public int CTUDCode { get; set; }
        public int Type { get; set; }
        public string Discount { get; set; }
    }
}