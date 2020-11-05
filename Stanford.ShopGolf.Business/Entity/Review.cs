namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int Id { get; set; }

        [StringLength(1000)]
        public string NoiDung { get; set; }

        public double? ReviewCount { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? SanPhamId { get; set; }

        public int? NguoiDungId { get; set; }
		public string TieuDe { get; set; }
		public virtual SanPham SanPham { get; set; }
    }
}
