namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tHopDong")]
    public partial class tHopDong
    {
        public int ID { get; set; }

        [Required]
        public string SOHD { get; set; }

        public int ID_GT { get; set; }

        public int ID_NT { get; set; }

        public int ID_CP { get; set; }

        public int ID_HT { get; set; }

        public int? ID_HD_GOC { get; set; }

        public int? ID_HD_CHINH { get; set; }

        public string NOIDUNG { get; set; }

        public double GIAHD { get; set; }

        public DateTime NGAYHD { get; set; }

        public DateTime NGAYKC { get; set; }

        public DateTime NGAYKT { get; set; }

        public DateTime? NGAYBLTU { get; set; }

        public DateTime? NGAYBLHD { get; set; }

        public double? TLTAMUNG { get; set; }

        public double? TLBLHD { get; set; }

        public double? TLQT { get; set; }

        public string FILEPDF { get; set; }

        public string FILEOFFICE { get; set; }

        public bool bTHANHLY { get; set; }

        public bool bQUYETTOAN { get; set; }

        public string GHICHU { get; set; }
    }
}
