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
    public partial class FrmMenuCajero : Form
    {

        public Usuario Cajero { get; set; }

        public FrmMenuCajero()
        {
             InitializeComponent();
            timer = new Timer();
            timer.Tick += new EventHandler(horaActual);
          
            timer.Enabled = true;
        }



        #region AbrirFormPNL
        private void AbrirFormEnPanel(object formHijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }
        #endregion


        private void horaActual(object ob, EventArgs evt)
        {

            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            FrmDatosFacturas frm = new FrmDatosFacturas();
            frm.Empleado = Cajero;
            frm.Show();
        }

        private void FrmMenuCajero_Load(object sender, EventArgs e)
        {
            if (Cajero != null)
            {



                label3.Text =  Cajero.Nombre;
                label4.Text =  Cajero.Apellido;
                label5.Text =  Cajero.Categoria.Descripcion;
            }

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            FrmDatosFacturas frm = new FrmDatosFacturas();
            frm.Empleado = Cajero;
            AbrirFormEnPanel(frm);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FrmMantUsuario frm = new FrmMantUsuario();
           
            AbrirFormEnPanel(frm);

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Seguro que desea salir?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FrmReportes frm = new FrmReportes();
            AbrirFormEnPanel(frm);
        }

        private void btnEnvios_Click(object sender, EventArgs e)
        {
            FrmEnvios frm = new FrmEnvios();
            AbrirFormEnPanel(frm);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FrmIngreso frm = new FrmIngreso();
                this.Cajero = null;
                frm.Show();
                this.Close();
            }
        }
    }
}
