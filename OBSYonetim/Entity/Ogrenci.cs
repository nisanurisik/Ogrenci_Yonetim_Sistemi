using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSYonetim.Entity
{
    
    public class Ogrenci
    {
        [Key]
        public int ID { get; set; }
        public string OgrenciAd { get; set; }
        public string OgrenciSoyad { get; set; }
        
        [Required]
        [Column(TypeName = "Varchar")]
        [StringLength(12)]
        public string OgrenciNo { get; set; }

        public int yariyil { get; set; }
        public string Sifre { get; set; }
        public int Sinif { get; set; }
        public int BolumID { get; set; }
        public virtual Bolum Bolum { get; set; }
        public int? OgretmenID { get; set; }
        
        public virtual Ogretmen Danisman { get; set; }
        public ICollection<OgrenciNot> Notlar { get; set; }
        public ICollection<OgrenciDers> OgrenciDers { get; set; }


    }
}
