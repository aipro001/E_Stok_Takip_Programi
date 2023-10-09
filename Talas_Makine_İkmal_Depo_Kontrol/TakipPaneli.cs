using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Office.Interop.Excel;
using Application = System.Windows.Forms.Application;

namespace Talas_Makine_İkmal_Depo_Kontrol
{
    public partial class FrmTakipPaneli : Form
    {
        public FrmTakipPaneli()
        {
            InitializeComponent();
        }

        BaglantiSinifi bgl = new BaglantiSinifi();
        //Data Source = 192.168.1.107,1433; Initial Catalog = TalasMakineİkmalDepo; User ID = HEİTOR0101; Password=1071

        void Listele()
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Raporlama", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmTakipPaneli_Load(object sender, EventArgs e)
        {
            Listele();
            Count();
        }

        void Count()
        {
            lblCount.Text = $"TOPLAM KAYIT SAYISI={dataGridView1.RowCount - 1}";
        }

        private void btnEXCEL_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                int StartCol = 1;
                int StartRow = 1;

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }

                StartRow++;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.StackTrace);
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            dTPGirisTarihi.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dTPCikisTarihi.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMalzemeAdi.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtGrubu.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtMiktar.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtBirim.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();          
            txtTeslimEden.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtTeslimAlan.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtFirma.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtResim.Text= dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            pictureBox3.ImageLocation= dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtCikisMalzemeAdi.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            txtCikisGrubu.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            txtCikisMiktar.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            txtCikisBirim.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();          
            txtCikisTeslimEden.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
            txtCikisTeslimAlan.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
            txtKullanimYeri.Text = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
            txtMudurluk.Text = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();


            //txtKullanimYeri.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            //txtMudurluk.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();          
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Raporlama where MALZEMEADI like '%" + txtAra.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnSayfayiYenile_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            Listele();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        private void FrmTakipPaneli_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
