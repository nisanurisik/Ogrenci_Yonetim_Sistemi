using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSYonetim.Entity
{
    public class Bolum
    {   [Key]
        public int BolumID { get; set; }
        public string BolumAd { get; set; }
        public virtual ICollection<Ders> Dersler { get; set; }
        public virtual ICollection<Ogrenci> Ogrenciler { get; set; }

        public virtual ICollection<Ogretmen> Ogretmenler { get; set; }
    }
}
