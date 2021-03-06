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
    public partial class aktiviteGoruntule : Form
    {
        public aktiviteGoruntule()
        {
            InitializeComponent();
        }
        Class1 sinif = new Class1();
        private SqlConnection baglanti = new SqlConnection();
        private void aktiviteGoruntule_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sinif.dataCagir("Select * From rega_aktivite_view");
        }
        private void buttons_Click(object sender, EventArgs e)
        {
            sinif.tiklandi = true;
            sinif.menuGecis((Button)sender);
        }

        private void aktiviteGoruntule_FormClosing(object sender, FormClosingEventArgs e)
        {
            sinif.programiKapat(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                baglanti.ConnectionString = sinif.getConnectionString();
                int id = Convert.ToInt32(drow.Cells[8].Value);
                string sql = "DELETE FROM rega_aktivite WHERE id=@id";
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            dataGridView1.DataSource = sinif.dataCagir("Select * From rega_aktivite");
        }
    }
}
