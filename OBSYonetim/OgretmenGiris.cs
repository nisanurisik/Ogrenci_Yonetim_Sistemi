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
    public partial class OgretmenGiris : Form
    {
        Context db =new Context();
        public OgretmenGiris()
        {
            InitializeComponent();
        }
        public int ID;
        private void button1_Click(object sender, EventArgs e)
        {
            var ogretmen = db.Ogretmenler.ToList();

            if (ogretmen.Where(p => p.OgretmenAd == textBox1.Text && p.OgretmenSifre == textBox2.Text).Any())
            {
                OgretmenSayfa ogretmensayfa = new OgretmenSayfa();
                ogretmensayfa.ID = db.Ogretmenler.Where(p => p.OgretmenAd == textBox1.Text && p.OgretmenSifre == textBox2.Text).Select(x => x.OgretmenID).FirstOrDefault();
                ogretmensayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Öğretmen Kullanıcı veya Şifre Hatalı");
            }
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            Form1 formadon = new Form1();
            formadon.Show();
            this.Hide();
        }
    }
}
