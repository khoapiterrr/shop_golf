using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Areas.Admin.Models
{
	public class SanPhamSearchModel
	{
		public string q { get; set; }
		public int? pageSize { get; set; }
		public int? page { get; set; }
		public int? ddlChuDe { get; set; }
		public int? ddlLoai { get; set; }
		public int? ddlNhanHieu { get; set; }
		public int? ddlGioiTinh { get; set; }
		public double? giatu { get; set; }
		public double? giaden { get; set; }
	}
}