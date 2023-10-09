using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Talas_Makine_İkmal_Depo_Kontrol
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        BaglantiSinifi bgl = new BaglantiSinifi();

        private void btnKayit_Click(object sender, EventArgs e)
        {
            FrmPersonelKayit fr = new FrmPersonelKayit(); // yeni formu f2 adında tanıttık.
            fr.Show();    // Yeni formu Ekrana gösterir.
            this.Hide();  // Mevcut formu gizler.
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSifreYenileme fr = new FrmSifreYenileme(); // yeni formu f2 adında tanıttık.
            fr.Show();    // Yeni formu Ekrana gösterir.
            this.Hide();  // Mevcut formu gizler.
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        private void FrmGiris_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (
                  txtKullaniciAdi.Text == "" || txtSifre.Text == "" ||
                  txtKullaniciAdi.Text == String.Empty || txtSifre.Text == String.Empty
               )
            {
                txtKullaniciAdi.BackColor = Color.Yellow;
                txtSifre.BackColor = Color.Yellow;
                MessageBox.Show("Sarı Rekli Alanları Boş Geçemezsiniz", "Boş Alan Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlConnection baglanti = new SqlConnection(bgl.Adres);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Select * From Tbl_KullaniciGirisi where KULLANICIADI=@p1 and SIFRE=@p2", baglanti);
                komut.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut.ExecuteNonQuery();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmTakipPaneli fr = new FrmTakipPaneli();
                    fr.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                baglanti.Close();
            }
        }
    }
}
