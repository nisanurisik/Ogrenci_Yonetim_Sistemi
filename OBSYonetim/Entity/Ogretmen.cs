using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSYonetim.Entity
{
    public class Ogretmen
    {
        [Key]
        public int OgretmenID { get; set; }
        public string OgretmenAd { get; set; }
        public string OgretmenSoyAd { get; set; }
        public string OgretmenSifre { get; set; }
        public string OgretmenTel { get; set; }
        public int? BolumID { get; set; }
        public virtual Bolum Bolum { get; set; }

        public virtual List<Ders> Dersler { get; set; }

    }
}
