using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CapaEntidades
{
    public class Factura
    {
        //public static int ContNumero = 0;
        public int ID_Factura { get; set; }
        public Usuario Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public Usuario Empleado { get; set; }
        public Pago Tipo { get; set; }
        public List<DetalleFactura> ListaDetalleFactura { get; set; }
        
        public Encargo oEncargo {  get; set; }

        public decimal Monto { get; set; }

       

        public Factura()
        {
            ListaDetalleFactura = new List<DetalleFactura>();
            //this.ID = ContNumero++;
        }


        public void AgregarDetalle(DetalleFactura det)
        {
            ListaDetalleFactura.Add(det);
        }



        public decimal MontoTotal()
        {
            decimal monto = 0;
            foreach (var item in ListaDetalleFactura)
            {
                monto += item.PrecioDetalle();
            }
            return monto;
        }


        public override string ToString()
        {
            
            string detalle = "\nDetalle Factura: \n";
            foreach (var item in ListaDetalleFactura)
            {
                detalle += "\n>" + item.Producto.Descripcion + ", Cantidad: " + item.Cantidad.ToString() + ", Precio: " + item.PrecioDetalle().ToString("0,00") + "\n";
            }



            string tipoPago = "\nTipo de Pago: ";
            if (Tipo != null)
            {
                tipoPago += Tipo.Tipo;
            }
                

            else
                tipoPago += "sin asignar";



            string cedulaCliente = "";
            if (Cliente != null)
                cedulaCliente += Cliente.ID;
            


            return "Factura N°: " + ID_Factura.ToString("0000000000") +
                 "\nCliente: " +cedulaCliente+
                "\nFecha: " + Fecha +
                "\nEmpleado a Cobro: " +(Empleado==null?"Sin asignar":Empleado.Nombre+" "+Empleado.Apellido )      +

              
                  tipoPago+
                "\nEstado de Factura: " +Estado+


                (oEncargo == null ? "\nEntrega realizada el mismo día" : oEncargo.ToString())+
                

                detalle +
                "\n--------------------------------\nMonto total Factura: " + MontoTotal().ToString("0.00");
                



    /*
            XmlDocument xmlDoc = new XmlDocument();


            XmlElement nodoFactura = xmlDoc.CreateElement("Factura");
            nodoFactura.SetAttribute("Número", ID_Factura.ToString());
            nodoFactura.SetAttribute("Fecha", Fecha.ToString());

            XmlElement nodoCliente = xmlDoc.CreateElement("Cliente");
            nodoCliente.SetAttribute("Identificación", Cliente.ID.ToString());
            nodoCliente.SetAttribute("Nombre", Cliente.Nombre);
            nodoCliente.SetAttribute("Apellido", Cliente.Apellido);


            XmlElement nodoEmpleado = xmlDoc.CreateElement("Empleado");
            if (Empleado != null)
            {
                nodoEmpleado.SetAttribute("Identificación", Empleado.ID.ToString());
                nodoEmpleado.SetAttribute("Nombre", Empleado.Nombre);
                nodoEmpleado.SetAttribute("Apellido", Empleado.Apellido);
            }




            XmlElement nodoPago = xmlDoc.CreateElement("Pago");
            if (Tipo != null)
            {
                nodoPago.SetAttribute("Tipo", Tipo.Tipo);
                if (Tipo is Tarjeta)
                {
                    nodoPago.SetAttribute("Marca", ((Tarjeta)Tipo).Marca.ToString());
                    nodoPago.SetAttribute("Número", ((Tarjeta)Tipo).Numero.ToString());
                }
                if (Tipo is Efectivo)
                {
                    nodoPago.SetAttribute("Paga con", "Arreglar esto en el xml");
                    nodoPago.SetAttribute("Su cambio", "Arreglar esto tambien");//((EfectivoLogica)factura.Tipo).Vuelto.ToString("0.00"));
                }
                if (Tipo is Deposito)
                {
                    nodoPago.SetAttribute("Cuenta", ((Deposito)Tipo).CodigoIBAN.ToString());
                }


            }



            XmlElement nodoEncargo = xmlDoc.CreateElement("Encargo");
            if (oEncargo != null)
            {
                nodoEncargo.InnerText = "Se solicitó por encargo";
                nodoEncargo.SetAttribute("Dirección", oEncargo.Direccion);
                nodoEncargo.SetAttribute("Teléfono disponible", oEncargo.Telefono);
                nodoEncargo.SetAttribute("Encargado de entrega", oEncargo.Encargado.Nombre + " " + oEncargo.Encargado.Apellido);
                nodoEncargo.SetAttribute("Fecha del envío", oEncargo.Fecha.ToString());

            }



            XmlElement nodoDetalle = xmlDoc.CreateElement("Detalle");




            foreach (DetalleFactura det in ListaDetalleFactura)
            {
                XmlElement detalle = xmlDoc.CreateElement("Adicional");
                detalle.InnerText = det.ToString();
                nodoDetalle.AppendChild(detalle);
            }





            nodoFactura.SetAttribute("Total", MontoTotal().ToString("0,00"));

            nodoFactura.AppendChild(nodoCliente);
            nodoFactura.AppendChild(nodoEmpleado);
            nodoFactura.AppendChild(nodoPago);
            nodoFactura.AppendChild(nodoEncargo);
            //xmlDoc.DocumentElement.AppendChild(nodoFactura);
            xmlDoc.AppendChild(nodoFactura);
            return xmlDoc.OuterXml.ToString();

    */



        }

    }
}
