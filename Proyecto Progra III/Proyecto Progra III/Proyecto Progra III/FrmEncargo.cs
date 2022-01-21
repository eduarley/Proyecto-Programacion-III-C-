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
using System.Windows.Forms;

namespace Proyecto_Progra_III
{
    public partial class FrmEncargo : Form
    {
        public Factura  Factura { get; set; }

        public Cliente Cliente { get; set; }

        public Producto ProductoPERSONALIZADO { get; set; }
        public FrmEncargo()
        {
            InitializeComponent();
        }

        private void btnEncargar_Click(object sender, EventArgs e)
        {
            #region
            #endregion

            //validaciones
            if (!mskTelefono.MaskFull)
            {
                MessageBox.Show("Ingresar el número de teléfono!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                errorProvider1.SetError(mskTelefono, "Ingresar teléfono");
                mskTelefono.Focus();
                return;
            }else
            {
                errorProvider1.Clear();
            }

            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                MessageBox.Show("Ingresar la dirección!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                errorProvider1.SetError(txtDireccion, "Ingresar dirección");
                txtDireccion.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (dtpFecha.Value < DateTime.Today)
            {
                MessageBox.Show("La fecha ingresada es menor a la actual!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                errorProvider1.SetError(dtpFecha, "");
               
                return;
            }




            if (MessageBox.Show("¿Seguro que desea formalizar el encargo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {





                Pago pago = null;
                if (rdbDeposito.Checked)
                {
                    /*Factory*/
                    pago = Factory.CrearPago("Depósito");
                    pago.MontoPagar = Convert.ToDouble(Factura.Monto);
                    ((Deposito)pago).CodigoIBAN = mskIban.Text;



                    if (!mskIban.MaskFull)
                    {
                        MessageBox.Show("Debe Completar los 22 números de su código IBAN ");
                        errorProvider1.SetError(mskIban, "");
                        mskIban.Focus();
                        return;
                    }
                    else
                    {
                        errorProvider1.Clear();
                    }





                }





                if (rdbTarjeta.Checked)
                {
                    /*FACTORY*/
                    pago = Factory.CrearPago("Tarjeta");
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





                Encargo encargo = new Encargo();
                encargo.Direccion = txtDireccion.Text;
                encargo.Encargado = null;
                encargo.Telefono = (mskTelefono.Text);
                encargo.Factura = Factura;
                encargo.Fecha = dtpFecha.Value;
                Factura.Estado = "Pagado";
                
                if(pago is Tarjeta)
                {
                    Factura.Tipo = new Tarjeta();
                    ((Tarjeta)encargo.Factura.Tipo).Numero = Convert.ToInt32(txtNumeroTarjeta.Text);
                }


                Factura.oEncargo = encargo;

                //if (rdbTarjeta.Checked)
                //{
                //    Factura.Tipo = new Tarjeta();
                //}

                //if (rdbDeposito.Checked)
                //{
                //    Factura.Tipo = new Deposito();
                //}



                Factura.Tipo = pago;


                EncargoLogica encargoLog = new EncargoLogica();
                FacturaLogica factLog = new FacturaLogica();
                DetalleFacturaLogica detFacLog = new DetalleFacturaLogica();














                try
                {
                    factLog.Nuevo(Factura);
                    encargoLog.Nuevo(encargo);
                    factLog.ActualizarPago(Factura, pago);
                    foreach (var item in Factura.ListaDetalleFactura)
                    {
                       // if (ProductoPERSONALIZADO != null) //por si se hizo un producto personalizado
                        detFacLog.Nuevo(item, Factura);
                    }






                    if (FacturaLogica.CheckearConexion())
                    {
                    try
                    {
                        factLog.crearArchivo();
                        factLog.ExportarXML(Factura);
                        factLog.EnviarCorreo(Factura);
                        factLog.ActualizarPago(Factura, pago);
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
                            factLog.ActualizarPago(Factura, pago);

                        }
                        else
                        {
                            return;
                        }
                    }







                }
                catch (Exception ex)
                {

                    MessageBox.Show("Lo sentimos, no hay suficientes materiales para realizar su encargo","Hubo un error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                richTextBox1.Text = Factura.ToString();
                MessageBox.Show("Éxito al Encargar!", "Éxito!", MessageBoxButtons.OK, MessageBoxIcon.Information);





            }
            //FIN VALIDACIONES






        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmEncargo_Load(object sender, EventArgs e)
        {
            txtTotal.Text = Factura.MontoTotal().ToString("0,00 Colones");
            rdbTarjeta.Checked = true;
        }

        private void rdbTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTarjeta.Checked)
            {
                gbxTarjeta.Visible = true;
                gbxDeposito.Visible = false;
            }
        }

        private void rdbDeposito_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDeposito.Checked)
            {
                gbxTarjeta.Visible = false;
                gbxDeposito.Visible = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
