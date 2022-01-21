using CapaEntidades;
using CapaLogicaNegocio;
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
    public partial class FrmDetalleMateriaPrima : Form
    {
        public MateriaPrima MateriaPrima { get; set; }
        private ProveedorLogica ProvLog;
        private MateriaPrimaLogica matLog;
        public FrmDetalleMateriaPrima()
        {
            InitializeComponent();
            ProvLog = new ProveedorLogica();
            matLog = new MateriaPrimaLogica();
        }

        private void CargarDatosMateria()
        {
            if (MateriaPrima != null)
            {
                txtCodigo.Text = MateriaPrima.ID.ToString();
                txtDescripcion.Text = MateriaPrima.Descripcion;
                txtPrecio.Text = MateriaPrima.Precio.ToString("0,00");
                txtExistencias.Text = MateriaPrima.Existencias.ToString();
                txtCodigo.Enabled = false;
                btnGuardar.Text = "Modificar";
            }
            else
            {
                btnGuardar.Text = "Guardar";
            }

        }


        private void CargarProveedores()
        {

            cmbProveedor.DisplayMember = "Nombre";
            cmbProveedor.ValueMember = "ID";

            if (MateriaPrima == null)
            {
                cmbProveedor.DataSource = ProvLog.ObtenerProveedores();

            }
            else
            {
                cmbProveedor.DataSource = ProvLog.ObtenerProveedores();

                cmbProveedor.SelectedValue = MateriaPrima.oProveedor.ID;

                //foreach (Proveedor item in cmbProveedor.Items)
                //{
                //    while (item.Nombre != MateriaPrima.Proveedor)
                //        cmbProveedor.SelectedIndex++;
                //}






            }

           


        }
        private void FrmDetalleMateriaPrima_Load(object sender, EventArgs e)
        {
            CargarDatosMateria();
            CargarProveedores();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


            //validaciones
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider1.SetError(txtCodigo, "");
                MessageBox.Show("Debe ingresar un código");
                txtCodigo.Focus();
                return;
            }else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider1.SetError(txtDescripcion, "");
                MessageBox.Show("Debe ingresar una descripción");
                txtDescripcion.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                errorProvider1.SetError(txtPrecio, "");
                MessageBox.Show("Debe ingresar un precio");
                txtPrecio.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtExistencias.Text))
            {
                errorProvider1.SetError(txtExistencias, "");
                MessageBox.Show("Debe ingresar la cantidad de existencias");
                txtExistencias.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }

            //fin validaciones


            


            try
            {
/*Factory*/     MateriaPrima mat = Factory.CrearMateriaPrima(Convert.ToInt32(txtCodigo.Text), txtDescripcion.Text, (Proveedor)cmbProveedor.SelectedItem, cmbProveedor.Text, Convert.ToDecimal(txtPrecio.Text), Convert.ToInt32(txtExistencias.Text), true);



                matLog.Guardar(mat);
                MessageBox.Show("Éxito al guardar!");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error debido a: "+ex.Message);
            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea Cerrar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
         }
    }
}
