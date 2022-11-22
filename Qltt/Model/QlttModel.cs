namespace Model
{
    using System.Data.Entity;

    public partial class QlttModel : DbContext
    {
        public QlttModel()
            : base("name=QlttModel")
        {
        }

        public virtual DbSet<tChiPhi> tChiPhis { get; set; }
        public virtual DbSet<tGoiThau> tGoiThaus { get; set; }
        public virtual DbSet<tHinhThuc> tHinhThucs { get; set; }
        public virtual DbSet<tHopDong> tHopDongs { get; set; }
        public virtual DbSet<tKeHoach> tKeHoaches { get; set; }
        public virtual DbSet<tNguonVon> tNguonVons { get; set; }
        public virtual DbSet<tNhanVien> tNhanViens { get; set; }
        public virtual DbSet<tNhaThau> tNhaThaus { get; set; }
        public virtual DbSet<tThanhToan> tThanhToans { get; set; }
        public virtual DbSet<tThongSo> tThongSoes { get; set; }      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
