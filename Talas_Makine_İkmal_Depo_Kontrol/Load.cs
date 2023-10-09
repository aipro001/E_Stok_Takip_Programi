﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Talas_Makine_İkmal_Depo_Kontrol
{
    public partial class FrmLoad : Form
    {
        public FrmLoad()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 2; // Timer her çalıştığında panel 2 genişliği artar.
            if (panel2.Width >= 412) // Form genişliği değerine ulaştığı durumda.
            {
                timer1.Stop(); // Sayacımızı durdurduk.
                FrmGiris fr = new FrmGiris(); // yeni formu f2 adında tanıttık.
                fr.Show();    // Yeni formu Ekrana gösterir.
                this.Hide();  // Mevcut formu gizler.
            }
        }
    }
}
