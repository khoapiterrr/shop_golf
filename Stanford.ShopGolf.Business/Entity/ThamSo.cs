namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThamSo")]
    public partial class ThamSo
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TenThamSo { get; set; }

        [StringLength(500)]
        public string GiaTri { get; set; }
    }
}
