namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tChiPhi")]
    public partial class tChiPhi
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string KH { get; set; }

        [Required]
        public string TENCP { get; set; }
    }
}
