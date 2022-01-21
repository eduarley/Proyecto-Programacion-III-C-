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

    public partial class FrmMantMateriaPrima : Form
    {
        private MateriaPrimaLogica matLog;
        private ProveedorLogica provLog;
        public FrmMantMateriaPrima()
        {
            InitializeComponent();
        
            matLog = new MateriaPrimaLogica();
            provLog = new ProveedorLogica();
            
        }
        public void Refrescar()
        {
            //dgvDatos.AutoGenerateColumns = false;
            dgvDatos.DataSource = matLog.ObtenerProductos();
            dgvDatos.Columns.RemoveAt(6);  //para que no se vea la columna de oProveedor
          

        }
        public void CargarProveedores()
        {
            cmbProveedor.DataSource = provLog.ObtenerProveedores();
            cmbProveedor.DisplayMember = "Nombre";
            cmbProveedor.ValueMember = "ID";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmDetalleMateriaPrima frm = new FrmDetalleMateriaPrima();
           

            var resultado = frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                Refrescar();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDetalleMateriaPrima frm = new FrmDetalleMateriaPrima();
            frm.MateriaPrima = (MateriaPrima)dgvDatos.SelectedRows[0].DataBoundItem;
            var resultado = frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                Refrescar();
        }

        private void FrmMantMateriaPrima_Load(object sender, EventArgs e)
        {
            Refrescar();
            CargarProveedores();
        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvDatos.SelectedRows.Count != 0)
            {
                MateriaPrima mat = (MateriaPrima)dgvDatos.SelectedRows[0].DataBoundItem;

                if (mat.EstadoActivo)
                {
                    //chkActivar.Checked = false;
                    chkActivar.Visible = false;
                    btnEliminar.Enabled = true;
                }

                else
                {
                    //chkActivar.Checked = false;
                    chkActivar.Visible = true;
                    btnEliminar.Enabled = false;
                }
                    
            }
            

        }

        private void chkActivar_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count != 0)
            {
                MateriaPrima m = (MateriaPrima)dgvDatos.SelectedRows[0].DataBoundItem;
                if (chkActivar.Checked)
                {
                    if (MessageBox.Show("¿Seguro que desea activar la materia prima seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        matLog.ActivarODesactivar(m);
                    else
                        chkActivar.Checked = false;
                }

                Refrescar();

            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count != 0)
            {
                MateriaPrima m = (MateriaPrima)dgvDatos.SelectedRows[0].DataBoundItem;
               
                    if (MessageBox.Show("¿Seguro que desea desactivar la materia prima seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        matLog.ActivarODesactivar(m);
                    else
                        chkActivar.Checked = false;
              

                Refrescar();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
