namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tNguonVon")]
    public partial class tNguonVon
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string KH { get; set; }

        [Required]
        public string TENNV { get; set; }
    }
}
