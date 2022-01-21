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
    public partial class FrmDatosFacturas : Form
    {
        public Usuario  Empleado { get; set; }
       
        FacturaLogica factLog;

        public FrmDatosFacturas()
        {
            InitializeComponent();
            factLog = new FacturaLogica();
        }


        public void Refrescar()
        {
            dgvDatos.DataSource = factLog.FacturasPendientes();
            dgvDatos.Columns.RemoveAt(6);

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {

            try
            {
                FrmFacturar frm = new FrmFacturar();
                frm.Factura = (Factura)dgvDatos.SelectedRows[0].DataBoundItem;
                frm.Empleado = Empleado;

                var resultado = frm.ShowDialog();
                if (resultado == DialogResult.OK)
                    Refrescar();





            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void FrmDatosFacturas_Load(object sender, EventArgs e)
        {
            cmbVer.SelectedIndex = 0;
            Refrescar();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Factura f = (Factura)dgvDatos.SelectedRows[0].DataBoundItem;




            //factLog.ExportarXML(f);
            //this.webBrowser1.Url = new Uri(@"c:\temp\boleto.xml");
            


        }
    }
}
