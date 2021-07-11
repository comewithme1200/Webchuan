using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace luyentap1.Models.Entities
{
    public partial class NguyenVuLong_dbContext : DbContext
    {
        public NguyenVuLong_dbContext()
            : base("name=NguyenVuLong_dbContext")
        {
        }

        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<LoaiHang> LoaiHangs { get; set; }
        public virtual DbSet<tblAccount> tblAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HangHoa>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<LoaiHang>()
                .HasMany(e => e.HangHoas)
                .WithRequired(e => e.LoaiHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAccount>()
                .Property(e => e.Username)
                .IsFixedLength();

            modelBuilder.Entity<tblAccount>()
                .Property(e => e.Password)
                .IsFixedLength();
        }
    }
}
