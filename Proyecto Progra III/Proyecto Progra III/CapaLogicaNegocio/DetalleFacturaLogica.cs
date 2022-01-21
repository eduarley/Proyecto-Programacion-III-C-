using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class DetalleFacturaLogica
    {

       

        public decimal PrecioDetalle(DetalleFactura detalle)                      
        {
            return detalle.Producto.Precio * detalle.Cantidad;
        }

       

        



        public void Nuevo(DetalleFactura detalle,Factura factura)
        {
            DetalleFacturaDatos datos = new DetalleFacturaDatos();
            datos.Insertar(detalle,factura);
        }

        public List<DetalleFactura> SeleccionarDetallePorFactura(Factura factura)
        {
            DetalleFacturaDatos datos = new DetalleFacturaDatos();
            return datos.SeleccionarDetallePorFactura(factura.ID_Factura);
        }

    }
}
