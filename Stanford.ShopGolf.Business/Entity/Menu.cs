namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TenItem { get; set; }

        [StringLength(250)]
        public string MoTa { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public int? ChuDeId { get; set; }

        public int? ParentId { get; set; }

        public int? SapXep { get; set; }

        [StringLength(50)]
        public string ImagePath { get; set; }

        [StringLength(50)]
        public string HeadTitle { get; set; }

    }
}
