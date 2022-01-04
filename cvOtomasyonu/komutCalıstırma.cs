using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvOtomasyonu
{
    class komutCalıstırma
    {
        public static void sorguCalıstır(string sorgu)
        {
            veritabanıBaglantı.baglanti.Open();
            OleDbCommand komut = new OleDbCommand(sorgu, veritabanıBaglantı.baglanti);
            komut.ExecuteNonQuery();
            veritabanıBaglantı.baglanti.Close();
        }
    }
}
