namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tThanhToan")]
    public partial class tThanhToan
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string SOPTT { get; set; }

        public DateTime NGAYPTT { get; set; }

        public double GTHT { get; set; }

        public double GTPS { get; set; }

        public double GTKTTU { get; set; }

        public double GTKTQT { get; set; }

        public double GTKTKH { get; set; }

        public double GTTT { get; set; }

        public double GTGN { get; set; }

        public int ID_HD { get; set; }

        public int ID_NV { get; set; }

        public string FILEPDF { get; set; }

        public string FILEOFFICE { get; set; }

        public string GHICHU { get; set; }
    }
}
