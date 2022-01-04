using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cvOtomasyonu
{
    public partial class kullanıcıGirisi : Form
    {
        public kullanıcıGirisi()
        {
            InitializeComponent();
        }

        private void giris_Click(object sender, EventArgs e)
        {
            veritabanıBaglantı.baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=cvdb.accdb");
            DataTable Ds = new DataTable();
            Ds = veritabanıBaglantı.goster("SELECT * FROM kullanıcıgirişi where isim='" + textBox1.Text + "'").Tables[0];
            if (Ds.Rows.Count > 0)
            {
                Ds = veritabanıBaglantı.goster("SELECT * FROM kullanıcıgirişi where parola = '" + textBox2.Text + "'").Tables[0];

                if (Ds.Rows.Count > 0)
                {
                    kullanıcıGirisi form1 = new kullanıcıGirisi();
                    form1.Close();
                    cvKayıt ac = new cvKayıt();
                    ac.Show();
                    this.Hide();
                    
                }
                else
                {
                    MessageBox.Show("Hatalı şifre girdiniz.");
                }

            }
            else
            {
                MessageBox.Show("Kullanıcı adınızı hatalı girdiniz.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point İlkkonum;
        bool durum = false;
        private void kullanıcıGirisi_MouseDown(object sender, MouseEventArgs e)
        {
            durum = true;
            this.Cursor = Cursors.SizeAll; // Fareyi taşıma şeklinde seçim yapılmış ikon halini alması.
            İlkkonum = e.Location;
        }

        private void kullanıcıGirisi_MouseMove(object sender, MouseEventArgs e)
        {
            if (durum)
            {
                this.Left = e.X + this.Left - (İlkkonum.X);
                this.Top = e.Y + this.Top - (İlkkonum.Y);
            }
        }

        private void kullanıcıGirisi_MouseUp(object sender, MouseEventArgs e)
        {
            durum = false;
            this.Cursor = Cursors.Default;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
