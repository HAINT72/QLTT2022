namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tThongSo")]
    public partial class tThongSo
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string KH { get; set; }

        public string NOIDUNG { get; set; }

        [Required]
        public string STR_DATA { get; set; }

        [Required]
        public string STR_PATH { get; set; }

        public bool? bHIEULUC { get; set; }
    }
}
