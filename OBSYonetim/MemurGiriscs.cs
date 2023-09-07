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
    public partial class MemurGiriscs : Form
    {
        Context db = new Context();
        public MemurGiriscs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Memur = db.Memurlar.ToList();

            if (Memur.Where(p => p.KullaniciAdi == textBox1.Text && p.sifre == textBox2.Text).Any())
            {
                MemurSayfa ogrenciSayfa = new MemurSayfa();

                ogrenciSayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Memur Kullanıcı veya Şifre Hatalı");
            }
        }

        private void MemurGiriscs_Load(object sender, EventArgs e)
        {

        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            Form1 formadon = new Form1();
            formadon.Show();
            this.Hide();
        }
    }
}
