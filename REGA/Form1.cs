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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Class1 sinif = new Class1();
        private SqlConnection baglanti = new SqlConnection();
        private SqlDataReader data;
        private void giris_Click(object sender, EventArgs e)
        {
            giris();
            if (data.Read())
            {
                homePage frm2 = new homePage();
                frm2.Show();
                this.Hide();
            }
            else MessageBox.Show("Yanlış kullanıcı adı ve/veya şifre. Lütfen tekrar deneyiniz!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            baglanti.Close();
        }
        public void baglantiOlustur()
        {
            baglanti.ConnectionString = sinif.getConnectionString();
            baglanti.Open();
        }
        public void giris()
        {
            baglantiOlustur();
            SqlCommand komut = new SqlCommand("Select * From rega_giris WHERE kullanici_adi='" + kullanici_adi.Text + "' AND sifre='" + sifre.Text + "'", baglanti);
            data = komut.ExecuteReader();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            giris();
            if (data.Read())
            {
                yeniKullanici frm2 = new yeniKullanici();
                frm2.Show();
                this.Hide();
            }
            else MessageBox.Show("Yanlış kullanıcı adı ve/veya şifre. Lütfen tekrar deneyiniz! Yeni bir kullanıcı ekleyebilmek için giriş yapmalısınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            baglanti.Close();
        }

        private void sifre_TextChanged(object sender, EventArgs e)
        {
            sifre.PasswordChar = '*';
        }
    }
}
