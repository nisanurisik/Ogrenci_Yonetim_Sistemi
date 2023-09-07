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
     
    public partial class OgrenciGiris : Form
    {
        Context db = new Context();
        public OgrenciGiris()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            var ogrenci = db.Ogrenciler.ToList();
            
            if (ogrenci.Where(p=>p.OgrenciNo==textBox1.Text && p.Sifre==textBox2.Text).Any())
            {
                OgrenciSayfa ogrenciSayfa = new OgrenciSayfa();
                ogrenciSayfa.ogrencino = textBox1.Text;
                ogrenciSayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Öğrenci No veya Şifre Hatalı");
            }
            
            
        }

        private void OgrenciGiris_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            Form1 formadon = new Form1();
            formadon.Show();
            this.Hide();
        }
    }
}
