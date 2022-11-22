namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tNhanVien")]
    public partial class tNhanVien
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string USERNAME { get; set; }

        [Required]
        public string HOTEN { get; set; }

        [Required]
        [StringLength(2)]
        public string QUYEN { get; set; }

        [Required]
        public string MATKHAU { get; set; }

        public bool? bHIEULUC { get; set; }
    }
}
