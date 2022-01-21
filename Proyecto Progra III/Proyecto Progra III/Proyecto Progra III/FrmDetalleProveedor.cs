using CapaEntidades;
using CapaLogicaNegocio.Patrones;
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
    public partial class FrmDetalleProveedor : Form
    {
        public Proveedor Proveedor { get; set; }
        CapaLogicaNegocio.ProveedorLogica logica;
        public FrmDetalleProveedor()
        {
            InitializeComponent();
            logica = new CapaLogicaNegocio.ProveedorLogica();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmDetalleProveedor_Load(object sender, EventArgs e)
        {

            if (Proveedor != null)
            {

            CargarProveedor();
            txtID.Enabled = false;


            if (Proveedor.EstadoActivo)
                chkActivo.Enabled = false;
            else
                chkActivo.Enabled = true;

                

            }
            
        }


        private void CargarProveedor()
        {
            if (Proveedor != null)
            {

                txtID.Text = Proveedor.ID.ToString();
                txtNombre.Text = Proveedor.Nombre;
                txtDireccion.Text = Proveedor.Direccion;
                txtCorreo.Text = Proveedor.Correo;
                txtTelefono.Text = Proveedor.Telefono.ToString();

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                //Validaciones
                if (string.IsNullOrEmpty(txtID.Text))
                {
                    errorProvider1.SetError(txtID, "Debe completar el campo de Identificación");
                    MessageBox.Show("Debe completar el campo Identificación!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
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


                
                if (string.IsNullOrEmpty(txtTelefono.Text))
                {
                    errorProvider1.SetError(txtTelefono, "Debe completar el campo taléfono");
                    MessageBox.Show("Debe completar el campo teléfono", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }
                else
                {
                    errorProvider1.Clear();
                }
                if (string.IsNullOrEmpty(txtCorreo.Text))
                {
                    errorProvider1.SetError(txtCorreo, "Debe completar el campo Correo");
                    MessageBox.Show("Debe completar el campo Correo!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                //FIN VALIDACIONES



                /*Factory*/
                Proveedor prov = Factory.CrearProveedor(Convert.ToInt32(txtID.Text), txtNombre.Text, txtDireccion.Text, Convert.ToInt32(txtTelefono.Text), txtCorreo.Text, true);


              

                try
                {
                    if (Proveedor != null)
                        logica.Guardar(prov);
                    else
                        logica.Nuevo(prov);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hubo un problema al guardar: \n" + ex.Message, "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Proveedor guardado con éxito", "Éxito al actualizar!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //como se abre con showDialog esta linea lo cierra
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
