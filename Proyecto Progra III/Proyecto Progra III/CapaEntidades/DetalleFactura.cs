using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class DetalleFactura
    {

        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        //public decimal PrecioDetalle { get; set; }




        public decimal PrecioDetalle()                    
        {
            return Producto.Precio * Cantidad;
        }

        public override string ToString()
        {
            return Producto.Descripcion + ", " + Cantidad + ", -->" + PrecioDetalle().ToString("0,00")+ " colones";
        }
    }
}
