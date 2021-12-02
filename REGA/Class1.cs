using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REGA
{
    class Class1
    {
        Form frm;
        private SqlConnection baglanti = new SqlConnection();
        private SqlCommand komut = new SqlCommand();
        private SqlDataReader data;
        public bool tiklandi=false;
        public void menuGecis(Button btn)
        {
            Form.ActiveForm.Close();
            if (btn.Name == "btn_ogrenci")
                frm = new homePage();
            else if (btn.Name == "btn_olcum")
                frm = new olcum();
            else if (btn.Name == "btn_aktivite")
                frm = new aktiviteEkle();
            else if (btn.Name == "btn_aktiviteGoruntule")
                frm = new aktiviteGoruntule();
            else if (btn.Name == "btn_karne")
                frm = new notKarti();
            else if (btn.Name == "btn_ogrenciGoruntule")
                frm = new ogrenciGoruntule();
            else if (btn.Name == "btn_olcumGoruntule")
                frm = new olcumGoruntule();
            else if (btn.Name == "btn_tumExel")
                frm = new tum_exel();
            else if (btn.Name == "btn_genelDegerlendirme")
                frm = new genelDegerlendirme();
            frm.Show();
        }
        public void veriEkle(string sql)
        {
            try
            {
                baglantiOlustur();
                komut.Connection = baglanti;
                komut.CommandText = @sql;
                komut.ExecuteNonQuery();
                baglanti.Close();
                veriMesaj(true);
            }
            catch 
            {
                veriMesaj(false);
            }
           
        }
        public DataTable dataCagir(string sql)
        {
            DataTable dt = new DataTable();
            try {
                baglantiOlustur();
                SqlCommand komut = new SqlCommand(sql, baglanti);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                baglanti.Close();
                
            }
            catch {
                veriMesaj(false);
            }
            return dt;
        }
        public void veriMesaj(bool basarili)
        {
            if(basarili == true) MessageBox.Show("İşleminiz Başarıyla Gerçekleştirildi!", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("İşleminiz gerçekleşttirilirken bir sorunla karşılaşıldı! Lütfen tekrar deneyiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public string getConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource =  //SQL Sunucu Adı
            builder.UserID = //Kullanıcı Adı
            builder.Password =  //Kullanıcı Şifresi
            builder.InitialCatalog =  //Veri Tabanı
            return builder.ToString();
        }
        public void ogrenciCekme(ComboBox cmb)
        {
            baglantiOlustur();
            komut = new SqlCommand("Select * From rega_ogrenci",baglanti);
            data = komut.ExecuteReader();
            while (data.Read())
            {
                cmb.Items.Add(data["ogr_no"]);
            }
            baglanti.Close();
        }
        public void baglantiOlustur()
        {
            baglanti.ConnectionString = getConnectionString();
            baglanti.Open();
        }
        public void programiKapat(FormClosingEventArgs e)
        {
            if (tiklandi == false)
            {
                DialogResult mesaj = MessageBox.Show("Programı kapatmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (mesaj == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                Application.Exit();
            }
        }
    }
}
