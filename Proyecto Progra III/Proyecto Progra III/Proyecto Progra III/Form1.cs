using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Progra_III
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmMenuGerente frm = new FrmMenuGerente();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmIngreso frm = new FrmIngreso();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmPedido frm = new FrmPedido();
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmPrueba frm = new FrmPrueba();
            frm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmDatosFacturas frm = new FrmDatosFacturas();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmReporteMasVendidos frm = new FrmReporteMasVendidos();
            frm.Show();
        }
    }
}
