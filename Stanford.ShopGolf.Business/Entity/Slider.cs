namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string TenSlider { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string AnhDaiDien { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        [StringLength(50)]
        public string KieuHienThi { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTaoId { get; set; }

        public int? Level { get; set; }

        public bool? DaDuyet { get; set; }

        public int? SapXep { get; set; }
    }
}
