namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SanPham_Inseam
    {
        public int Id { get; set; }

        public int? SanPhamId { get; set; }

        public int? InseamId { get; set; }

        public double? GiaBan { get; set; }

        public double? GiaGiam { get; set; }

        public double? PhanTramGiam { get; set; }

        public virtual Inseam Inseam { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
