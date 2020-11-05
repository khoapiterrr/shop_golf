namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string TieuDe { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        [StringLength(30)]
        public string TacGia { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? NguoiTaoId { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public DateTime? NgayDuyet { get; set; }

        public bool? DaDuyet { get; set; }

        public bool? DaXoa { get; set; }

        public int? ChuDeId { get; set; }

        public virtual ChuDeTin ChuDeTin { get; set; }
    }
}
