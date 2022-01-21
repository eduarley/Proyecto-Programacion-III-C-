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
    public partial class FrmIngreso : Form
    {

        CapaLogicaNegocio.UsuarioLogica logica;
        //public Empleado Empleado { get; set; }

        public FrmIngreso()
        {
            logica = new CapaLogicaNegocio.UsuarioLogica();
            InitializeComponent();
        }


        #region MoverForm
        //para mover form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //
        #endregion MoverForm



        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void FrmIngreso_Load(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void Al_Presionar_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                /////////////////////////////////copiar y pegar el metodo que esta en el boton de ingresar///////////////


                if (string.IsNullOrEmpty(txtID.Text))
                {
                    errorProvider1.SetError(txtID, "Debe completar su ID");
                    MessageBox.Show("Debe ingresar su Identificación!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    errorProvider1.SetError(txtPassword, "Debe ingresar su contraseña");
                    MessageBox.Show("Debe ingresar su contraseña!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }



                try
                {
                    Usuario usuario = logica.Login(Convert.ToInt32(txtID.Text), txtPassword.Text);
                    if (usuario != null)
                    {
                        if (!usuario.EstadoActivo)  //si está desactvado 
                        {
                            MessageBox.Show("El usuario: " + usuario.ID + " está actualmente desactivado, por favor contactar con un operario");
                        }
                        else   //está activo
                        {

                            if (usuario.Categoria.Descripcion == "Gerente")
                            {
                                FrmMenuGerente frm = new FrmMenuGerente();
                                this.Visible = false;
                                frm.Usuario = usuario;
                                frm.Show();

                            }

                            if (usuario.Categoria.Descripcion == "Cliente")
                            {
                                FrmMenuCliente frm = new FrmMenuCliente();
                                this.Visible = false;
                                frm.Usuario = usuario;
                                frm.Show();
                            }

                            if (usuario.Categoria.Descripcion == "Cajero")
                            {
                                FrmMenuCajero frm = new FrmMenuCajero();
                                this.Visible = false;
                                frm.Cajero = usuario;
                                frm.Show();
                            }

                        }


                    }
                    else
                    {
                        MessageBox.Show("La contraseña o el usuario son incorrectos!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }




                }
                catch (Exception ex)
                {

                    MessageBox.Show("La identificación deben ser únicamente números enteros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Focus();
                    txtID.SelectAll();
                }

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Contacte con un Administrador!");
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                txtPassword.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {



            if (string.IsNullOrEmpty(txtID.Text))
            {
                errorProvider1.SetError(txtID, "Debe completar su ID");
                MessageBox.Show("Debe ingresar su Identificación!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Debe ingresar su contraseña");
                MessageBox.Show("Debe ingresar su contraseña!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }



            try
            {
                Usuario usuario = logica.Login(Convert.ToInt32(txtID.Text), txtPassword.Text);
                if (usuario != null)
                {
                    if (!usuario.EstadoActivo)  //si está desactvado 
                    {
                        MessageBox.Show("El usuario: " + usuario.ID + " está actualmente desactivado, por favor contactar con un operario");
                    }
                    else   //está activo
                    {

                        if (usuario.Categoria.Descripcion == "Gerente")
                        {
                            FrmMenuGerente frm = new FrmMenuGerente();
                            this.Visible = false;
                            frm.Usuario = usuario;
                            frm.Show();

                        }

                        if (usuario.Categoria.Descripcion == "Cliente")
                        {
                            FrmMenuCliente frm = new FrmMenuCliente();
                            this.Visible = false;
                            frm.Usuario = usuario;
                            frm.Show();
                        }

                        if (usuario.Categoria.Descripcion == "Cajero")
                        {
                            FrmMenuCajero frm = new FrmMenuCajero();
                            this.Visible = false;
                            frm.Cajero = usuario;
                            frm.Show();
                        }

                    }


                }
                else
                {
                    MessageBox.Show("La contraseña o el usuario son incorrectos!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }




            }
            catch (Exception ex)
            {

                MessageBox.Show("La identificación deben ser únicamente números enteros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
                txtID.SelectAll();
            }




        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmIngreso_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
