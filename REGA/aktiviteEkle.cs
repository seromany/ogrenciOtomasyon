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

namespace REGA
{
    public partial class aktiviteEkle : Form
    {
        public aktiviteEkle()
        {
            InitializeComponent();
        }
        Class1 sinif = new Class1();
        SqlCommand komut;
        private string sorgu;
        private SqlConnection baglanti = new SqlConnection();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.ConnectionString = sinif.getConnectionString();
                baglanti.Open();
                sorgu = "INSERT INTO rega_aktivite(yas_grubu,etkinlik_adi,etkinlik_tarih,ogr_no,yorum,etkinlik_saati) VALUES(@yas_grubu,@etkinlik_adi,@etkinlik_tarih,@ogr_no,@yorum,@etkinlik_saat)";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@yas_grubu", yas_grubu.Text);
                komut.Parameters.AddWithValue("@etkinlik_adi", etkinlik_ad.Text);
                komut.Parameters.AddWithValue("@etkinlik_tarih", etkinlik_tarih.Value);
                komut.Parameters.AddWithValue("@ogr_no", ogr_no.Text);
                komut.Parameters.AddWithValue("@yorum", yorum.Text);
                komut.Parameters.AddWithValue("@etkinlik_saat", etkinlik_saat.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                sinif.veriMesaj(true);
            }
            catch
            {
                sinif.veriMesaj(false);
            }

        }

        private void aktiviteEkle_Load(object sender, EventArgs e)
        {
            sinif.ogrenciCekme(ogr_no);
        }

        private void buttons_Click(object sender, EventArgs e)
        {
            sinif.tiklandi = true;
            sinif.menuGecis((Button)sender);
        }

        private void aktiviteEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            sinif.programiKapat(e);
        }
    }
}
