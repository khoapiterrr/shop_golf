namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinhThanh")]
    public partial class TinhThanh
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string MaTinh { get; set; }

        [StringLength(50)]
        public string TenTinh { get; set; }

        public int? MaBuuDien { get; set; }

        public int? QuocGiaId { get; set; }

        public int? SapXep { get; set; }

        public virtual QuocGia QuocGia { get; set; }
    }
}
