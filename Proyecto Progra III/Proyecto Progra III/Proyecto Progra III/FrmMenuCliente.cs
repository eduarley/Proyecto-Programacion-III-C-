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
    public partial class FrmMenuCliente : Form
    {
        public Usuario Usuario { get; set; }

        public FrmMenuCliente()
        {
            InitializeComponent();
        }

        private void FrmMenuCliente_Load(object sender, EventArgs e)
        {
            lblNombre.Text = this.Usuario.Nombre;
            lblApellidos.Text = this.Usuario.Apellido;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmPedido frm = new FrmPedido();
            frm.Usuario = Usuario;
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmReporteMasVendidos frm = new FrmReporteMasVendidos();
            frm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FrmIngreso frm = new FrmIngreso();
                this.Usuario = null;
                frm.Show();
                this.Close();
            }
        }
    }
}
