using OBSYonetim.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OBSYonetim
{
    public partial class OgretmenSayfa : Form
    {
        Context db =new Context();  
        public OgretmenSayfa()
        {
            InitializeComponent();
        }
        public int ID;
        private void OgretmenSayfa_Load(object sender, EventArgs e)
        {
            OgrenciListele();
            var ogretmen = db.Ogretmenler.Where(x => x.OgretmenID == ID).FirstOrDefault();
            ogrtAd.Text = ogretmen.OgretmenAd;
            ogrtBolum.Text = ogretmen.Bolum.BolumAd;
            ogrtSoyad.Text = ogretmen.OgretmenSoyAd;
            ogrtTel.Text = ogretmen.OgretmenTel;
            NotlariListele();
            
            SeciliNot();
        }
        public void OgrenciListele() 
        {
            var ogrenciler = db.Ogrenciler.Where(x => x.OgretmenID == ID).Select(x => new ViewDanismanOgrenci { ogrenciAdı = x.OgrenciAd, ogrenciSoyadı = x.OgrenciSoyad, ogrenciBolum = x.Bolum.BolumAd, ogrenciID = x.ID, ogrenciyariyil = x.yariyil }).ToList();
            GridOgrenci.DataSource= ogrenciler;
        
        }
        public void OgrenciDersleriListele(int id)
        {
            
            var OgrenciDers = db.OgrenciDersler.Where(x => x.Statu == 1 && x.OgrenciID ==id).Select(x=>new ViewOgrenciDersler {dersAdı=x.Ders.DersAd,dersKodu=x.Ders.DersCode,ogrenciDersID=x.OgrenciDersID }).ToList();
            GridSecilenDers.DataSource= OgrenciDers;

        }
        public void NotlariListele()
        {
            //var OgrenciDers = db.OgrenciDersler.Where(x => x.Statu == 2 && x.Ders.OgretmenID == ID).Select(x => x.OgrenciDersID).ToList();
            //var notlar =db.OgrenciNots.Where(x=>x.OgrenciDersID==)
            var OgrenciDers = db.OgrenciDersler.Where(x => x.Statu == 2 && x.Ders.OgretmenID==ID ).Select(x => new ViewNot { notdersad = x.Ders.DersAd, notderscode = x.Ders.DersCode, notogrencidersID = x.OgrenciDersID,notogrenciID=x.OgrenciID ,notogrenciad=x.Ogrenci.OgrenciAd,notsoyad=x.Ogrenci.OgrenciSoyad,notfinal=x.Notlar.Where(y=>y.OgrenciDersID==x.OgrenciDersID).Select(y=>(decimal)y.Final).FirstOrDefault(), notvize= x.Notlar.Where(y => y.OgrenciDersID == x.OgrenciDersID).Select(y => (decimal)y.Vize).FirstOrDefault() }).ToList();
            GridNot.DataSource = OgrenciDers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach ( DataGridViewRow item in GridOgrenci.SelectedRows)
            {
                var ogrenciID =Convert.ToInt32(item.Cells["ogrenciID"].Value);
                var OgrenciDers = db.OgrenciDersler.Where(x => x.Statu == 1 && x.OgrenciID == ogrenciID);
                OgrenciDersleriListele(ogrenciID);
            }
            
            

        }
        public void SeciliNot() 
        {
            foreach (DataGridViewRow item in GridNot.SelectedRows)
            {
                vizebox.Text = item.Cells["notvize"].Value.ToString();
                Finalbox.Text = item.Cells["notfinal"].Value.ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OgrenciDers OgrenciDers=new OgrenciDers();
            for (int i = 0; i < GridSecilenDers.Rows.Count; i++)
            {
                var OgrenciDersID= Convert.ToInt32(GridSecilenDers.Rows[i].Cells["ogrenciDersID"].Value);
                OgrenciDers = db.OgrenciDersler.Where(x => x.OgrenciDersID == OgrenciDersID).FirstOrDefault();
                OgrenciDers.Statu = 2;

            }
            db.SaveChanges();
            MessageBox.Show("Kayıt tamamlandı");



        }
        private void button3_Click(object sender, EventArgs e)
        {
            SeciliNot();
        }
        private void btnNotgir_Click(object sender, EventArgs e)
        {
            
            OgrenciNot not = new OgrenciNot();
            OgrenciNot yeninot =new OgrenciNot();
            foreach (DataGridViewRow item in GridNot.SelectedRows)
            {
                
                var OgrenciDersID = Convert.ToInt32(item.Cells["notogrencidersID"].Value);
                not = db.OgrenciNots.Where(x => x.OgrenciDersID == OgrenciDersID).FirstOrDefault();
                var ogrenciID = Convert.ToInt32(item.Cells["notogrenciID"].Value);
                if (not ==null)
                {
                    yeninot.OgrenciDersID = OgrenciDersID;
                    yeninot.Vize =string.IsNullOrEmpty(vizebox.Text) ? (decimal?)null : Convert.ToDecimal(vizebox.Text);
                    yeninot.Final = string.IsNullOrEmpty(Finalbox.Text) ? (decimal?)null : Convert.ToDecimal(Finalbox.Text);
                    yeninot.OgrenciID = ogrenciID;
                    db.OgrenciNots.Add(yeninot);
                    
                }
                else { 
                    not.Vize = string.IsNullOrEmpty(vizebox.Text) ? (decimal?)null : Convert.ToDecimal(vizebox.Text);
                    not.Final = string.IsNullOrEmpty(Finalbox.Text) ? (decimal?)null : Convert.ToDecimal(Finalbox.Text);
                }
            }
            db.SaveChanges();
            NotlariListele();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form1 formadon = new Form1();
            formadon.Show();
            this.Hide();
        }
        public class ViewDanismanOgrenci 
        {
            public string ogrenciAdı { get; set; }    
            public string ogrenciSoyadı { get; set; }    
            public int ogrenciID { get; set; }    
            public string ogrenciBolum { get; set; }   
            public int ogrenciyariyil { get; set; }    
               
          
        }
        public class ViewOgrenciDersler
        {
            public string dersAdı { get; set; }
            public string dersKodu { get; set; }
            public int ogrenciDersID { get; set; }
            //public string ogrenciBolum { get; set; }
            //public int ogrenciyariyil { get; set; }


        }
        public class ViewNot 
        {
            public string notogrenciad { get; set; }
            public string notsoyad { get; set; }
            public string notdersad { get; set; }
            public string notderscode { get; set; }
            public decimal notvize { get; set; }
            public decimal notfinal { get; set; }
            public int notogrencidersID { get; set; }
            public int notogrenciID { get; set; }
        }

        private void GridNot_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        private void GridNot_cellclick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void GridNot_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

       
    }
}
