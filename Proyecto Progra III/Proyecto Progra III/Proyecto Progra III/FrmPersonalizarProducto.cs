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
    public partial class FrmPersonalizarProducto : Form
    {

        //public static Producto Prod { get; set; }

        private MateriaPrimaLogica matLog;
        private MateriaPrima mat;
        public Producto Producto { get; set; }
        public FrmPersonalizarProducto()
        {
            matLog = new MateriaPrimaLogica();
            //producto = new Producto();
            InitializeComponent();
        }


        public void Refrescar()
        {
            dgvDatos.DataSource = matLog.ObtenerProductos();
            dgvDatos.Columns.RemoveAt(6);
        }


        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea cancelar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void FrmPersonalizarProducto_Load(object sender, EventArgs e)
        {
            Refrescar();
        }



        private void button1_Click(object sender, EventArgs e)
        {



            if (dgvDatos.SelectedRows.Count != 0)
            {
                mat = (MateriaPrima)dgvDatos.SelectedRows[0].DataBoundItem;
                if (mat.Existencias == 0)
                {
                    MessageBox.Show("Lo sentimos, actualmente no hay existencias de este producto!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                foreach (DetalleProducto item in lstDetalleProducto.Items)
                {
                    if (mat.ID == item.oMateriaPrima.ID)
                    {
                        MessageBox.Show("Ya agregaste esta materia!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                DetalleProducto det = new DetalleProducto();
                det.Cantidad = (int)nudCantidad.Value;
                det.oMateriaPrima = mat;

                lstDetalleProducto.Items.Add(det);

            }


            decimal monto = 0;
            foreach (DetalleProducto item in lstDetalleProducto.Items)
            {
                monto += item.PrecioDetalle;
            }
            txtPrecio.Text = monto.ToString("0,00 Colones");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            lstDetalleProducto.Items.Remove((DetalleProducto)lstDetalleProducto.SelectedItem);



            decimal monto = 0;
            foreach (DetalleProducto item in lstDetalleProducto.Items)
            {
                monto += item.PrecioDetalle;
            }
            txtPrecio.Text = monto.ToString("0,00 Colones");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (lstDetalleProducto.Items.Count == 0)
            {
                MessageBox.Show("Debe escoger un producto mínimo!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                errorProvider1.SetError(dgvDatos, "Escoja algún producto");
                return;
            }else
            {
                errorProvider1.Clear();
            }




            if (MessageBox.Show("¿Seguro que desea crear este producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Categoria cat = new Categoria();
                cat.ID = 0;
                cat.Descripcion = "Personalizado";

                Producto = new Producto();
                Producto.Descripcion = "Producto Personalizado";
                Producto.Categoria = cat;  //aqui podria crear una categoria que sea personalizada en la bd
                Producto.EstadoActivo = true;
               
                Producto.ID = 0;
                
                


                foreach (DetalleProducto item in lstDetalleProducto.Items)
                {
                    Producto.AgregarDetalle(item);
                    Producto.Precio += item.PrecioDetalle;
                }


               // Prod = Producto; //setear el static para facturar
                DialogResult = DialogResult.OK;

            }

        }
    }
}
