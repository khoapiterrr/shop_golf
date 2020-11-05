namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnhSanPham")]
    public partial class AnhSanPham
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string TenAnh { get; set; }

        [StringLength(300)]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string Avatar { get; set; }

        public int? ColorId { get; set; }

        public int? SanPhamId { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
