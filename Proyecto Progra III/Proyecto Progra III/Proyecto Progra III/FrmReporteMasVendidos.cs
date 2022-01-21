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
    public partial class FrmReporteMasVendidos : Form
    {
        public FrmReporteMasVendidos()
        {
            InitializeComponent();
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'FloristeriaDataSet.PA_ReporteMasVendidos' Puede moverla o quitarla según sea necesario.
            this.PA_ReporteMasVendidosTableAdapter.Fill(this.FloristeriaDataSet.PA_ReporteMasVendidos);

            this.reportViewer1.RefreshReport();
        }
    }
}
