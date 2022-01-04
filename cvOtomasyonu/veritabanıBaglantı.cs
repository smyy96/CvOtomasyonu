using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvOtomasyonu
{
    class veritabanıBaglantı
    {
        public static OleDbConnection baglanti;
        public static DataSet goster(string sql)
        {

            if (baglanti.State.ToString() == "Open")
            {
                baglanti.Close();
            }
            if (baglanti == null)
            {
                      baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=cvdb.accdb");
            }

            OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, baglanti);
            DataSet ds = new DataSet();
            adaptor.Fill(ds, "tablo");
            baglanti.Close();
            return ds;
        }
    }
}

