using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSYonetim.Entity
{
    public class Memur
    {
        [Key]
        public int ID { get; set; }
        public string MemurAdi { get; set; }
        public string sifre { get; set; }

        public string MemurSoyAdi { get; set; }
        public string KullaniciAdi { get; set; }
    }
}
