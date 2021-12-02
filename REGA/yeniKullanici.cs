using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REGA
{
    public partial class yeniKullanici : Form
    {
        public yeniKullanici()
        {
            InitializeComponent();
        }
        Class1 sinif = new Class1();
        private void sifreler_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.PasswordChar = '*';
        }
        private void yeniKullanici_FormClosing(object sender, FormClosingEventArgs e)
        {
            sinif.programiKapat(e);
        }

        private void btn_ogrenci_Click(object sender, EventArgs e)
        {
            if (sifre.Text == sifreTekrar.Text)
            {
                string komut = "INSERT INTO rega_giris VALUES('" + kullanici_adi.Text + "','" + sifre.Text + "')";
                sinif.veriEkle(komut);
                sinif.tiklandi = true;
                sinif.menuGecis((Button)sender);
            }
            else MessageBox.Show("Yazdığınız şifreler uyuşmuyor! Lütfen tekrar deneyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
