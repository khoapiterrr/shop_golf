namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhatKyHeThong")]
    public partial class NhatKyHeThong
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        public int? HanhDongId { get; set; }

        [StringLength(50)]
        public string FormName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public int? NguoiDungId { get; set; }

        [StringLength(50)]
        public string DiaChiMayTinh { get; set; }

        public virtual HanhDong HanhDong { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
