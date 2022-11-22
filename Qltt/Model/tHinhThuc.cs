namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tHinhThuc")]
    public partial class tHinhThuc
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string KH { get; set; }

        [Required]
        public string TENHTHD { get; set; }
    }
}
