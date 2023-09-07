using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSYonetim.Entity
{
    public class OgrenciNot
    {
        [Key]
        public int OgrenciNotID { get; set; }     
        public decimal? Vize { get; set; }
        public decimal? Final { get; set; }
        public int OgrenciID { get; set; }
        public int OgrenciDersID { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }
        public virtual OgrenciDers OgrenciDers { get; set; }
    }
}
