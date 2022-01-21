using CapaEntidades;
using CapaLogicaNegocio;
using CapaLogicaNegocio.Patrones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Proyecto_Progra_III
{
    public partial class FrmFacturar : Form
    {


        public Usuario Empleado { get; set; }
        public Factura Factura { get; set; }

        private FacturaLogica factLog;
        //private Tarjeta tarjeta;
        public FrmFacturar()
        {
            InitializeComponent();
            factLog = new FacturaLogica();
        }

        public void CargarDatosFactura()
        {
            txtId.Text = Factura.ID_Factura.ToString("0000000000");
            txtCliente.Text = Factura.Cliente.Nombre + " " + Factura.Cliente.Apellido;
            dtpFecha.Value = Factura.Fecha;
            txtEstado.Text = Factura.Estado;
            if(Empleado!=null)
                txtEmpleado.Text = Empleado.Nombre + " " + Empleado.Apellido;
            txtMonto.Text = Factura.Monto.ToString("0,00");
            cmbTipoPago.SelectedIndex = 0;

        }


        private void FrmFacturar_Load(object sender, EventArgs e)
        {
           
            CargarDatosFactura();
        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoPago.Text == "Efectivo")
            {
                //gpbContado.Enabled = true;
                //gpbTarjeta.Enabled = false;
                gpbContado.Visible = true;
                gpbTarjeta.Visible = false;
                txtVuelto.Visible = true;
                label12.Visible = true;
                gbxDeposito.Visible = false;
            }

            if (cmbTipoPago.Text == "Tarjeta")
            {
                //gpbContado.Enabled = false;
                //gpbTarjeta.Enabled = true;
                gpbContado.Visible = false;
                gpbTarjeta.Visible = true;
                txtVuelto.Visible = false;
                label12.Visible = false;
                gbxDeposito.Visible = false;
            }

            if (cmbTipoPago.Text == "Depósito")
            {
                gbxDeposito.Visible = true;
                gpbContado.Visible = false;
                gpbTarjeta.Visible = false;
                label12.Visible = false;
                txtVuelto.Visible = false;
            }


            }

        private void button1_Click(object sender, EventArgs e)
        {
            






            if (MessageBox.Show("¿Seguro que desea facturar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {





                Pago pago = null;
                if (cmbTipoPago.Text == "Efectivo")
                {
/*Factory*/         pago = Factory.CrearPago(cmbTipoPago.Text);
                    pago.MontoPagar = Convert.ToDouble(Factura.Monto);
                    EfectivoLogica efectLog = new EfectivoLogica();
                    double pagaCon = efectLog.Vuelto(pago, Convert.ToDouble(txtPagaCon.Text));
                    if (pagaCon == -1)
                    {
                        MessageBox.Show("El monto ingresado es menor al monto total de la factura!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                        txtVuelto.Text = pagaCon.ToString("0,00 Colones");
                       

                }
                if (cmbTipoPago.Text == "Tarjeta")
                {
/*FACTORY*/         pago = Factory.CrearPago(cmbTipoPago.Text);
                    TarjetaLogica tarjetaLogica = new TarjetaLogica();

                    //VALIDACIONES

                    if (cmbTipoTarjeta.SelectedIndex == -1)
                    {
                        MessageBox.Show("Debe seleccionar algún tipo de tarjeta!");
                        errorProvider1.SetError(cmbTipoTarjeta, "");
                        cmbTipoTarjeta.Focus();
                        return;
                    }
                    else
                    {
                        errorProvider1.Clear();
                    }


                    if (string.IsNullOrEmpty(txtNumeroTarjeta.Text))
                    {
                        MessageBox.Show("Debe ingresar su número de tarjeta!");
                        errorProvider1.SetError(txtNumeroTarjeta, "");
                        txtNumeroTarjeta.Focus();
                        return;
                    }
                    else
                    {
                        errorProvider1.Clear();
                    }

                    if (!mskCodigo.MaskFull)
                    {
                        MessageBox.Show("Debe Completar los 4 dígitos de su tarjeta");
                        errorProvider1.SetError(mskCodigo, "");
                        mskCodigo.Focus();
                        return;
                    }
                    else
                    {
                        errorProvider1.Clear();
                    }



                    








                    try
                    {
                        ((Tarjeta)pago).Numero = Convert.ToInt64(txtNumeroTarjeta.Text);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("El número de tarjeta solo debe incluir dígitos!");
                        txtNumeroTarjeta.Focus();
                        txtNumeroTarjeta.SelectAll();
                        return;
                    }

                    
                    
                    ((Tarjeta)pago).Codigo = Convert.ToInt32(mskCodigo.Text);
                    ((Tarjeta)pago).Marca = cmbTipoTarjeta.Text;
                    pago.MontoPagar = (double)Factura.Monto;
                   


                    if (!tarjetaLogica.ValidarTarjeta(pago as Tarjeta))
                    {
                        MessageBox.Show("Número de tarjeta incorrecto!", "Hubo un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        errorProvider1.SetError(txtNumeroTarjeta, "");
                        txtNumeroTarjeta.Focus();
                        return;
                    }
                    else
                    {
                        errorProvider1.Clear();
                    }
                   

                }

                

                if (cmbTipoPago.Text == "Depósito")
                {


                    if (!mskIban.MaskFull)
                    {
                        MessageBox.Show("Debe Completar los 22 dígitos de su tarjeta");
                        errorProvider1.SetError(mskIban, "");
                        mskIban.Focus();
                        return;
                    }
                    else
                    {
                        errorProvider1.Clear();
                    }

                }

                //FIN VALIDACIONES






                if (cmbTipoPago.Text == "Depósito")
                {
                    pago = new Deposito();
                    ((Deposito)pago).CodigoIBAN = mskIban.Text;
                }

                if (Empleado != null)
                    Factura.Empleado = Empleado;


                Factura.Tipo = pago;
                Factura.Estado = "Pagada";
                DetalleFacturaLogica detFactLog = new DetalleFacturaLogica();
                EncargoLogica encLog = new EncargoLogica();
                Factura.ListaDetalleFactura = detFactLog.SeleccionarDetallePorFactura(Factura);
                Factura.oEncargo = encLog.SeleccionarEncargoPorFactura(Factura);


                //factLog.EnviarCorreo(Factura);

                try
                {

                    using (TransactionScope trans = new TransactionScope())     ///TRANSACCION!!
                    {
                        if (FacturaLogica.CheckearConexion())
                        {
                            try
                            {
                                factLog.crearArchivo();
                                factLog.ExportarXML(Factura);
                                //this.webBrowser1.Url = new Uri(@"c:\temp\factura.xml");
                                webBrowser1.Url = new Uri(factLog.NombreArchivo);
                                factLog.EnviarCorreo(Factura);
                                factLog.Pagar(Factura);
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Error al enviar el correo debido a " + ex.Message);
                            return;
                        }
                    }
                    else
                        {
                            if (MessageBox.Show("No hay conexión a la red, seguro que desea proceder a facturar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                factLog.Pagar(Factura);
                                factLog.crearArchivo();
                                factLog.ExportarXML(Factura);
                                webBrowser1.Url = new Uri(factLog.NombreArchivo);
                            }
                            else
                            {
                                return;
                            }
                        }
                        


                        

                        trans.Complete();
                    }
                    MessageBox.Show("Éxito al facturar!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    richTextBox1.Text = Factura.ToString();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al Pagar: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                
                
            }


















        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {





            //factLog.ExportarXML(f);
            //this.webBrowser1.Url = new Uri(@"c:\temp\boleto.xml");
            Factura.Empleado = this.Empleado;
            factLog.ExportarXML(Factura);
            this.webBrowser1.Url = new Uri(@"c:\temp\factura.xml");
        }
    }
}
