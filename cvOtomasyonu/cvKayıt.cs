using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using FastReport;
namespace cvOtomasyonu
{
    public partial class cvKayıt : Form
    {
        public cvKayıt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point İlkkonum;
        bool durum = false;
        private void cvKayıt_MouseDown(object sender, MouseEventArgs e)
        {
            durum = true;
            this.Cursor = Cursors.SizeAll;
            İlkkonum = e.Location;
        }

        private void cvKayıt_MouseMove(object sender, MouseEventArgs e)
        {
            if (durum)
            {
                this.Left = e.X + this.Left - (İlkkonum.X);
                this.Top = e.Y + this.Top - (İlkkonum.Y);
            }
        }

        private void cvKayıt_MouseUp(object sender, MouseEventArgs e)
        {
            durum = false;
            this.Cursor = Cursors.Default;
        }

        private void btnTemizle()// formdaki toolları temizleme
        {
            foreach (Control item in this.groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;
                    tbox.Clear();
                }
            }
            foreach (Control item in this.groupBox2.Controls)
            {
                if (item is CheckBox)
                {
                    CheckBox tbox = (CheckBox)item;
                    tbox.Checked = false;
                }
            }
            rdBsrcNo.Checked = false;
            rBsrcYes.Checked = false;
            txtBlgsyr.Clear();
            txtEgitimBlgs.Clear();
            txtIsDnym.Clear();
            txtOzet.Clear();
            txtSertfk.Clear();
            txtYbncDl.Clear();
        }

        public void datagöster()//datagridleri doldurma
        {
            DataSet göster = new DataSet();
            göster = veritabanıBaglantı.goster("select * from users");
            dataGridView1.DataSource = göster.Tables["tablo"];
        }
        // public static bool kontrol_et(string kontrol)//ıd girilen textin boş olup olamama durumuna bakıyor buna göre günvcelleme mi yoksa kayıt işlemimi ayırt etmesini saglayan kısım
        //{
        //    bool sonuc=false;
        //    veritabanıBaglantı.baglanti.Open();
        //    if (kontrol=="")
        //    {
        //        veritabanıBaglantı.baglanti.Close();
        //        return sonuc;
        //    }
        //    else
        //    {
        //        OleDbCommand komut = new OleDbCommand("select * from users where Id =" + kontrol, veritabanıBaglantı.baglanti);
        //        OleDbDataReader dr = komut.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            if (kontrol == dr[0].ToString())
        //            {
        //                veritabanıBaglantı.baglanti.Close();
        //                return true;
        //            }
        //            else return sonuc;
        //        }
        //    }
        //    veritabanıBaglantı.baglanti.Close();
        //    return sonuc; 
        //}
        
        private void cvKayıt_Load(object sender, EventArgs e)
        {

            ToolTip bilgikutucugu = new ToolTip();
            bilgikutucugu.SetToolTip(txtEgitimBlgs, "Eğitim gördüğünüz okulları yazınız.");
            bilgikutucugu.SetToolTip(txtYbncDl, "Örnek: İngilizce (orta)");
            datagöster();
        }


        private void btnSil_Click(object sender, EventArgs e)//silme
        {
            komutCalıstırma.sorguCalıstır("delete from users where Id=" + dataGridView1.CurrentRow.Cells[0].Value.ToString());
            MessageBox.Show("Kayıt başarıyla silinmiştir.");
            datagöster();

        }
        
