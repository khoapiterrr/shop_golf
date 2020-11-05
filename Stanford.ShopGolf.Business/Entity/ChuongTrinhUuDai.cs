namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuongTrinhUuDai")]
    public partial class ChuongTrinhUuDai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongTrinhUuDai()
        {
            ChuongTrinhUuDai_SanPham = new HashSet<ChuongTrinhUuDai_SanPham>();
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string TenChuongTrinh { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? TuNgay { get; set; }

        public DateTime? DenNgay { get; set; }

        public int? NguoiTaoId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuongTrinhUuDai_SanPham> ChuongTrinhUuDai_SanPham { get; set; }
    }
}
