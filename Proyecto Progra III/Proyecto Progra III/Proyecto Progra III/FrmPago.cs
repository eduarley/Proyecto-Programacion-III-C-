using CapaEntidades;
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
    public partial class FrmPago : Form
    {

        public Factura Factura { get; set; }
        public Cliente Cliente { get; set; }
        public FrmPago()
        {
            InitializeComponent();
        }

        private void FrmPago_Load(object sender, EventArgs e)
        {
            lblTotal.Text = Factura.MontoTotal().ToString("0,00")+" Colones.";
            //lblTotal.Text = Pago.montoLetras((int)Factura.MontoTotal());
        }

        private void button1_Click(object sender, EventArgs e)
        {



            MessageBox.Show("Listo!, Pasa a la caja para proceder con el pago.");

        }
    }
}
