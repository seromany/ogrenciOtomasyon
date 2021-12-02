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
    public partial class notKarti : Form
    {
        public notKarti()
        {
            InitializeComponent();
        }
        Class1 sinif = new Class1();
        SqlDataAdapter adtr;
        DataTable dt = new DataTable();
        private SqlConnection baglanti = new SqlConnection();
        CrystalReport1 rapor = new CrystalReport1();
        CrystalReport2 rapor2 = new CrystalReport2();
        private void notKarti_Load(object sender, EventArgs e)
        {
            rapor.SetDatabaseLogon("regaAka", "NuNYWk987");
            rapor2.SetDatabaseLogon("regaAka", "NuNYWk987");
            sinif.ogrenciCekme(ogr_no);
        }
        private void notKarti_FormClosing(object sender, FormClosingEventArgs e)
        {
            sinif.programiKapat(e);
        }

        private void ogr_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            baglanti.ConnectionString = sinif.getConnectionString();
            adtr = new SqlDataAdapter("SELECT * FROM rega_report WHERE ogr_no=" + Convert.ToInt32(ogr_no.Text) + "", baglanti);
            adtr.Fill(dt);
            rapor.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rapor;
            */
        }
        private void buttons_Click(object sender, EventArgs e)
        {
            sinif.tiklandi = true;
            sinif.menuGecis((Button)sender);
        }

        private void olcumSayisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.ConnectionString = sinif.getConnectionString();
            adtr = new SqlDataAdapter("SELECT * FROM rega_report WHERE ogr_no=" + Convert.ToInt32(ogr_no.Text) + "", baglanti);
            adtr.Fill(dt);
            if (olcumSayisi.Text == "6 ve daha az")
            {
                rapor.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rapor;
            }
            else
            {
                rapor2.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rapor2;
            }
        }
    }
}
