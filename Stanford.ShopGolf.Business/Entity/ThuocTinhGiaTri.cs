namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuocTinhGiaTri")]
    public partial class ThuocTinhGiaTri
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string GiaTri { get; set; }

        public int? ThuocTinhId { get; set; }

        public virtual ThuocTinhSanPham ThuocTinhSanPham { get; set; }
    }
}
