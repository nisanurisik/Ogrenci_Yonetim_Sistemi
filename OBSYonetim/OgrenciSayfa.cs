using OBSYonetim.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OBSYonetim
{
    public partial class OgrenciSayfa : Form
    {

        Context db = new Context();
        public OgrenciSayfa()
        {
            InitializeComponent();
        }
        public string ogrencino;
        public int sayac = 0;
        private void OgrenciSayfa_Load(object sender, EventArgs e)
        {
            OgrenciGiris frm1 = new OgrenciGiris();

            var ogrenci = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
            ogrAD.Text = ogrenci.OgrenciAd;
            OgrBolum.Text = ogrenci.Bolum.BolumAd;
            ogrSinif.Text = ogrenci.Sinif.ToString();
            ogrSoyad.Text = ogrenci.OgrenciSoyad;

            var Ders = db.Dersler.Where(x => x.BolumID == ogrenci.BolumID).ToList();
            dersleriGoster();
            EklenenDersleriGöster();
            DanismanOnayıDersleri();
            KaydıTanımlananDersler();
            Transkript();
            notlariGoster();

        }
        public void dersleriGoster()
        {
            var SistemYariyili = db.Ayarlars.Select(x => x.SistemYariYil).FirstOrDefault();
            OgrenciDers1Grid.AutoGenerateColumns = false;
            using (var db = new Context())
            {
                var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
                var TümDersler = db.Dersler.Where(x => x.BolumID == ogrenci1.BolumID).ToList();
                var SecilenDersler = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Ders.Yariyil == SistemYariyili).Select(x => x.DersID).ToList();
                var UygunDersler = TümDersler.Where(x => !SecilenDersler.Contains(x.DersID) && x.Yariyil == SistemYariyili).ToList();
                OgrenciDers1Grid.DataSource = UygunDersler;
            }



        }
        public void Transkript()
        {
            var SistemYariyili = db.Ayarlars.Select(x => x.SistemYariYil).FirstOrDefault();
            var ogrenci = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).Select(x => x.yariyil).FirstOrDefault();
            for (int i = 0; i < ogrenci; i++)
            {
                if (!yariyilsecim.Items.Contains(i + 1))
                {
                    yariyilsecim.Items.Add(i + 1);
                }

            }
            List<ViewTranskript> transkripts = new List<ViewTranskript>();  
            var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
            var yariyil = Convert.ToInt32(yariyilsecim.SelectedItem);
            var NotDers = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 2 && x.Ders.Yariyil == yariyil).Select(x => x.OgrenciDersID).ToList();
            var DerslerID = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 2 && x.Ders.Yariyil == yariyil).Select(x=>x.OgrenciDersID).ToList();
            foreach (var item in DerslerID)
            {
                decimal gecmenotu;
                string gecmedurumu = "";
                var Girilenvize = db.OgrenciNots.Where(x => x.OgrenciID == ogrenci1.ID && x.OgrenciDersID == item).Select(x => x.Vize).FirstOrDefault();
                var girilenfinal = db.OgrenciNots.Where(x => x.OgrenciID == ogrenci1.ID && x.OgrenciDersID == item).Select(x => x.Final).FirstOrDefault();
                if (girilenfinal == null || Girilenvize == null)
                {

                    gecmedurumu = "";


                }
                else
                {
                    gecmenotu = (decimal)((Girilenvize * 40) / 100 + (girilenfinal * 60) / 100);
                    if (gecmenotu < 50)
                    {
                        gecmedurumu = "BAŞARISIZ";
                    }
                    else
                    {
                        gecmedurumu = "BAŞARILI";
                    }
                
                }
                var Dersler = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 2 && x.Ders.Yariyil == yariyil&&x.OgrenciDersID==item).Select(x => new ViewTranskript() { trAd = x.Ders.DersAd, trCode = x.Ders.DersCode, trYariyil = x.Ders.Yariyil,trgecmeDurumu= gecmedurumu }).FirstOrDefault();
                transkripts.Add(Dersler);

            }
            GridTranskript.DataSource = transkripts;
            var Not = db.OgrenciNots.Where(x => x.OgrenciID == ogrenci1.ID && NotDers.Contains(x.OgrenciDersID)).ToList();


            //var Dersler = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 2 && x.Ders.Yariyil == yariyil).Select(x => new ViewTranskript() { trAd = x.Ders.DersAd, trCode = x.Ders.DersCode, trYariyil = x.Ders.Yariyil }).ToList();

            //foreach (var item in Not)
            //{

            //    var gecmeNot = (item.Vize * 40) / 100 + (item.Final * 60) / 100;
            //    for (int i = 0; i < GridTranskript.Rows.Count; i++)
            //    {
            //        var a = GridTranskript.Rows[i].Cells["trDersCode"].Value.ToString();
            //        if (Not.Where(x => x.OgrenciDers.Ders.DersCode == a).Any())
            //        {
            //            if (gecmeNot < 50 )
            //            {
            //                GridTranskript.Rows[i].Cells["trgecmeDurumu"].Value = "BAŞARISIZ";
            //            }
            //            else
            //            {
            //                GridTranskript.Rows[i].Cells["trgecmeDurumu"].Value = "BAŞARILI";
            //            }
            //        }

            //    }


            //}
        }
        public void notlariGoster()
        {
            var ogrenci = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).Select(x => x.yariyil).FirstOrDefault();
            for (int i = 0; i < ogrenci; i++)
            {
                if (!notYariyilcombox.Items.Contains(i + 1))
                {
                    notYariyilcombox.Items.Add(i + 1);
                }

            }
            var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
            var yariyil = Convert.ToInt32(notYariyilcombox.SelectedItem);
            var NotDersler = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 2 && x.Ders.Yariyil == yariyil).Select(x => x.OgrenciDersID).ToList();
            List<ViewNot> notliste = new List<ViewNot>();
            foreach (var item in NotDersler)
            {
                string ortalama=""; 
                var Girilenvize = db.OgrenciNots.Where(x => x.OgrenciID == ogrenci1.ID && x.OgrenciDersID ==item).Select(x=>x.Vize).FirstOrDefault();
                var girilenfinal = db.OgrenciNots.Where(x => x.OgrenciID == ogrenci1.ID && x.OgrenciDersID == item).Select(x => x.Final).FirstOrDefault();
                if (girilenfinal == null || Girilenvize==null)
                {

                    ortalama ="";
                        
                    
                }
                else
                {
                    var deger = (decimal)((Girilenvize * 60) / 100 + (girilenfinal * 40) / 100);
                    ortalama = deger.ToString();
                    
                        
                    
                }
                var Notlar = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 2 && x.Ders.Yariyil == yariyil&&x.OgrenciDersID==item).Select(x => new ViewNot { notderscode = x.Ders.DersCode, notdersadı = x.Ders.DersAd,notvize=Girilenvize,notfinal= girilenfinal, notdersortalama= ortalama }).FirstOrDefault();
                notliste.Add(Notlar);
            }
            GridNot.DataSource = notliste;
            
            
            


        }
        public void KaydıTanımlananDersler()
        {
            var SistemYariyili = db.Ayarlars.Select(x => x.SistemYariYil).FirstOrDefault();
            var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
            var veri = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 2 && x.Ders.Yariyil == SistemYariyili).Select(x => new ViewDers() { drsAd = x.Ders.DersAd, drsCode = x.Ders.DersCode }).ToList();
            GridOnayli.DataSource = veri;
        }
        public void DanismanOnayıDersleri()
        {
            //var SistemYariyili = db.Ayarlars.Select(x => x.SistemYariYil).FirstOrDefault();
            var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
            var veri = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 1).Select(x => new ViewDers() { drsAd = x.Ders.DersAd, drsCode = x.Ders.DersCode }).ToList();
            GridDanismanOnay.DataSource = veri;
        }

        public void EklenenDersleriGöster()
        {


            DersEkleGrid.AutoGenerateColumns = false;
            using (var db = new Context())
            {
                var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();

                var OgrenciDers = db.OgrenciDersler.Where(x => x.Ders.BolumID == ogrenci1.BolumID && x.Statu == 0 && x.OgrenciID == ogrenci1.ID).Select(x => new ViewDers() { drsAd = x.Ders.DersAd, drsCode = x.Ders.DersCode }).ToList();
                DersEkleGrid.DataSource = OgrenciDers;
                dersleriGoster();


            }
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
            DersEkleGrid.AutoGenerateColumns = false;
            using (var db = new Context())
            {
                OgrenciDers ogrenciDers = new OgrenciDers();
                foreach (DataGridViewRow item in OgrenciDers1Grid.SelectedRows)
                {
                    string seciliDersKod = item.Cells["DersKodu"].Value.ToString();
                    int ID = db.Dersler.Where((x) => x.DersCode == seciliDersKod).Select(x => x.DersID).FirstOrDefault();
                    ogrenciDers.DersID = ID;
                    ogrenciDers.OgrenciID = ogrenci1.ID;
                    db.OgrenciDersler.Add(ogrenciDers);
                    db.SaveChanges();

                }

            }
            EklenenDersleriGöster();
            dersleriGoster();
        }

        private void DersSil_Click(object sender, EventArgs e)
        {
            var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
            if (MessageBox.Show("Silmek İstediğinize Emin misiniz ?", "Silme Onayı", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow item in DersEkleGrid.SelectedRows)
                {

                    string seciliDersKod = item.Cells["drsCode"].Value.ToString();
                    int ID = db.OgrenciDersler.Where(x => x.Ders.DersCode == seciliDersKod && x.OgrenciID == ogrenci1.ID && x.Statu == 0).Select(x => x.OgrenciDersID).FirstOrDefault();
                    var silinecekDers = db.OgrenciDersler.Find(ID);
                    db.OgrenciDersler.Remove(silinecekDers);
                    db.SaveChanges();
                    MessageBox.Show("Başarılı bir şekilde çıkarıldı");

                }
            }

            EklenenDersleriGöster();
            dersleriGoster();
        }

        private void DanismanGonder_Click(object sender, EventArgs e)
        {

            using (Context db = new Context())
            {
                List<OgrenciDers> Onayagonderilen = new List<OgrenciDers>();
                var ogrenci1 = db.Ogrenciler.Where(x => x.OgrenciNo == ogrencino).FirstOrDefault();
                Onayagonderilen = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 0).ToList();
                if (MessageBox.Show("Ders Onayına Göndereceksiniz ve Onay Sayfasına Yönlendirileceksiniz Emin misiniz ?", "Ders Onayı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var item in Onayagonderilen)
                    {
                        item.Statu = 1;


                    }

                }
                db.SaveChanges();

                EklenenDersleriGöster();
                dersleriGoster();
                DanismanOnayıDersleri();
                //var veri = db.OgrenciDersler.Where(x => x.OgrenciID == ogrenci1.ID && x.Statu == 1 ).Select(x => new ViewDers() { drsAd = x.Ders.DersAd, drsCode = x.Ders.DersCode }).ToList();
                //GridDanismanOnay.DataSource = veri;
                tabControl1.SelectTab("tabPage3");
            }


        }

        private void yariyilsecim_SelectedIndexChanged(object sender, EventArgs e)
        {
            Transkript();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            notlariGoster();

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void logout_Click(object sender, EventArgs e)
        {
            Form1 formadon = new Form1();
            formadon.Show();
            this.Hide();
        }
    }
    public class ViewDers
    {
        public string drsAd { get; set; }
        public string drsCode { get; set; }
    }
    public class ViewTranskript
    {
        public string trAd { get; set; }
        public string trCode { get; set; }
        public int trYariyil { get; set; }
        public string trgecmeDurumu { get; set; }


    }
    public class ViewNot 
    {
        public string notdersadı { get; set; }
        public string notderscode { get; set; }
        public decimal? notvize { get; set; }
        public decimal? notfinal { get; set; }
        public string notdersortalama { get; set; }
    }
}
