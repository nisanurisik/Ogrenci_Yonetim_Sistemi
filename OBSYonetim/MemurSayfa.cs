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
    public partial class MemurSayfa : Form
    {
        Context db = new Context();
        public MemurSayfa()
        {
            InitializeComponent();
        }


        private void MemurSayfa_Load(object sender, EventArgs e)
        {
            var Bolum = db.Bolumler.ToList();
            foreach (var item in Bolum)
            {
                comboBox1.Items.Add(item);

            }
            comboBox1.DataSource = Bolum;
            comboBox2.DataSource = Bolum;
            comboBox3.Items.Add(1);
            comboBox3.Items.Add(2);
            comboBox3.Items.Add(3);
            comboBox3.Items.Add(4);
            OgretmenGoster();
        }
        public void Temizle()
        {
            ogretmenad.Text = "";
            ogretmentel.Text = "";
            ogretmensifre.Text = "";
            ogretmensoyad.Text = "";

            ogrenciad.Text = "";
            ogrencino.Text = "";
            ogrencisifre.Text = "";
            ogrencisoyad.Text = "";
        }
        public void OgretmenGoster()
        {
            var ogretmenler = db.Ogretmenler.Select(x=>new ViewOgretmen { ogrtAd=x.OgretmenAd,ogrtSoyad=x.OgretmenSoyAd,ogrtBolum=x.Bolum.BolumAd,ogrtID=x.OgretmenID}).ToList();
            GridOgretmen.DataSource = ogretmenler;


        }
        private void button1_Click(object sender, EventArgs e)
        {
            var a = Convert.ToInt32(comboBox1.SelectedValue);
            Ogretmen ogr = new Ogretmen();
            if (ogretmenad.Text != "" && ogretmentel.Text != "" && ogretmensifre.Text != "" && ogretmensoyad.Text != "" && a != 0)
            {
                ogr.OgretmenAd = ogretmenad.Text;
                ogr.OgretmenSoyAd = ogretmensoyad.Text;
                ogr.OgretmenSifre = ogretmensifre.Text;
                ogr.OgretmenTel = ogretmentel.Text;
                ogr.BolumID = a;
                db.Ogretmenler.Add(ogr);
                db.SaveChanges();
                MessageBox.Show("Başarıyla kaydedildi");
            }
            else
            {
                MessageBox.Show("Doldurulmayan Alanlar var Lütfen işlemi yeniden Yapınız ");
            }
            Temizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var a = Convert.ToInt32(comboBox2.SelectedValue);
            var ogrenciler = db.Ogrenciler.Select(x => x.OgrenciNo).ToList();
            Ogrenci ogrenci = new Ogrenci();
            if (!ogrenciler.Contains(ogrencino.Text))
            {
                if (ogrenciad.Text != "" && ogrencino.Text != "" && ogrencisifre.Text != "" && ogrencino.Text != "" && ogrencisoyad.Text != "" && a != 0)
                {
                    ogrenci.OgrenciAd = ogrenciad.Text;
                    ogrenci.OgrenciSoyad = ogrencisoyad.Text;
                    ogrenci.Sifre = ogrencisifre.Text;
                    ogrenci.OgrenciNo = ogrencino.Text;
                    ogrenci.BolumID = a;
                    ogrenci.yariyil = 1;
                    ogrenci.Sinif = 1;

                    db.Ogrenciler.Add(ogrenci);
                    db.SaveChanges();
                    MessageBox.Show("Başarıyla kaydedildi");
                }
                else
                {
                    MessageBox.Show("Doldurulmayan Alanlar var Lütfen işlemi yeniden Yapınız ");
                }
            }
            else
            {
                MessageBox.Show("Öğrenci numarası Farklı olmalıdır. ");
            }

            Temizle();
        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            //var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
            var sınıf = Convert.ToInt32(comboBox3.SelectedItem);
            using (var db = new Context())
            {
                if (GridOgretmen.SelectedRows.Count !=0 && sınıf!=null && sınıf!=0 )
                {
                    
                    List<Ogrenci> ogrenci = new List<Ogrenci>();
                    foreach (DataGridViewRow item in GridOgretmen.SelectedRows)
                    {
                        var Bolumadi = item.Cells["ogrtBolum"].Value.ToString();
                        var bolumID= db.Bolumler.Where(x => x.BolumAd == Bolumadi).Select(x => x.BolumID).FirstOrDefault();
                        ogrenci = db.Ogrenciler.Where(x => x.BolumID == bolumID && x.Sinif == sınıf).ToList();
                        foreach (var abc in ogrenci)
                        {
                            abc.OgretmenID = Convert.ToInt32(item.Cells["ogrtID"].Value);
                        }
                        
                        
                        
                    }
                    db.SaveChanges();
                    MessageBox.Show("Danışman olarak atandı");
                    var denem = db.Ogrenciler.ToList();
                }
                else
                {
                    MessageBox.Show("Bilgileri Eksiksiz Giriniz");
                }

                OgretmenGoster();

            }
        }
        public class ViewOgretmen
        {
            public int ogrtID { get; set; }
            public string ogrtAd { get; set; }
            public string ogrtSoyad { get; set; }
            public string ogrtBolum { get; set; }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Form1 formadon = new Form1();
            formadon.Show();
            this.Hide();
        }
    }
}
