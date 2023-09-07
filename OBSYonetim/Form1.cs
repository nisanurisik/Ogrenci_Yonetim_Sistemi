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
    
    public partial class Form1 : Form
    {
        Context db = new Context();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // db.Database.Create();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OgrenciGiris ogrenciGiris = new OgrenciGiris();
            ogrenciGiris.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MemurGiriscs memurGiriscs = new MemurGiriscs();
            memurGiriscs.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OgretmenGiris ogretmenGiris = new OgretmenGiris();
            ogretmenGiris.Show();
            this.Hide();
        }
    }
}
