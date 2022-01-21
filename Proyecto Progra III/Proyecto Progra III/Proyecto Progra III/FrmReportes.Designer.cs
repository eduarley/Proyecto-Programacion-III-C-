namespace Proyecto_Progra_III
{
    partial class FrmReportes
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.PA_ReporteProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FloristeriaDataSet = new Proyecto_Progra_III.FloristeriaDataSet();
            this.PA_ReporteVentasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Productos = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Ventas = new System.Windows.Forms.TabPage();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PA_ReporteProductosTableAdapter = new Proyecto_Progra_III.FloristeriaDataSetTableAdapters.PA_ReporteProductosTableAdapter();
            this.PA_ReporteVentasTableAdapter = new Proyecto_Progra_III.FloristeriaDataSetTableAdapters.PA_ReporteVentasTableAdapter();
            this.Top5 = new System.Windows.Forms.TabPage();
            this.reportViewer3 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PA_ReporteMasVendidosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PA_ReporteMasVendidosTableAdapter = new Proyecto_Progra_III.FloristeriaDataSetTableAdapters.PA_ReporteMasVendidosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.PA_ReporteProductosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloristeriaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PA_ReporteVentasBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.Productos.SuspendLayout();
            this.Ventas.SuspendLayout();
            this.Top5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PA_ReporteMasVendidosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PA_ReporteProductosBindingSource
            // 
            this.PA_ReporteProductosBindingSource.DataMember = "PA_ReporteProductos";
            this.PA_ReporteProductosBindingSource.DataSource = this.FloristeriaDataSet;
            // 
            // FloristeriaDataSet
            // 
            this.FloristeriaDataSet.DataSetName = "FloristeriaDataSet";
            this.FloristeriaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PA_ReporteVentasBindingSource
            // 
            this.PA_ReporteVentasBindingSource.DataMember = "PA_ReporteVentas";
            this.PA_ReporteVentasBindingSource.DataSource = this.FloristeriaDataSet;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Productos);
            this.tabControl1.Controls.Add(this.Ventas);
            this.tabControl1.Controls.Add(this.Top5);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(655, 459);
            this.tabControl1.TabIndex = 0;
            // 
            // Productos
            // 
            this.Productos.Controls.Add(this.reportViewer1);
            this.Productos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Productos.Location = new System.Drawing.Point(4, 22);
            this.Productos.Name = "Productos";
            this.Productos.Padding = new System.Windows.Forms.Padding(3);
            this.Productos.Size = new System.Drawing.Size(647, 433);
            this.Productos.TabIndex = 0;
            this.Productos.Text = "Productos";
            this.Productos.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.PA_ReporteProductosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Proyecto_Progra_III.ReporteProductos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 3);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ShowPageNavigationControls = false;
            this.reportViewer1.Size = new System.Drawing.Size(641, 427);
            this.reportViewer1.TabIndex = 0;
            // 
            // Ventas
            // 
            this.Ventas.Controls.Add(this.btnAceptar);
            this.Ventas.Controls.Add(this.label2);
            this.Ventas.Controls.Add(this.label1);
            this.Ventas.Controls.Add(this.dtpFechaFinal);
            this.Ventas.Controls.Add(this.dtpFechaInicial);
            this.Ventas.Controls.Add(this.reportViewer2);
            this.Ventas.Location = new System.Drawing.Point(4, 22);
            this.Ventas.Name = "Ventas";
            this.Ventas.Padding = new System.Windows.Forms.Padding(3);
            this.Ventas.Size = new System.Drawing.Size(647, 433);
            this.Ventas.TabIndex = 1;
            this.Ventas.Text = "Ventas";
            this.Ventas.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(17, 70);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(108, 23);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Hasta;";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Desde:";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(246, 44);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(110, 20);
            this.dtpFechaFinal.TabIndex = 3;
            this.dtpFechaFinal.Value = new System.DateTime(2019, 8, 20, 0, 0, 0, 0);
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(17, 44);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(110, 20);
            this.dtpFechaInicial.TabIndex = 2;
            this.dtpFechaInicial.Value = new System.DateTime(2019, 8, 20, 0, 0, 0, 0);
            // 
            // reportViewer2
            // 
            this.reportViewer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.PA_ReporteVentasBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "Proyecto_Progra_III.ReporteVentas.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(17, 113);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(624, 314);
            this.reportViewer2.TabIndex = 1;
            this.reportViewer2.Load += new System.EventHandler(this.reportViewer2_Load);
            // 
            // PA_ReporteProductosTableAdapter
            // 
            this.PA_ReporteProductosTableAdapter.ClearBeforeFill = true;
            // 
            // PA_ReporteVentasTableAdapter
            // 
            this.PA_ReporteVentasTableAdapter.ClearBeforeFill = true;
            // 
            // Top5
            // 
            this.Top5.Controls.Add(this.reportViewer3);
            this.Top5.Location = new System.Drawing.Point(4, 22);
            this.Top5.Name = "Top5";
            this.Top5.Size = new System.Drawing.Size(647, 433);
            this.Top5.TabIndex = 2;
            this.Top5.Text = "Top 5 Vendidos";
            this.Top5.UseVisualStyleBackColor = true;
            // 
            // reportViewer3
            // 
            this.reportViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.PA_ReporteMasVendidosBindingSource;
            this.reportViewer3.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer3.LocalReport.ReportEmbeddedResource = "Proyecto_Progra_III.ReporteMasVendidos.rdlc";
            this.reportViewer3.Location = new System.Drawing.Point(0, 0);
            this.reportViewer3.Name = "reportViewer3";
            this.reportViewer3.Size = new System.Drawing.Size(647, 433);
            this.reportViewer3.TabIndex = 0;
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
            // FrmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 473);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReportes";
            this.Load += new System.EventHandler(this.FrmReportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PA_ReporteProductosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FloristeriaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PA_ReporteVentasBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.Productos.ResumeLayout(false);
            this.Ventas.ResumeLayout(false);
            this.Ventas.PerformLayout();
            this.Top5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PA_ReporteMasVendidosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Productos;
        private System.Windows.Forms.TabPage Ventas;
        private System.Windows.Forms.BindingSource PA_ReporteProductosBindingSource;
        private FloristeriaDataSet FloristeriaDataSet;
        private FloristeriaDataSetTableAdapters.PA_ReporteProductosTableAdapter PA_ReporteProductosTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.BindingSource PA_ReporteVentasBindingSource;
        private FloristeriaDataSetTableAdapters.PA_ReporteVentasTableAdapter PA_ReporteVentasTableAdapter;
        private System.Windows.Forms.TabPage Top5;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer3;
        private System.Windows.Forms.BindingSource PA_ReporteMasVendidosBindingSource;
        private FloristeriaDataSetTableAdapters.PA_ReporteMasVendidosTableAdapter PA_ReporteMasVendidosTableAdapter;
    }
}