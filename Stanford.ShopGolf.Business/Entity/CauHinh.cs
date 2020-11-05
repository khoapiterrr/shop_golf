namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CauHinh")]
    public partial class CauHinh
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Logo { get; set; }

        [StringLength(100)]
        public string Slogan { get; set; }

        [StringLength(250)]
        public string CompanyName { get; set; }

        [StringLength(300)]
        public string DiaChi { get; set; }

        [StringLength(150)]
        public string DienThoai { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(250)]
        public string Website { get; set; }

        [Column(TypeName = "ntext")]
        public string GioiThieu { get; set; }

        [Column(TypeName = "ntext")]
        public string LienHe { get; set; }

        [Column(TypeName = "ntext")]
        public string ChinhSach { get; set; }
    }
}
