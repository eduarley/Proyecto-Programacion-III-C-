using CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Progra_III
{
    public partial class FrmMenuGerente : Form
    {
        private FrmIngreso frmLogin = new FrmIngreso();


        public Usuario Usuario { get; set; }
        private Timer timer;
        public FrmMenuGerente()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(horaActual); 
            InitializeComponent();
            timer.Enabled = true;
        }

        #region MoverForm
        //para mover form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //
        #endregion MoverForm






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


        



        private void horaActual(object ob,EventArgs evt)
        {
            
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void FrmMenuGerente_Load(object sender, EventArgs e)
        {
            if (Usuario != null)
            {
                


                label3.Text = Usuario.Nombre;
                label4.Text = Usuario.Apellido;
                label5.Text = Usuario.Categoria.Descripcion;
            }
                

           
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMantProducto frm = new FrmMantProducto();
            frm.MdiParent = this;
            frm.Show();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMantUsuario frm = new FrmMantUsuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMantProveedor frm = new FrmMantProveedor();
            frm.MdiParent = this;
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Usuario = null;
            frmLogin.Visible = true;
            this.Close();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            FrmMantProducto frm = new FrmMantProducto();
           
            AbrirFormEnPanel(frm);
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FrmMantUsuario frm = new FrmMantUsuario();
            frm.Usuario = this.Usuario;
            AbrirFormEnPanel(frm);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            FrmMantProveedor frm = new FrmMantProveedor();

            AbrirFormEnPanel(frm);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            
            if (panelMenu.Width == 55)
            {
                panelMenu.Width = 230;
            }
            else

                panelMenu.Width = 55;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelContenedor_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnEnvios_Click(object sender, EventArgs e)
        {
            FrmEnvios frm = new FrmEnvios();
            AbrirFormEnPanel(frm);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FrmReportes frm = new FrmReportes();
            AbrirFormEnPanel(frm);
        }

        private void button2_Click(object sender, EventArgs e)
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
