namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChuongTrinhUuDai_SanPham
    {
        public long Id { get; set; }

        public int? SanPhamId { get; set; }

        public int? ChuongTrinhId { get; set; }

        public double? GiaGiam { get; set; }

        public double? PhanTramGiam { get; set; }

        public virtual ChuongTrinhUuDai ChuongTrinhUuDai { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}