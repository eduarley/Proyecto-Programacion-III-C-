namespace Proyecto_Progra_III
{
    partial class FrmReporteMasVendidos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.FloristeriaDataSet = new Proyecto_Progra_III.FloristeriaDataSet();
            this.PA_ReporteMasVendidosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PA_ReporteMasVendidosTableAdapter = new Proyecto_Progra_III.FloristeriaDataSetTableAdapters.PA_ReporteMasVendidosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.FloristeriaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PA_ReporteMasVendidosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.PA_ReporteMasVendidosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Proyecto_Progra_III.ReporteMasVendidos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(757, 585);
            this.reportViewer1.TabIndex = 0;
            // 
            // FloristeriaDataSet
            // 
            this.FloristeriaDataSet.DataSetName = "FloristeriaDataSet";
            this.FloristeriaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PA_ReporteMasVendidosBindingSource
            // 
            this.PA_ReporteMasVendidosBindingSource.DataMember = "PA_ReporteMasVendidos";
            this.PA_ReporteMasVendidosBindingSource.DataSource = this.FloristeriaDataSet;
            // 
            // PA_ReporteMasVendidosTableAdapter
            // 
            this.PA_ReporteMasVendidosTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 585);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmReporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReporte";
            this.Load += new System.EventHandler(this.FrmReporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FloristeriaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PA_ReporteMasVendidosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PA_ReporteMasVendidosBindingSource;
        private FloristeriaDataSet FloristeriaDataSet;
        private FloristeriaDataSetTableAdapters.PA_ReporteMasVendidosTableAdapter PA_ReporteMasVendidosTableAdapter;
    }
}