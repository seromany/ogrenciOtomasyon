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
using Exel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace REGA
{
    public partial class tum_exel : Form
    {
        public tum_exel()
        {
            InitializeComponent();
        }
        Class1 sinif = new Class1();
        private SqlConnection baglanti = new SqlConnection();
        private string sql;
        private void tum_exel_Load(object sender, EventArgs e)
        {
            sinif.ogrenciCekme(ogr_no);
            dataGridView1.DataSource = sinif.dataCagir("Select * From rega_tum_excel");
        }
        private void buttons_Click(object sender, EventArgs e)
        {
            sinif.tiklandi = true;
            sinif.menuGecis((System.Windows.Forms.Button)sender);
        }
        private void tum_exel_FormClosing(object sender, FormClosingEventArgs e)
        {
            sinif.programiKapat(e);
        }

        private void ogr_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql2 = sql + " WHERE ogr_no='"+ogr_no.Text+"'";
            dataGridView1.DataSource = sinif.dataCagir(sql2);
        }

        private void tablo_bilgi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ogr_no.Enabled = true;
            switch (tablo_bilgi.Text)
            {
                case "Tüm Hepsi":
                    sql = "Select * From rega_tum_excel";
                    break;
                case "Ölçümler":
                    sql = "Select * From rega_olcum_excel";
                    break;
                default:
                    sql = "Select * From rega_not_excel";
                    break;
            }
            dataGridView1.DataSource = sinif.dataCagir(sql);
        }

        private void btn_exel_aktar_Click(object sender, EventArgs e)
        {
            Exel.Application exelDosya = new Exel.Application();
            exelDosya.Visible = true;
            object Missing = Type.Missing;
            Workbook calismaKitabi = exelDosya.Workbooks.Add(Missing);
            Worksheet sheet1 = (Worksheet)calismaKitabi.Sheets[1];
            int sutun = 1;
            int satir = 1;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Range myrange = (Range)sheet1.Cells[satir, sutun + i];
                myrange.Value2 = dataGridView1.Columns[i].HeaderText;
            }
            satir++;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range myrange = (Range)sheet1.Cells[satir + i, sutun + j];
                    myrange.Value2 = dataGridView1[j, i].Value == null ? "": dataGridView1[j, i].Value;
                    myrange.Select();
                }
            }
        }
    }
}
