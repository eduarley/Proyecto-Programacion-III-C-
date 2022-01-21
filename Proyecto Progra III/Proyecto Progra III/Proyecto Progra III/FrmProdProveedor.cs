using CapaEntidades;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Proyecto_Progra_III
{
    public partial class FrmProdProveedor : Form
    {
        CapaLogicaNegocio.ProductoLogica productoLogica;
        CapaLogicaNegocio.MateriaPrimaLogica materiaLogica;
        CapaLogicaNegocio.CategoriaLogica categoriaLogica;
        public Producto Producto { get; set; }
        //ImagenProducto img = new ImagenProducto();
       
        private Producto prod;

        DetalleProductoLogica detLog;
        MateriaPrima mat;
        public FrmProdProveedor()
        {
            InitializeComponent();
            productoLogica = new CapaLogicaNegocio.ProductoLogica();
            materiaLogica = new CapaLogicaNegocio.MateriaPrimaLogica();
            categoriaLogica = new CapaLogicaNegocio.CategoriaLogica();
            //ListaDetalle = new List<DetalleProducto>();
            prod = new Producto();
            detLog = new DetalleProductoLogica();
        }

        #region moverForm
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        #endregion moverForm

        
        public void CargarDescripcionImagenesEnElCombo()
        {
            

            if (Producto!=null)   
                productoLogica.CargarImagenes(cmbImagenes, Producto.ID);

            
        }


        public void VerImagen()
        {

            if (Producto != null)
            {
                if (cmbImagenes.Items.Count != 0)
                {
                    cmbImagenes.SelectedIndex = 0;
                    productoLogica.VerImagen(pbImagen, (string)cmbImagenes.Text);
                }
                
            }
            
            
        }

        




        private void FrmProdProveedor_Load(object sender, EventArgs e)
        {
            
            CargarCategoriaProducto();
           
            if (Producto != null)
            {
                CargarProducto();
                txtCodigo.Enabled = false;
                

                //foreach (var item in Producto.ListaDetalle)
                //{
                //    lstDetalleProducto.Items.Add(item);

                //}

                CargarDescripcionImagenesEnElCombo();
               
                VerImagen();

                btnAceptar.Text = "Modificar Producto";
                btnAgregarImagen.Enabled = true;

                btnEliminarImagen.Visible = true;
            }
            else
            {
                //hacer metodo CargarDatosProducto
                btnAceptar.Text = "Crear Producto";
                btnEliminarImagen.Visible = false;
            }
            Refrescar();
            
        }


        private void CargarCategoriaProducto()
        {
            cmbCategoria.DataSource = categoriaLogica.ObtenerCategoriasProducto();
            cmbCategoria.SelectedIndex = 1;
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "ID";
        }



        private void CargarProducto()
        {
            txtCodigo.Text = Producto.ID.ToString();
            txtDescripcion.Text = Producto.Descripcion;
            txtPrecio.Text = Producto.Precio.ToString("0,00");
            CargarCategoriaProducto();



            

            foreach (var item in detLog.ObtenerMateriasPrimasPorProducto(Producto))
            {
                Producto.AgregarDetalle(item);
            }

            foreach (var item in Producto.ListaDetalle)
            {
                lstDetalleProducto.Items.Add(item);
            }



            cmbCategoria.SelectedValue = Producto.Categoria.ID;
            
        }



        public void Refrescar()
        {
            dgvDatos.DataSource = materiaLogica.ObtenerProductos();
            dgvDatos.Columns.RemoveAt(6);  //para que no se vea la columna de oProveedor


            cmbImagenes.Items.Clear();
            CargarDescripcionImagenesEnElCombo();
            VerImagen();
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
                if (mat.Existencias<nudCantidad.Value)
                {
                    MessageBox.Show("Lo sentimos, no hay suficientes existencias de este producto", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (mat.EstadoActivo == false)
                {
                    MessageBox.Show("Lo sentimos, actualmente la materia está desactivada", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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




                decimal monto = 0;
                foreach (DetalleProducto item in lstDetalleProducto.Items)
                {
                    monto += item.PrecioDetalle;
                }
                txtPrecio.Text = monto.ToString("0,00");
                //lstMateriaPrima.DisplayMember = "Descripcion";
                //lstMateriaPrima.ValueMember = "ID";

                //txtPrecioFinal.Text=monto+Convert.tod

            }



      }

        




       

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {



            #region validaciones


            int codigo;
            double precioExtra;
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Debe ingresar el código");
                txtCodigo.Focus();
                return;
            }else
            {
                try
                {
                     codigo = Convert.ToInt32(txtCodigo.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("El código debe ser únicamente numérico");
                    txtCodigo.Focus();
                    return;
                }

            }


            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar la descripción");
                txtDescripcion.Focus();
                return;
            }

            if (txtExtra.Text=="0")
            {
                if (MessageBox.Show("¿No ha agregado un monto extra, desea continuar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    txtExtra.Focus();
                    return;
                }
            }else
            {
                try
                {
                    precioExtra = Convert.ToDouble(txtExtra.Text);

                }
                catch (Exception)
                {

                    MessageBox.Show("El monto extra debe ser numéro");
                    txtExtra.Focus();
                    return;
                }
            }



            #endregion






            //hacer validaciones!!!!!!!!!!!
            //////////FACTORY////////////////



            if (Producto != null)               ///espacio para modificar producto
                {
                    try
                    {




                   


                        prod.ID = Convert.ToInt32(txtCodigo.Text);
                        prod.Categoria = (Categoria)cmbCategoria.SelectedItem;
                        prod.Descripcion = txtDescripcion.Text;

                        prod.EstadoActivo = true;
                    

                         productoLogica.Guardar(prod);
                       
                   


                        foreach (var item in Producto.ListaDetalle)
                        {
                            detLog.EliminarDetalle(Producto, item.oMateriaPrima); //borro para sobreescribir
                        }


                        foreach (DetalleProducto item in lstDetalleProducto.Items)
                        {
                            prod.AgregarDetalle(item);
                            try
                            {

                           

                                detLog.NuevoDetalleProducto(item, prod, item.oMateriaPrima);
                              
                            

                                btnAgregarImagen.Enabled = true;
                                Refrescar();
                                richTextBox1.Text = prod.ToString();
                            }


                            catch (Exception ex)
                            {

                                MessageBox.Show("Error al guardar debido a " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        


                    }   //////////////////////////////////////////////fin transaccion//////////////////////////



                        MessageBox.Show("Éxito al actualizar! ", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar debido a " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



                prod.MontoExtra = Convert.ToDecimal(txtExtra.Text);
                productoLogica.ManoObra(prod.ID, Convert.ToDecimal(txtExtra.Text));
                richTextBox1.Text = prod.ToString();


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                }
                else                       //espacio para  agregar producto
                {





                if (lstDetalleProducto.Items.Count != 0)
                {
                    //using (TransactionScope trans = new TransactionScope())     ///TRANSACCION!!
                    //{


                        errorProvider1.Clear();
                        prod.ID = int.Parse(txtCodigo.Text);
                        prod.Categoria = (Categoria)cmbCategoria.SelectedItem;
                        prod.Descripcion = txtDescripcion.Text;
                        prod.EstadoActivo = true;

                        try
                        {

                            productoLogica.Nuevo(prod);



                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Error al guardar debido a " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }



                        foreach (DetalleProducto item in lstDetalleProducto.Items)
                        {
                            prod.AgregarDetalle(item);
                            try
                            {

                                
                                detLog.NuevoDetalleProducto(item, prod, item.oMateriaPrima);

                                btnAgregarImagen.Enabled = true;
                                Refrescar();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al guardar debido a " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }


                        }
                        MessageBox.Show("Éxito al guardar!", "Èxito", MessageBoxButtons.OK, MessageBoxIcon.Information);




                        



                    btnAceptar.Enabled = false;
                    //}//fin transaccion!!

                    }
                    else
                    {
                        MessageBox.Show("No ha agregado ningún material!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        errorProvider1.SetError(dgvDatos, "Debe agregar materiales!");
                        return;
                    }


                prod.MontoExtra = Convert.ToDecimal(txtExtra.Text);
                productoLogica.ManoObra(prod.ID, Convert.ToDecimal(txtExtra.Text));
                richTextBox1.Text = prod.ToString();
            }


            



            Refrescar();


            //DialogResult = DialogResult.OK;



        }

        private void button2_Click(object sender, EventArgs e)
        {   


            
                lstDetalleProducto.Items.Remove((DetalleProducto)lstDetalleProducto.SelectedItem);




            decimal monto = 0;
            foreach (DetalleProducto item in lstDetalleProducto.Items)
            {
                monto += item.PrecioDetalle;
            }
            txtPrecio.Text = monto.ToString("0,00");


        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            /*
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(txtCodigo.Text, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Red, Brushes.White);
            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imagenTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imagenTemporal, new Size(new Point(200, 200)));
            PnlCodigo.BackgroundImage = imagen;


            imagen.Save("imagen.png", ImageFormat.Png);

            btnGuardar.Enabled = true;
            */


        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {


                FrmAgregarImagen frm = new FrmAgregarImagen();


                if(prod!=null)
                    frm.Producto = prod;


                prod.ID = int.Parse(txtCodigo.Text);
                prod.Categoria = (Categoria)cmbCategoria.SelectedItem;
                prod.Descripcion = txtDescripcion.Text;
                prod.EstadoActivo = true;

                var resultado = frm.ShowDialog();

                if (resultado == DialogResult.OK)
                    Refrescar();



            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ParaMoverFrame(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbImagenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            productoLogica.VerImagen(pbImagen, (string)cmbImagenes.SelectedItem);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (pbImagen.Image == null)
                {
                    MessageBox.Show("Este producto no tiene imágenes!");
                    return;
                }

                if (MessageBox.Show("¿Seguro que desea eliminar la imagen?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    productoLogica.EliminarImagen(cmbImagenes.Text);
                    MessageBox.Show("Éxito al eliminar");
                    pbImagen.Image = null;
                    cmbImagenes.Items.Clear();
                }
                    
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al eliminar imagen debido a " + ex.Message);
            }
        }
    }
}
