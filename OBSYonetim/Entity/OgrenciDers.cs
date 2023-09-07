using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSYonetim.Entity
{
    public class OgrenciDers
    {
        [Key]
        public int OgrenciDersID { get; set; }
        public int OgrenciID { get; set; }
        public int DersID { get; set; }
        
        public int Statu { get; set; }
        public int? Kayityariyili { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }
        public virtual Ders Ders { get; set; }

        
        public ICollection<OgrenciNot> Notlar { get; set; }
    }
}
