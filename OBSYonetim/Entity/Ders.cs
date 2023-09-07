using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSYonetim.Entity
{
    public class Ders
    {
        [Key]
        public int DersID { get; set; }
        public string DersCode { get; set; }
        public string DersAd { get; set; }
        public int Yil { get; set; }
        public int Yariyil { get; set; }
        public int BolumID { get; set; }
        public int? OgretmenID { get; set; }

        public  virtual Ogretmen Ogretmen { get; set; }

        public virtual Bolum Bolum { get; set; }
       
    }
}
