using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSYonetim.Entity
{
    public class Context :DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            

            modelBuilder.Entity<Ogrenci>()
                .HasIndex(u => u.OgrenciNo)
                .IsUnique();

        }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        public DbSet<Ogretmen> Ogretmenler { get; set; }
        public DbSet<OgrenciNot> OgrenciNots { get; set; }
        public DbSet<OgrenciDers> OgrenciDersler { get; set; }
        public DbSet<Memur> Memurlar { get; set; }
        public DbSet<Ayarlar> Ayarlars { get; set; }


    }
}
