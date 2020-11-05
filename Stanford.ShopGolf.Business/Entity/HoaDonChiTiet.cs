namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonChiTiet")]
    public partial class HoaDonChiTiet
    {
		public Guid guid { get; set; }
		public int Id { get; set; }

        public int? HoaDonId { get; set; }

        public int? SanPhamId { get; set; }

        public double? GiaBan { get; set; }

        public double? PhanTramGiam { get; set; }

        public double? GiaGiam { get; set; }

        public int? SoLuong { get; set; }

        public int? SizeId { get; set; }

        public int? WidthId { get; set; }

        public int? ColorId { get; set; }

        public int? WaistId { get; set; }

        public int? InseamId { get; set; }

        public int? BrandId { get; set; }

        [StringLength(50)]
        public string RightLeftHand { get; set; }

        public int? LoftId { get; set; }

        public int? FlexId { get; set; }

        public int? ShaftId { get; set; }

        public int? LengthSizeId { get; set; }

        public int? BounceId { get; set; }

        public int? GenderId { get; set; }

        public virtual HoaDonBan HoaDonBan { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
