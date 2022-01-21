using CapaEntidades;
using CapaLogicaNegocio;
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
    public partial class FrmDetalleUsuario : Form
    {
        public Usuario Usuario { get; set; }
        CapaLogicaNegocio.UsuarioLogica logicaUsuario;
        public FrmDetalleUsuario()
        {
            InitializeComponent();
            logicaUsuario = new CapaLogicaNegocio.UsuarioLogica();
        }



        public void CargarCategoria()
        {
            CategoriaLogica log = new CategoriaLogica();
            cmbCategoria.DataSource = log.ObtenerCategorias();
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "Id";
        }



        private void FrmDetalleUsuario_Load(object sender, EventArgs e)
        {

            CargarCategoria();

            if (Usuario != null)
            {


                CargarDatosUsuario();


                if (Usuario.EstadoActivo)
                    chkActivo.Enabled = false;
                else
                    chkActivo.Enabled = true;


                txtId.Enabled = false;

                
            }

            
        }


        private void CargarDatosUsuario()
        {
            txtId.Text = Usuario.ID.ToString();
            txtNombre.Text = Usuario.Nombre;
            txtApellido.Text = Usuario.Apellido;
            txtDireccion.Text = Usuario.Direccion;
     


            CargarCategoria();

            if(Usuario.Categoria.ID==0)
                cmbCategoria.SelectedIndex = 0;
            if (Usuario.Categoria.ID == 1)
                cmbCategoria.SelectedIndex = 1;
            if (Usuario.Categoria.ID == 2)
                cmbCategoria.SelectedIndex = 2;


            txtTelefono.Text = Usuario.Telefono.ToString();
            txtCorreo.Text = Usuario.Correo;
        }







        private void btbAceptar_Click(object sender, EventArgs e)
        {

           

           try
            {




                //Validaciones
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    errorProvider1.SetError(txtId, "Debe completar el campo de Identificación");
                    MessageBox.Show("Debe completar el campo Identificación!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtId.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    errorProvider1.SetError(txtNombre, "Debe completar el campo Nombre");
                    MessageBox.Show("Debe completar el campo Nombre!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }
                if (string.IsNullOrEmpty(txtApellido.Text))
                {
                    errorProvider1.SetError(txtApellido, "Debe completar el campo Apellido");
                    MessageBox.Show("Debe completar el campo Apellido!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtApellido.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    errorProvider1.SetError(txtPassword, "Debe ingresar la contraseña");
                    MessageBox.Show("Contraseña requerida!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }


                if (cmbCategoria.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cmbCategoria, "Debe seleccionar un cargo");
                    MessageBox.Show("Debe seleccionar un cargo", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCategoria.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }

                if (string.IsNullOrEmpty(txtTelefono.Text))
                {
                    errorProvider1.SetError(txtTelefono, "Debe ingresar el teléfono");
                    MessageBox.Show("Debe agregar su número de telefóno!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }


                if (string.IsNullOrEmpty(txtCorreo.Text))
                {
                    errorProvider1.SetError(txtCorreo, "Debe ingresar el correo");
                    MessageBox.Show("Debe agregar su correo!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }

               
                if (string.IsNullOrEmpty(txtDireccion.Text))
                {
                    errorProvider1.SetError(txtDireccion, "Debe ingresar la Dirección");
                    MessageBox.Show("Debe agregar su dirección!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDireccion.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }





                //fin validaciones//














                //FACTORY           ////////////////////////////////////////


                Usuario usuario = null;


               

                if (cmbCategoria.Text=="Cliente")
                    usuario = new Cliente();
                if (cmbCategoria.Text == "Cajero")
                    usuario = new Cajero();
                if (cmbCategoria.Text == "Gerente")
                    usuario = new Gerente();






                try
                {
                    usuario.ID = Convert.ToInt32(txtId.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("La identificación debe ser números enteros");
                    txtId.Focus();
                    return;
                }

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Contrasena = txtPassword.Text;
                //usuario.Tipo = cmbPuesto.Text;
                usuario.Categoria = (Categoria)cmbCategoria.SelectedItem;
                usuario.Telefono = long.Parse(txtTelefono.Text);
                usuario.Correo = txtCorreo.Text;
                usuario.EstadoActivo = true;
                
                usuario.Direccion = txtDireccion.Text;


                if (chkActivo.Checked)
                    logicaUsuario.ActivarODesactivar(usuario);


                try
                {
                    if (Usuario != null)
                        logicaUsuario.Guardar(usuario);
                    else
                        logicaUsuario.Nuevo(usuario);
                }
                catch (Exception ex)
                {

                     MessageBox.Show("Hubo un problema al guardar: \n"+ex.Message, "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                }
               
              
                




                MessageBox.Show("Cliente guardado con éxito", "Éxito al actualizar!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //como se abre con showDialog esta linea lo cierra
                DialogResult = DialogResult.OK;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
