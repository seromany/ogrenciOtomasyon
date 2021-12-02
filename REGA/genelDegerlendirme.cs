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
    public partial class genelDegerlendirme : Form
    {
        public genelDegerlendirme()
        {
            InitializeComponent();
        }
        Class1 sinif = new Class1();
        private SqlConnection baglanti = new SqlConnection();
        private SqlDataReader data;

        private void genelDegerlendirme_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sinif.dataCagir("Select * From rega_genelDegerlendirme");
            sinif.ogrenciCekme(ogrNo);
        }
        private void buttons_Click(object sender, EventArgs e)
        {
            sinif.tiklandi = true;
            sinif.menuGecis((Button)sender);
        }

        private void genelDegerlendirme_FormClosing(object sender, FormClosingEventArgs e)
        {
            sinif.programiKapat(e);
        }
        public void baglantiOlustur()
        {
            baglanti.ConnectionString = sinif.getConnectionString();
            baglanti.Open();
        }
        public void giris()
        {
            baglantiOlustur();
            SqlCommand komut1 = new SqlCommand("Select * From rega_report3 WHERE ogr_no=@ogr_no",baglanti);
            komut1.Parameters.AddWithValue("@ogr_no", Convert.ToInt32(ogrNo.Text));
            data = komut1.ExecuteReader();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                giris();
                if (data.Read())
                {
                    data.Close();
                    String sorgu = "UPDATE rega_report3 SET genel_degerlendirme=@genel_degerlendirme, aile_okur_yazarlik=@aile_okur_yazarlik WHERE ogr_no=@ogr_no";
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@ogr_no", Convert.ToInt32(ogrNo.Text));
                    komut.Parameters.AddWithValue("@genel_degerlendirme", genel_degerlendirme.Text);
                    komut.Parameters.AddWithValue("@aile_okur_yazarlik", aile_okurYazarlik.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Değerlendirmeniz başarıyla güncellendi Gerçekleştirildi!", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    data.Close();
                    String sorgu = "INSERT INTO rega_report3(ogr_no,genel_degerlendirme,aile_okur_yazarlik) VALUES(@ogr_no,@genel_degerlendirme,@aile_okur_yazarlik)";
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@ogr_no", Convert.ToInt32(ogrNo.Text));
                    komut.Parameters.AddWithValue("@genel_degerlendirme", genel_degerlendirme.Text);
                    komut.Parameters.AddWithValue("@aile_okur_yazarlik", aile_okurYazarlik.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("İşleminiz Başarıyla Gerçekleştirildi!", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                baglanti.Close();
            }
            catch
            {
                MessageBox.Show("İşleminiz gerçekleşttirilirken bir sorunla karşılaşıldı! Lütfen tekrar deneyiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void ogrNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sinif.dataCagir("Select * From rega_genelDegerlendirme WHERE ogr_no="+Convert.ToInt32(ogrNo.Text));
        }
    }
}
