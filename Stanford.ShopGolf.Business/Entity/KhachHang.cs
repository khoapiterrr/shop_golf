namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            HoaDonBans = new HashSet<HoaDonBan>();
        }

        public int Id { get; set; }

        [StringLength(20)]
		[Required(ErrorMessage ="Mã khách hàng không thể trống")]
        public string MaKH { get; set; }

        [StringLength(150)]
		[Required(ErrorMessage = "Tên khách hàng không thể trống")]
		public string TenKH { get; set; }

        [StringLength(20)]
        public string DienThoai { get; set; }

        [StringLength(50)]
		[Required(ErrorMessage = "Email không thể trống")]
		public string Email { get; set; }

        [StringLength(150)]
        public string DiaChi { get; set; }

        [StringLength(20)]
        public string MaSoThue { get; set; }

        [StringLength(20)]
        public string MaSoCongDan { get; set; }

        public int? TinhThanhId { get; set; }

        public int? LoaiId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonBan> HoaDonBans { get; set; }

        public virtual LoaiKhachHang LoaiKhachHang { get; set; }
    }
}
