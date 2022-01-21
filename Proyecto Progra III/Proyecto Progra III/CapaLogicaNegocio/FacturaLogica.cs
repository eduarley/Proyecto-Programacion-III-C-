using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CapaLogicaNegocio
{
    public class FacturaLogica
    {



        public string NombreArchivo { get; set; }

        public FacturaLogica()
        {
            NombreArchivo = Environment.CurrentDirectory + @"\Factura.xml";
        }




        public string crearArchivo()
        {
            string ruta = "";
            FileStream archivo = new FileStream(NombreArchivo, FileMode.Append, FileAccess.Write);
            if (archivo != null)
            {
                ruta = archivo.Name;
            }
            archivo.Close();
            return ruta;
        }


        public void Nuevo(Factura factura)
        {
            FacturaDatos datos = new FacturaDatos();
            datos.Insertar(factura);
        }



        public int IDMayor()
        {
            FacturaDatos datos = new FacturaDatos();
            return datos.UltimoId();
        }



        public List<Factura> FacturasPendientes()
        {
            FacturaDatos datos = new FacturaDatos();
            return datos.SeleccionarTodasLasFacturasPendientes();
        }




        public void Pagar(Factura fact)
        {
            FacturaDatos datos = new FacturaDatos();
            datos.Actualizar(fact);
        }




        public void ActualizarPago(Factura fact, Pago pago)
        {
            FacturaDatos datos = new FacturaDatos();
            datos.ActualizarTipoPago(fact.ID_Factura, pago.Tipo);
        }






        public void ExportarXML(Factura factura)
        {
           

            
            
            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists(NombreArchivo))
            {
                xmlDoc.LoadXml("<Factura></Factura>");
            }

            else
            {
                xmlDoc.Load(NombreArchivo);
            }






            //XmlDeclaration declaracion = xmlDoc.CreateXmlDeclaration("1.0", "iso-8859-1", null);
            //xmlDoc.AppendChild(declaracion);





            XmlElement nodoFactura = xmlDoc.CreateElement("Factura");
            nodoFactura.SetAttribute("Numero", factura.ID_Factura.ToString("0000000000"));
            nodoFactura.SetAttribute("Fecha", factura.Fecha.ToString());
            //xmlDoc.AppendChild(nodoFactura);



            XmlElement nodoCliente = xmlDoc.CreateElement("Cliente");
            nodoCliente.SetAttribute("Identificacion", factura.Cliente.ID.ToString());
            nodoCliente.SetAttribute("Nombre", factura.Cliente.Nombre);
            nodoCliente.SetAttribute("Apellido", factura.Cliente.Apellido);
            //nodoFactura.AppendChild(nodoCliente);


           
            XmlElement nodoEmpleado = xmlDoc.CreateElement("Empleado");
            if (factura.Empleado != null)
            {
                nodoEmpleado.SetAttribute("Identificacion", factura.Empleado.ID.ToString());
                nodoEmpleado.SetAttribute("Nombre", factura.Empleado.Nombre);
                nodoEmpleado.SetAttribute("Apellido", factura.Empleado.Apellido);
            }
            //nodoFactura.AppendChild(nodoEmpleado);



            XmlElement nodoPago = xmlDoc.CreateElement("Pago");
            if (factura.Tipo != null)
            {
                nodoPago.SetAttribute("Tipo", factura.Tipo.Tipo);
                if (factura.Tipo is Tarjeta)
                {
                    nodoPago.SetAttribute("Marca", ((Tarjeta)factura.Tipo).Marca.ToString());
                    nodoPago.SetAttribute("Numero", ((Tarjeta)factura.Tipo).Numero.ToString());
                }
                if (factura.Tipo is Efectivo)
                {
                    //nodoPago.SetAttribute("Paga con", "Arreglar esto en el xml");
                    //nodoPago.SetAttribute("Su cambio", "Arreglar esto tambien");//((EfectivoLogica)factura.Tipo).Vuelto.ToString("0.00"));
                }
                if (factura.Tipo is Deposito)
                {
                    nodoPago.SetAttribute("Cuenta", ((Deposito)factura.Tipo).CodigoIBAN.ToString());
                }


            }
            //nodoFactura.AppendChild(nodoPago);





            XmlElement nodoEncargo = xmlDoc.CreateElement("Encargo");
            if (factura.oEncargo != null)
            {
               // nodoEncargo.InnerText = "Se solicito por encargo";
                nodoEncargo.SetAttribute("Direccion", factura.oEncargo.Direccion);
                nodoEncargo.SetAttribute("Telefono_disponible", factura.oEncargo.Telefono);
                if(factura.oEncargo.Encargado!=null)
                    nodoEncargo.SetAttribute("Encargado_de_entrega", factura.oEncargo.Encargado.Nombre + " " + factura.oEncargo.Encargado.Apellido);
                nodoEncargo.SetAttribute("Fecha_del_envio", factura.oEncargo.Fecha.ToString());

            }
            //nodoFactura.AppendChild(nodoEncargo);




            XmlElement nodoDetalle = xmlDoc.CreateElement("Detalle");           
            foreach (DetalleFactura det in factura.ListaDetalleFactura)
            {
                XmlElement detalle = xmlDoc.CreateElement("Adicional");
                detalle.InnerText = det.ToString();
                nodoDetalle.AppendChild(detalle);
            }
            // nodoFactura.AppendChild(nodoDetalle);

            

            
            nodoFactura.SetAttribute("Total", factura.MontoTotal().ToString("0,00"));
           


            //xmlDoc.AppendChild(nodoFactura);




            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            xmlDoc.WriteTo(tx);

            string str = sw.ToString();




            xmlDoc.DocumentElement.AppendChild(nodoFactura);
            xmlDoc.DocumentElement.AppendChild(nodoCliente);
            xmlDoc.DocumentElement.AppendChild(nodoEmpleado);
            xmlDoc.DocumentElement.AppendChild(nodoPago);
            xmlDoc.DocumentElement.AppendChild(nodoDetalle);
            xmlDoc.DocumentElement.AppendChild(nodoEncargo);

            xmlDoc.Save(NombreArchivo);

        }




        public void EnviarCorreo(Factura fact)
        {
            FacturaLogica factLog = new FacturaLogica();

            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
            mmsg.To.Add(fact.Cliente.Correo);
            mmsg.Subject = "Factura electrónica Floristería Arley´s";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            System.Net.Mail.Attachment archivo = new System.Net.Mail.Attachment(crearArchivo());
            mmsg.Attachments.Add(archivo);


            //mmsg.Body = fact.ToString();
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true;
            mmsg.From = new System.Net.Mail.MailAddress("cuentax128@gmail.com");

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials = new System.Net.NetworkCredential("cuentax128@gmail.com","eduardo99");
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";


            try
            {
                cliente.Send(mmsg);
            }
            catch (Exception)
            {

                throw;
            }

        }






        /// <summary>
        /// Verifica si hay conexión a internet para enviar el correo
        /// </summary>
        /// <returns>bool</returns>
        public static bool CheckearConexion()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }





        public void EnviarCorreoAlProveedor()
        {
            
        }








    }
}
