namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

	[Table("HoaDonBan")]
	public partial class HoaDonBan
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public HoaDonBan()
		{
			HoaDonChiTiets = new HashSet<HoaDonChiTiet>();
		}

		public int Id { get; set; }

		[StringLength(500)]
		public string MoTa { get; set; }

		public DateTime? NgayTao { get; set; }
		[DisplayFormat(DataFormatString = "{0:F} đ", ApplyFormatInEditMode = true)]
		public double? TongTien { get; set; }

        public bool? DaThanhToan { get; set; }

        public int? KhachHangId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
