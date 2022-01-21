using CapaEntidades;
using CapaLogicaNegocio;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Progra_III
{
    public partial class FrmMantProducto : Form
    {
        CapaLogicaNegocio.ProductoLogica productoLogica;
        public FrmMantProducto()
        {
            InitializeComponent();
            productoLogica = new CapaLogicaNegocio.ProductoLogica();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {


            try
            {

             FrmProdProveedor frm = new FrmProdProveedor();

                var resultado = frm.ShowDialog();
                if (resultado == DialogResult.OK)
                    Refrescar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }



        public void CargarQR()
        {


            if (dgvDatos.SelectedRows.Count != 0)
            {
                Producto prod = (Producto)dgvDatos.SelectedRows[0].DataBoundItem;
                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                QrCode qrCode = new QrCode();
                qrEncoder.TryEncode(prod.ID.ToString(), out qrCode);
                GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);
                MemoryStream ms = new MemoryStream();
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
                var imagenTemporal = new Bitmap(ms);
                var imagen = new Bitmap(imagenTemporal, new Size(new Point(100, 100)));
                pictureBox2.BackgroundImage = imagen;

            }
     
        }


        private void FrmMantProducto_Load(object sender, EventArgs e)
        {
            Refrescar();
            //cmbImagenes.SelectedIndex = 0;
            CargarDescripcionImagenesEnElCombo();
            VerImagen();
        }
        public void Refrescar()
        {
            dgvDatos.DataSource = productoLogica.ObtenerProductos();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count != 0)
            {
                Producto prod = (Producto)dgvDatos.SelectedRows[0].DataBoundItem;
                FrmProdProveedor frm = new FrmProdProveedor();
                frm.Producto= prod;
                var resultado = frm.ShowDialog();
                if (resultado == DialogResult.OK)
                    Refrescar();
            }

        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            CargarQR();
            pbImagen.Image = null;
            CargarMateriasPrimasPorProducto();
            
            CargarDescripcionImagenesEnElCombo();

            if(cmbImagenes.Items.Count!=0)
                cmbImagenes.SelectedIndex = 0;

           
      

        }


        private void CargarMateriasPrimasPorProducto()
        {
            DetalleProductoLogica detLogica = new DetalleProductoLogica();
            if (dgvDatos.SelectedRows.Count != 0)
            {
                Producto pro = (Producto)dgvDatos.SelectedRows[0].DataBoundItem;
                lstMateriales.DataSource = detLogica.ObtenerMateriasPrimasPorProducto(pro);
             
            }

                
        }


        public void CargarDescripcionImagenesEnElCombo()
        {

            if (dgvDatos.SelectedRows.Count != 0)
            {
                cmbImagenes.Items.Clear();
                Producto pro = (Producto)dgvDatos.SelectedRows[0].DataBoundItem;
                productoLogica.CargarImagenes(cmbImagenes, pro.ID);
            }
                
        }
        public void VerImagen()
        {
            if (cmbImagenes.SelectedIndex != -1)
            {
                productoLogica.VerImagen(pbImagen, (string)cmbImagenes.Text);
            }
           
            //cmbImagenes.SelectedIndex = 0;
        }



        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbImagenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            productoLogica.VerImagen(pbImagen, (string)cmbImagenes.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            CargarQR();
            pbImagen.Image = null;
            CargarMateriasPrimasPorProducto();

            CargarDescripcionImagenesEnElCombo();

            if (cmbImagenes.Items.Count != 0)
                cmbImagenes.SelectedIndex = 0;


            if (dgvDatos.SelectedRows.Count != 0)
            {
                Producto p = (Producto)dgvDatos.SelectedRows[0].DataBoundItem;
                if (p.EstadoActivo)
                {
                    btnEliminar.Enabled = true;
                    chkActivar.Enabled = false;
                    chkActivar.Visible = false;
                }

                else
                {
                    btnEliminar.Enabled = false;
                    chkActivar.Enabled = true;
                    chkActivar.Visible = true;
                }
                   
            }
            

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {

                FrmProdProveedor frm = new FrmProdProveedor();

                var resultado = frm.ShowDialog();
                if (resultado == DialogResult.OK)
                    Refrescar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count != 0)
            {
                Producto prod = (Producto)dgvDatos.SelectedRows[0].DataBoundItem;
                FrmProdProveedor frm = new FrmProdProveedor();
                frm.Producto = prod;
                frm.Producto.ListaDetalle.Clear();
                var resultado = frm.ShowDialog();
                if (resultado == DialogResult.OK)
                    Refrescar();
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Refrescar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("¿Seguro que desea desactivar el Producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        /*Usuario usuario = (Usuario)dgvDatos.SelectedRows[0].DataBoundItem;
                        usuarioLogica.ActivarODesactivar(usuario);
                        btnEliminar.Enabled = false;*/

                        Producto p= (Producto)dgvDatos.SelectedRows[0].DataBoundItem;
                        productoLogica.ActivarODesactivar(p);
                        btnEliminar.Enabled = false;


                    }
                    else
                    {
                        return;
                    }
                }
                Refrescar();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void chkActivar_CheckedChanged(object sender, EventArgs e)
        {

            if (dgvDatos.SelectedRows.Count != 0)
            {
                Producto p = (Producto)dgvDatos.SelectedRows[0].DataBoundItem;



                //foreach (DetalleProducto item in p.ListaDetalle)
                //{
                //    if (item.oMateriaPrima.EstadoActivo == true)
                //    {
                //        MessageBox.Show("Lo sentimos, el producto " + item.oMateriaPrima.Descripcion + " no cuenta con suficientes existencias o actualmente esta desactivado");
                //        return;
                //    }
                //}




                if (chkActivar.Checked)
                {
                    if (MessageBox.Show("¿Seguro que desea activar el Producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        productoLogica.ActivarODesactivar(p);
                    else
                        chkActivar.Checked = false;
                }
                    
                Refrescar();
                
            }
                


           
        }

        private void FrmMantProducto_MouseEnter(object sender, EventArgs e)
        {
            //Refrescar();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            Refrescar();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmMantMateriaPrima frm = new FrmMantMateriaPrima();
            var resultado = frm.ShowDialog();
            if (resultado == DialogResult.OK)
                Refrescar();
        }
    }
}
