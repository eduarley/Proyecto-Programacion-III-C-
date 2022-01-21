using CapaEntidades;
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
    public partial class FrmMantProveedor : Form
    {

        CapaLogicaNegocio.ProveedorLogica logica;

        public FrmMantProveedor()
        {
            InitializeComponent();
            logica = new CapaLogicaNegocio.ProveedorLogica();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                
                
                    
                    FrmDetalleProveedor frm = new FrmDetalleProveedor();     
                    var resultado = frm.ShowDialog();
                    if (resultado == DialogResult.OK)
                        Refrescar();
                



            }
            catch (Exception ex)
            {

                MessageBox.Show("Hubo un problema al guardar: \n"+ex.Message);
            }







        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count != 0)
                {
                    Proveedor prov = (Proveedor)dgvDatos.SelectedRows[0].DataBoundItem;
                    FrmDetalleProveedor frm = new FrmDetalleProveedor();
                    frm.Proveedor = prov;
                    var resultado = frm.ShowDialog();
                    if (resultado == DialogResult.OK)
                        Refrescar();
                }

            }
            catch (Exception)
            {

                throw;
            }
        
    }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {


                if (dgvDatos.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("¿Seguro que desea desactivar el Proveedor?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Proveedor prov = (Proveedor)dgvDatos.SelectedRows[0].DataBoundItem;
                        logica.ActivarODesactivar(prov);
                        btnEliminarP.Enabled = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMantProveedor_Load(object sender, EventArgs e)
        {
            Refrescar();
        }
        public void Refrescar()
        {
            dgvDatos.DataSource = logica.ObtenerProveedores();
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvDatos.SelectedRows.Count != 0)
            {
                Proveedor prov = (Proveedor)dgvDatos.SelectedRows[0].DataBoundItem;
                if (prov.EstadoActivo)
                    btnEliminarP.Enabled = true;
                else
                    btnEliminarP.Enabled = false;
            }
            
        }

        private void btnAgregarP_Click(object sender, EventArgs e)
        {

            try
            {



                FrmDetalleProveedor frm = new FrmDetalleProveedor();
                var resultado = frm.ShowDialog();
                if (resultado == DialogResult.OK)
                    Refrescar();




            }
            catch (Exception ex)
            {

                MessageBox.Show("Hubo un problema al guardar: \n" + ex.Message);
            }
        }

        private void btnModificarP_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count != 0)
                {
                    Proveedor prov = (Proveedor)dgvDatos.SelectedRows[0].DataBoundItem;
                    FrmDetalleProveedor frm = new FrmDetalleProveedor();
                    frm.Proveedor = prov;
                    var resultado = frm.ShowDialog();
                    if (resultado == DialogResult.OK)
                        Refrescar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error debido a " + ex.Message);
            }
        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            try
            {


                if (dgvDatos.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("¿Seguro que desea desactivar el Proveedor?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Proveedor prov = (Proveedor)dgvDatos.SelectedRows[0].DataBoundItem;
                        logica.ActivarODesactivar(prov);
                        btnEliminarP.Enabled = false;
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

                MessageBox.Show("Error debido a " + ex.Message);
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
