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
using System.Transactions;
using System.Windows.Forms;

namespace Proyecto_Progra_III
{


    public partial class FrmPedido : Form
    {
        public Usuario Usuario { get; set; }
        private ProductoLogica productoLog;
        private FacturaLogica factLog;
        public Producto ProductoPERSONALIZADO { get; set; }

        //DetalleFactura det;
        private Factura factura;
        public FrmPedido()
        {
            InitializeComponent();
            productoLog = new ProductoLogica();
            factLog = new FacturaLogica();
        }

        private void FrmPedido_Load(object sender, EventArgs e)
        {
            CargarProductos();
            if (Usuario != null)
            {
                lblUsuario.Text = Usuario.Nombre + " " + Usuario.Apellido;
            }


            if (cmbImagenes.Items.Count != 0)
                cmbImagenes.SelectedIndex = 0;

            //cmbImagenes.SelectedIndex = 1;



            CargarDescripcionImagenesEnElCombo();

            if (cmbImagenes.Items.Count != 0)
            {
                cmbImagenes.SelectedIndex = 0;
            }

            VerImagen();

        }

        public void CargarDescripcionImagenesEnElCombo()
        {

            if (dgvDatos.SelectedRows.Count != 0)
            {
                cmbImagenes.Items.Clear();
                Producto pro = (Producto)dgvDatos.SelectedRows[0].DataBoundItem;
                productoLog.CargarImagenes(cmbImagenes, pro.ID);
            }

        }
        public void VerImagen()
        {
            if (cmbImagenes.SelectedIndex != -1)
            {
                productoLog.VerImagen(pbImagen, (string)cmbImagenes.Text);
            }

            //cmbImagenes.SelectedIndex = 0;
        }
        public void CargarProductos()
        {
            dgvDatos.DataSource = productoLog.ObtenerProductosActivos();
            dgvDatos.Columns.RemoveAt(4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {

            using (TransactionScope trans = new TransactionScope())
            {


                try
                {
                    if (dgvDatos.SelectedRows.Count != 0)
                    {
                        Producto prod = (Producto)dgvDatos.SelectedRows[0].DataBoundItem;

                        foreach (DetalleFactura item in lstDetalleProducto.Items)
                        {
                            if (prod.ID == item.Producto.ID)
                            {
                                MessageBox.Show("Ya agregaste este producto!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        



                        /*Factory*/
                        DetalleFactura det = Factory.CrearDetalleFactura(prod, (int)nudCantidad.Value);   ///////////FACTORY

                        //foreach (DetalleProducto item in prod.ListaDetalle)
                        //{
                        //    if (item.oMateriaPrima.Existencias != 0)
                        //    {
                        //        MessageBox.Show("Lo sentimos, no tenemos suficiente material de " + item.oMateriaPrima.Descripcion);
                        //        return;
                        //    }
                        //}

                        







                        lstDetalleProducto.Items.Add(det);


                        decimal monto = 0;
                        foreach (DetalleFactura item in lstDetalleProducto.Items)
                        {

                            monto += item.PrecioDetalle();
                            txtTotalPagar.Text = monto.ToString("0,00");

                        }


                        //factura.AgregarDetalle(det);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }





                trans.Complete();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmPersonalizarProducto frm = new FrmPersonalizarProducto();
            var resultado=frm.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                if(frm.Producto!=null)
                    this.ProductoPERSONALIZADO = frm.Producto;

                CargarProductoPersonalizado();
                
            }
                
        }

        public void CargarProductoPersonalizado()
        {
            dgvDatos.DataSource = null;




            //dgvDatos.Columns.Add(new DataGridViewColumn());
            //dgvDatos.Columns.Add(new DataGridViewColumn());
            //dgvDatos.Columns.Add(new DataGridViewColumn());
            //dgvDatos.Columns.Add(new DataGridViewColumn());
            //dgvDatos.Columns.Add(new DataGridViewColumn());
            //dgvDatos.Rows.Add(ProductoPERSONALIZADO);


            List<Producto> lista = new List<Producto>();
            lista.Add(ProductoPERSONALIZADO);
            dgvDatos.DataSource = lista;

            //if (lstDetalleProducto.Items.Count != 0)
                //lstDetalleProducto.Items.Add(dgvDatos.SelectedRows[0].DataBoundItem);
            //dgvDatos.DataSource = ProductoPERSONALIZADO;
        }



        private void button5_Click(object sender, EventArgs e)
        {
            lstDetalleProducto.Items.Remove((DetalleFactura)lstDetalleProducto.SelectedItem);
            //factura.ListaDetalleFactura.Clear();



            if (lstDetalleProducto.Items.Count == 0)
                txtTotalPagar.Clear();

            decimal monto = 0;
            foreach (DetalleFactura item in lstDetalleProducto.Items)
            {

                monto += item.PrecioDetalle();
                txtTotalPagar.Text = monto.ToString("0,00");

            }


        }

        private void button7_Click(object sender, EventArgs e)
        {


            using (TransactionScope trans = new TransactionScope())
            {





                if (lstDetalleProducto.Items.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar algún producto", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorProvider1.SetError(dgvDatos, "Debe seleccionar un producto!");
                    return;
                }
                errorProvider1.Clear();



                try
                {
                    if (MessageBox.Show("¿Seguro que desea realizar la compra?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {





                        /*Factory*/
                        factura = Factory.CrearFactura(factLog.IDMayor() + 1, this.Usuario, DateTime.Now, "Pendiente", null, null, null, 0);  //////////////FACTORY
                                                                                                                                              

                        factura.ListaDetalleFactura.Clear();

                        foreach (DetalleFactura item in lstDetalleProducto.Items)
                        {
                            factura.AgregarDetalle(item);
                        }




                        try
                        {

                            factLog.Nuevo(factura);

                            DetalleFacturaLogica detFacLog = new DetalleFacturaLogica();
                            foreach (var item in factura.ListaDetalleFactura)
                            {
                                if (ProductoPERSONALIZADO == null) //por si se hizo un producto personalizado
                                    detFacLog.Nuevo(item, factura);
                            }



                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Lo sentimos, no hay suficientes materiales para elaborar su pedido " , "Hubo un fallo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        richTextBox1.Text = factura.ToString();
                        MessageBox.Show("Éxito al Comprar!, Favor pasar a la caja", "Éxito!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        return;
                    }



                }
                catch (Exception ex)
                {

                    throw;
                }



                trans.Complete();
            }



        }

        private void button6_Click(object sender, EventArgs e)
        {


            if (lstDetalleProducto.Items.Count == 0)
            {
                MessageBox.Show("Debe seleccionar algún producto", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider1.SetError(dgvDatos, "Debe seleccionar un producto!");
                return;
            }
            errorProvider1.Clear();



            try
            {
                if (MessageBox.Show("¿Seguro que desea realizar el encargo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {




/*Factory*/         factura = Factory.CrearFactura(factLog.IDMayor() + 1, this.Usuario, DateTime.Now, "Pendiente", null, null, null, 0);


                    factura.ListaDetalleFactura.Clear();

                    foreach (DetalleFactura item in lstDetalleProducto.Items)
                    {
                        factura.AgregarDetalle(item);
                    }



                    //richTextBox1.Text = factura.ToString();

                   
                    FrmEncargo frm = new FrmEncargo();
                    frm.Factura = factura;
                    frm.Cliente = Usuario as Cliente;
                    frm.ShowDialog();
                    //frm.ProductoPERSONALIZADO = this.ProductoPERSONALIZADO;

                }
                else
                {
                    return;
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: "+ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {

            pbImagen.Image = null;
         

            CargarDescripcionImagenesEnElCombo();

            if (cmbImagenes.Items.Count != 0)
                cmbImagenes.SelectedIndex = 0;
        }

        private void cmbImagenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            productoLog.VerImagen(pbImagen, (string)cmbImagenes.SelectedItem);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
