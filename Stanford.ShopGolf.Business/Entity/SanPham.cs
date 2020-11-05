namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            AnhSanPhams = new HashSet<AnhSanPham>();
            ChuongTrinhUuDai_SanPham = new HashSet<ChuongTrinhUuDai_SanPham>();
            HoaDonChiTiets = new HashSet<HoaDonChiTiet>();
            Reviews = new HashSet<Review>();
            SanPham_Bounce = new HashSet<SanPham_Bounce>();
            SanPham_Color = new HashSet<SanPham_Color>();
            SanPham_Flex = new HashSet<SanPham_Flex>();
            SanPham_Inseam = new HashSet<SanPham_Inseam>();
            SanPham_LengthSize = new HashSet<SanPham_LengthSize>();
            SanPham_Loft = new HashSet<SanPham_Loft>();
            SanPham_Shaft = new HashSet<SanPham_Shaft>();
            SanPham_Size = new HashSet<SanPham_Size>();
            ThuocTinhSanPhams = new HashSet<ThuocTinhSanPham>();
            SanPham_Waist = new HashSet<SanPham_Waist>();
            SanPham_Width = new HashSet<SanPham_Width>();
        }

        public int Id { get; set; }

        [StringLength(20)]
        public string MaSanPham { get; set; }

        [StringLength(250)]
        public string TenSanPham { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        public double? GiaBan { get; set; }

        public double? PhanTramGiam { get; set; }

        public double? GiaGiam { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(50)]
        public string AnhDaiDien { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public DateTime? NgayDuyet { get; set; }

        public bool? DaDuyet { get; set; }

        public int? NguoiTaoId { get; set; }

        public int? ChuDeId { get; set; }

        public int? LoaiId { get; set; }

        [StringLength(50)]
        public string RightLeftHand { get; set; }

        public int? TrangThaiId { get; set; }

        public int? QuocGiaId { get; set; }

        public double? Review { get; set; }

        public bool? DaXoa { get; set; }

        public bool? Sale { get; set; }

        public int? BrandId { get; set; }

        public int? GenderId { get; set; }

        public string GenderName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnhSanPham> AnhSanPhams { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual ChuDe ChuDe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuongTrinhUuDai_SanPham> ChuongTrinhUuDai_SanPham { get; set; }

        public virtual Gender Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual QuocGia QuocGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Bounce> SanPham_Bounce { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Color> SanPham_Color { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Flex> SanPham_Flex { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Inseam> SanPham_Inseam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_LengthSize> SanPham_LengthSize { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Loft> SanPham_Loft { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Shaft> SanPham_Shaft { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Size> SanPham_Size { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Waist> SanPham_Waist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Width> SanPham_Width { get; set; }

        public virtual TrangThai TrangThai { get; set; }
    }
}
