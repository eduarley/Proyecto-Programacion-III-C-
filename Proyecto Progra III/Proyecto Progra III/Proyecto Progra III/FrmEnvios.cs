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
    public partial class FrmEnvios : Form
    {


        private EnvioLogica envioLog;
        private CategoriaLogica catLog;
        
        public FrmEnvios()
        {
            InitializeComponent();
            envioLog = new EnvioLogica();
            catLog = new CategoriaLogica();
        }

        private void FrmEnvios_Load(object sender, EventArgs e)
        {
            Refrescar();
            CargarEmpleados();
        }
        private void CargarEmpleados()
        {
            cmbEmpleados.DataSource = catLog.ObtenerCajeros();
            cmbEmpleados.DisplayMember = "Apellido";
            cmbEmpleados.ValueMember = "Id";
        }

        private void Refrescar()
        {
            dgvDatos.DataSource = envioLog.SeleccionarTodosLosEnvios();
            
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            Envio envio = (Envio)dgvDatos.SelectedRows[0].DataBoundItem;
            int empleadoID = (int)cmbEmpleados.SelectedValue;
            try
            {
                envioLog.AgregarEmpleado(empleadoID,envio.PedidoFacturaID);
                //MessageBox.Show("Éxito al asignar!");
                Refrescar();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hubo un error debido a " + ex.Message);
            }


        }

        private void btnEntregar_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("¿Seguro que desea realizar el envío?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {





                try
                {

                    if (dgvDatos.SelectedRows.Count != 0)
                    {
                        Envio env = (Envio)dgvDatos.SelectedRows[0].DataBoundItem;
                        if (env.Empleado == "Sin asignar")
                        {
                            errorProvider1.SetError(cmbEmpleados, "");
                            MessageBox.Show("Debe asignar un empleado a cargo del envío!");
                            return;
                        }
                        else
                        {
                            errorProvider1.Clear();
                        }


                        envioLog.RealizarEnvio(env);
                        MessageBox.Show("Éxito al realizar el envío!");
                        Refrescar();
                        richTextBox1.Text = envioLog.SeleccionarFacturaPorEnvio(env.PedidoFacturaID).ToString();
                    }

                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hubo un error debido a " + ex.Message);
                }




            }




        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvDatos.SelectedRows.Count != 0)
            {
                Envio env = (Envio)dgvDatos.SelectedRows[0].DataBoundItem;
                richTextBox1.Text = envioLog.SeleccionarFacturaPorEnvio(env.PedidoFacturaID).ToString();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FacturaLogica factLog = new FacturaLogica();

            if (dgvDatos.SelectedRows.Count != 0)
            {
                Envio env = (Envio)dgvDatos.SelectedRows[0].DataBoundItem;



            }


            
        }
    }
}
