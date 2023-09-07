using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSYonetim.Entity
{
    public class Ayarlar
    {
        [Key]
        public int ID { get; set; }
        public int SistemYariYil { get; set; }
        public string SistemYil { get; set; }
    }
}
