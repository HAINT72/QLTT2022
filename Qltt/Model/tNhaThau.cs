namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tNhaThau")]
    public partial class tNhaThau
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string KH { get; set; }

        [Required]
        public string TENNT { get; set; }

        [Required]
        [StringLength(20)]
        public string MST { get; set; }

        [Required]
        public string DIACHI { get; set; }

        [Required]
        public string LIENHE { get; set; }

        public string GHICHU { get; set; }
    }
}
