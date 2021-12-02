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
    public partial class homePage : Form
    {
        public homePage()
        {
            InitializeComponent();
        }
        Class1 sinif = new Class1();
        private void buttons_Click(object sender, EventArgs e)
        {
            sinif.tiklandi = true;
            sinif.menuGecis((Button)sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string komut = "INSERT INTO rega_ogrenci(ogr_no,ogr_ad,ogr_soyad,ogr_tel,ogr_mail,adres,sinif_duzeyi) VALUES(" + Convert.ToInt32(ogrNo.Text) + ",'" + ogrAd.Text + "','" + ogrSoyad.Text + "','" + veliTel.Text + "','" + veliMail.Text + "','" + adres.Text + "',"+Convert.ToInt32(sinifDuzeyi.Text)+")";
            sinif.veriEkle(komut);
        }

        private void homePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            sinif.programiKapat(e);
        }
    }
}
