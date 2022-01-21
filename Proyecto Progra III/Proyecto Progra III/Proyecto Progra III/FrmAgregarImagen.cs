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
    public partial class FrmAgregarImagen : Form
    {
        public Producto Producto { get; set; }


        public FrmAgregarImagen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.OK;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.openFileDialog1.ShowDialog();
                if (!this.openFileDialog1.FileName.Equals(""))
                    pbImagen.Load(this.openFileDialog1.FileName);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo cargar la imagen debido a " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {





            try
            {

                if (string.IsNullOrEmpty(txtDescripcionImagen.Text))
                {
                    errorProvider1.SetError(txtDescripcionImagen, "Debe agregar una descripción a la imagen!");
                    MessageBox.Show("Debe agregar una descripción a la imagen","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }else
                {
                    errorProvider1.Clear();
                }


                
                ProductoLogica prodLog = new ProductoLogica();
                if (Producto != null)
                {
                    prodLog.AgregarImagen(Producto, txtDescripcionImagen.Text, pbImagen);
                    MessageBox.Show("Imagen agregada con éxito!");
                    this.Close();
                }

                DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {

                MessageBox.Show("No se ha insertado la imagen: " + ex.Message);
            }



        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void FrmAgregarImagen_Load(object sender, EventArgs e)
        {
            txtDescripcionImagen.Text = Producto.Descripcion;
            txtDescripcionImagen.SelectAll();
        }
    }
}
