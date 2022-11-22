namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tKeHoach")]
    public partial class tKeHoach
    {
        public int ID { get; set; }

        [Required]
        public string TENKH { get; set; }

        public double GIATRI { get; set; }

        public string GHICHU { get; set; }
    }
}
