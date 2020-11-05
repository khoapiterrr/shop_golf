namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SanPham_Flex
    {
        public int Id { get; set; }

        public int? SanPhamId { get; set; }

        public int? FlexId { get; set; }

        public double? GiaBan { get; set; }

        public double? GiaGiam { get; set; }

        public double? PhanTramGiam { get; set; }

        public virtual Flex Flex { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
