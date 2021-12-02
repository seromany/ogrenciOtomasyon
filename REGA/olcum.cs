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
    public partial class olcum : Form
    {
        public olcum()
        {
            InitializeComponent();
        }
        Class1 sinif = new Class1();
        private string okurYazarlik;
        private SqlCommand komut;
        private SqlCommand komut2;
        private SqlConnection baglanti = new SqlConnection();
        private string sorgu;
        private int olcumId;
        private void buttons_Click(object sender, EventArgs e)
        {
            sinif.tiklandi = true;
            sinif.menuGecis((Button)sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sorgu = "INSERT INTO rega_olcum(olcum,ogr_no,basit_soru,derin_soru,basit_puan,derin_puan,toplam_okudugu_kelime, yerine_koyma,ekleme,ihmal_etme,takilma,tersine_cevirme,sure,aciklama,ev_okur_yazarlik) VALUES('" + olcum_no.Text + "','" + ogrNo.Text + "'," + Convert.ToInt32(bsoruSayisi.Text) + "," + Convert.ToInt32(dsoruSayisi.Text) + "," + Convert.ToInt32(basitPuan.Text) + "," + Convert.ToInt32(derinPuan.Text) + "," + Convert.ToInt32(toplamKelime.Text) + "," + Convert.ToInt32(aHatasi.Text) + "," + Convert.ToInt32(bHatasi.Text) + "," + Convert.ToInt32(cHatasi.Text) + "," + Convert.ToInt32(dHatasi.Text) + "," + Convert.ToInt32(eHatasi.Text) + "," + Convert.ToInt32(sure.Text) + ",'" + aciklama.Text + "','" + okurYazarlik + "')";
            sinif.veriEkle(sorgu);
            baglanti.ConnectionString = sinif.getConnectionString();
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("SELECT id FROM rega_olcum WHERE ogr_no=@ogr_no AND olcum=@olcum",baglanti);
            komut3.Parameters.AddWithValue("@ogr_no", ogrNo.Text);
            komut3.Parameters.AddWithValue("@olcum", olcum_no.Text);
            olcumId = Convert.ToInt32(komut3.ExecuteScalar());/*Eklediğimiz ölçümün id bilgisini alıyoruz*/

            double DogruKelime = Convert.ToInt32(toplamKelime.Text) - (Convert.ToInt32(aHatasi.Text) + Convert.ToInt32(bHatasi.Text) + Convert.ToInt32(cHatasi.Text) +
                Convert.ToInt32(dHatasi.Text) + Convert.ToInt32(eHatasi.Text));
            double kelime_anlama = (100 * DogruKelime) / Convert.ToInt32(toplamKelime.Text);
            double dakika_hiz = (Convert.ToDouble(toplamKelime.Text) * 60) / Convert.ToDouble(sure.Text);
            double saniye_hiz = (Convert.ToDouble(toplamKelime.Text) * 1) / Convert.ToDouble(sure.Text);
            double basitTavanPuani = Convert.ToInt32(bsoruSayisi.Text) * 2;
            double derinTavanPuani = Convert.ToInt32(dsoruSayisi.Text) * 3;
            double toplamTavanPuan = basitTavanPuani + derinTavanPuani;
            double basit_anlama = (Convert.ToInt32(basitPuan.Text) * 100) / basitTavanPuani;
            double derin_anlama = (Convert.ToInt32(derinPuan.Text) * 100) / derinTavanPuani;
            double toplam_anlama = ((Convert.ToInt32(basitPuan.Text) + Convert.ToInt32(derinPuan.Text)) * 100) / toplamTavanPuan;
            kelime_anlama = ondalikAzalt(kelime_anlama);
            dakika_hiz = ondalikAzalt(dakika_hiz);
            saniye_hiz = ondalikAzalt(saniye_hiz);
            basit_anlama = ondalikAzalt(basit_anlama);
            derin_anlama = ondalikAzalt(derin_anlama);
            toplam_anlama = ondalikAzalt(toplam_anlama);

            sorgu = "INSERT INTO rega_not(ogr_no,kelime_anlama,basit_anlama,derin_anlama,toplam_anlama,dakika_hiz,saniye_hiz, yorum, prozodi, olcumId) VALUES(@ogr_no,@kelime_anlama,@basit_anlama,@derin_anlama,@toplam_anlama,@dakika_hiz,@saniye_hiz, @yorum,@prozodi, @olcumId)";
            komut = new SqlCommand(sorgu,baglanti);
            komut.Parameters.AddWithValue("@ogr_no",ogrNo.Text);
            komut.Parameters.AddWithValue("@kelime_anlama", kelime_anlama);
            komut.Parameters.AddWithValue("@basit_anlama", basit_anlama);
            komut.Parameters.AddWithValue("@derin_anlama", derin_anlama);
            komut.Parameters.AddWithValue("@toplam_anlama", toplam_anlama);
            komut.Parameters.AddWithValue("@dakika_hiz", dakika_hiz);
            komut.Parameters.AddWithValue("@saniye_hiz", saniye_hiz);
            komut.Parameters.AddWithValue("@yorum", yorum.Text);
            komut.Parameters.AddWithValue("@prozodi", Convert.ToDouble(prozodi.Text));
            komut.Parameters.AddWithValue("@olcumId", olcumId);
            komut.ExecuteNonQuery();
            sorgu = "INSERT INTO rega_report2(ogr_no,kelime_anlama,basit_anlama,derin_anlama,toplam_anlama,dakika_hiz,saniye_hiz, yorum,yerine_koyma,ekleme,ihmal_etme,takilma,tersine_cevirme,prozodi,olcumId) VALUES(@ogr_no,@kelime_anlama,@basit_anlama,@derin_anlama,@toplam_anlama,@dakika_hiz,@saniye_hiz, @yorum,@yerine_koyma,@ekleme,@ihmal_etme,@takilma,@tersine_cevirme,@prozodi,@olcumId)";
            komut2 = new SqlCommand(sorgu, baglanti);
            komut2.Parameters.AddWithValue("@ogr_no", ogrNo.Text);
            komut2.Parameters.AddWithValue("@kelime_anlama", kelime_anlama);
            komut2.Parameters.AddWithValue("@basit_anlama", basit_anlama);
            komut2.Parameters.AddWithValue("@derin_anlama", derin_anlama);
            komut2.Parameters.AddWithValue("@toplam_anlama", toplam_anlama);
            komut2.Parameters.AddWithValue("@dakika_hiz", dakika_hiz);
            komut2.Parameters.AddWithValue("@saniye_hiz", saniye_hiz);
            komut2.Parameters.AddWithValue("@yorum", yorum.Text);
            komut2.Parameters.AddWithValue("@yerine_koyma", Convert.ToInt32(aHatasi.Text));
            komut2.Parameters.AddWithValue("@ekleme", Convert.ToInt32(bHatasi.Text));
            komut2.Parameters.AddWithValue("@ihmal_etme", Convert.ToInt32(cHatasi.Text));
            komut2.Parameters.AddWithValue("@takilma", Convert.ToInt32(dHatasi.Text));
            komut2.Parameters.AddWithValue("@tersine_cevirme", Convert.ToInt32(eHatasi.Text));
            komut2.Parameters.AddWithValue("@prozodi", Convert.ToDouble(prozodi.Text));
            komut2.Parameters.AddWithValue("@olcumId", olcumId);
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }

        public double ondalikAzalt(double sayi)
        {
            string sayiDuzgun = sayi.ToString("0.##");
            return Convert.ToDouble(sayiDuzgun);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdn = (RadioButton)sender;
            okurYazarlik = rdn.Text;
        }

        private void olcum_FormClosing(object sender, FormClosingEventArgs e)
        {
            sinif.programiKapat(e);
        }

        private void olcum_Load(object sender, EventArgs e)
        {
            sinif.ogrenciCekme(ogrNo);
        }
    }
}