        public void dbBilgilerGetir(string sorgu)
        {
            veritabanıBaglantı.baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, veritabanıBaglantı.baglanti);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtTC.Text = dr["TC"].ToString();
                txtAd.Text = dr["kullanıcıAdı"].ToString();
                txtSoyad.Text = dr["kullanıcıSoyadı"].ToString();
                txtTelefon.Text = dr["telefon"].ToString();
                dateDogumT.Text = dr["dogumTarihi"].ToString();
                txtAdres.Text = dr["adres"].ToString();
                txtYbncDl.Text = dr["yabancıDil"].ToString();
                txtEgitimBlgs.Text = dr["egitimBilgisi"].ToString();
                txtIsDnym.Text = dr["isDeneyimleri"].ToString();
                txtSertfk.Text = dr["sertifikaBilgisi"].ToString();
                txtOzet.Text = dr["özetBilgi"].ToString();
                txtBlgsyr.Text = dr["bilgisayarBilgisi"].ToString();
                txtMail.Text = dr["eposta"].ToString();
                string cinsiyet, uyruk, srcBlg;
                cinsiyet = dr["cinsiyet"].ToString();
                if (cinsiyet == "Kadın") checkBox1.Checked = true;
                else checkBox2.Checked = true;
                uyruk = dr["uyruk"].ToString();
                if (uyruk == "T.C") checkBox4.Checked = true;
                else checkBox3.Checked = true;
                srcBlg = dr["sürücüBelgesi"].ToString();
                if (srcBlg == "1") rBsrcYes.Checked = true;
                else rdBsrcNo.Checked = true;

            }
            veritabanıBaglantı.baglanti.Close();
        }
        

        public static string cinsiyet, uyruk;
        public static int sürücüBel;
        public void  kayıt_güncelleme()
        {
            
            if (checkBox2.Checked) cinsiyet = "Erkek";
            else cinsiyet = "Kadın";
            if (checkBox4.Checked) uyruk = "T.C";
            else uyruk = "Yabancı";
            if (rBsrcYes.Checked) sürücüBel = 1;
            else sürücüBel = 0;
            
        }

        public static bool kayıtTürü=false;
        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            string sorgu;
            sorgu = "select * from users where ıd =" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dbBilgilerGetir(sorgu);
            tabControl1.SelectedTab = tabPage1;
            kayıtTürü = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnTemizle();
            kayıtTürü = false;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRaporgös_Click(object sender, EventArgs e)
        {
            
            veritabanıBaglantı.baglanti.Open();
            using (OleDbCommand com = new OleDbCommand())
            {
                com.Connection = veritabanıBaglantı.baglanti;
                com.CommandType = CommandType.Text;
                com.CommandText = "select * from users where ıd=" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                using (OleDbDataReader dr = com.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(dr);
                        dt.TableName = "users";
                        //dt.Columns.Add("NewColumn",typeof(string));
                        //for (int i = 0; i < 1; i++)
                        //{
                        //    var row = dt.NewRow();
                        //    row["NewColumn"]= dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        //}
                        Report report = new Report();
                        report.Load("cvOtomasyon1.frx");
                        report.RegisterData(dt,dt.TableName);
                        report.Prepare();
                        report.ShowPrepared();
                    }
                }
                
            }
            veritabanıBaglantı.baglanti.Close();
        }

        private void btnButunRapor_Click(object sender, EventArgs e)
        {
            using (Report rapor = new Report())
            {
                rapor.Load(Application.StartupPath + "\\cvOtomasyon1.frx");
                rapor.Show();
            }
        }

        private void dateDogumT_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Kayıt_Click(object sender, EventArgs e)
        {
             DialogResult cikis = new DialogResult();
            //bool kayıt_durumu;
            string sorgu;

            if (txtAd.Text == "" || txtSoyad.Text == "" || txtTelefon.Text == "" || dateDogumT.Value == null || txtAdres.Text == "" || txtMail.Text=="") // kayıt işleminde kişisel bilgilerin boş bırakılmamasına dair verilen uyarı mesajı
            {
                MessageBox.Show("İletişim bilgilerinizin hepsini doldurmanız gerekmektedir. \nYabancı uyrukluysanız T.C bilgisini boş bırakabilirsiniz.");
            }
            else //kayıt işleminin yapıldıgı kısım
            {
               
                //kayıt_durumu = kontrol_et(txtGetir.Text);
                if (kayıtTürü==true)
                {
                    cikis = MessageBox.Show("Güncelleştirme yapmak üzeresiniz. Devam etmek istiyor musunuz?","UYARI",MessageBoxButtons.YesNo);
                    if (cikis==DialogResult.Yes)
                    {
                        kayıt_güncelleme();
                        sorgu ="update users set kullanıcıAdı= '" + txtAd.Text + "',kullanıcıSoyadı='" + txtSoyad.Text + "',telefon='" + txtTelefon.Text + "',dogumTarihi='" + dateDogumT.Value + "',adres='" + txtAdres.Text + "',yabancıDil='" + txtYbncDl.Text + "',egitimBilgisi='" + txtEgitimBlgs.Text + "',isDeneyimleri='" + txtIsDnym.Text + "',sertifikaBilgisi='" + txtSertfk.Text + "',bilgisayarBilgisi='" + txtBlgsyr.Text + "',özetBilgi='" + txtOzet.Text + "',cinsiyet='" + cinsiyet + "',uyruk='" + uyruk + "',sürücüBelgesi='" + sürücüBel + "',TC='" + txtTC.Text + "',eposta='" + txtMail.Text + "' where Id=" + dataGridView1.CurrentRow.Cells[0].Value.ToString()+ "";
                        komutCalıstırma.sorguCalıstır(sorgu);
                        MessageBox.Show("Cv başarıyla güncelleştirilmiştir.");
                        datagöster();
                        btnTemizle();
                    }
                    if (cikis==DialogResult.No)
                    {
                        MessageBox.Show("İşleminiz iptal edildi.");
                        btnTemizle();
                    }
                    kayıtTürü = false;
                }
                else if(kayıtTürü == false)
                {
                    kayıt_güncelleme();
                    sorgu = "insert into users (kullanıcıAdı,kullanıcıSoyadı,telefon,dogumTarihi,adres,yabancıDil,egitimBilgisi,isDeneyimleri,sertifikaBilgisi,bilgisayarBilgisi,özetBilgi,cinsiyet,uyruk,sürücüBelgesi,TC,eposta)values ('" + txtAd.Text + "', '" + txtSoyad.Text + "', '" + txtTelefon.Text + "', '" + dateDogumT.Value + "', '" + txtAdres.Text + "', '" + txtYbncDl.Text + "', '" + txtEgitimBlgs.Text + "', '" + txtIsDnym.Text + "', '" + txtSertfk.Text + "', '" + txtBlgsyr.Text + "', '" + txtOzet.Text + "', '" + cinsiyet + "', '" + uyruk + "', '" + sürücüBel + "', '" + txtTC.Text + "', '" + txtMail.Text + "')";
                    komutCalıstırma.sorguCalıstır(sorgu);
                    MessageBox.Show("Cv başarıyla kaydedilmiştir.");
                    datagöster();
                    btnTemizle();
                }
                else
                {
                    MessageBox.Show("Hata oluştu.");
                }
                
            }
        }
    }
}
