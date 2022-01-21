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
    public partial class FrmReportes : Form
    {

        private DateTime FechaInicial;
        private DateTime FechaFinal;
        private Usuario usuario;
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'FloristeriaDataSet.PA_ReporteMasVendidos' Puede moverla o quitarla según sea necesario.
            this.PA_ReporteMasVendidosTableAdapter.Fill(this.FloristeriaDataSet.PA_ReporteMasVendidos);

            // TODO: esta línea de código carga datos en la tabla 'FloristeriaDataSet.PA_ReporteProductos' Puede moverla o quitarla según sea necesario.
            this.PA_ReporteProductosTableAdapter.Fill(this.FloristeriaDataSet.PA_ReporteProductos);

            this.reportViewer1.RefreshReport();

            this.reportViewer3.RefreshReport();
        }

        private void reportViewer2_Load(object sender, EventArgs e)
        {
           
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            FechaInicial = dtpFechaInicial.Value;
            FechaFinal = dtpFechaFinal.Value;
            // TODO: esta línea de código carga datos en la tabla 'FloristeriaDataSet.PA_ReporteVentas' Puede moverla o quitarla según sea necesario.
            this.PA_ReporteVentasTableAdapter.Fill(this.FloristeriaDataSet.PA_ReporteVentas, this.FechaInicial, this.FechaFinal);
            this.reportViewer2.RefreshReport();


        }
    }
}
