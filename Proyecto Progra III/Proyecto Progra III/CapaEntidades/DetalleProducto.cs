using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class DetalleProducto
    {
        public MateriaPrima oMateriaPrima { get; set; }


        public int Cantidad { get; set; }

        public decimal PrecioDetalle { get; set; }
        
        


        public override string ToString()
        {
            this.PrecioDetalle = oMateriaPrima.Precio * this.Cantidad;
            return  "\n"+ this.oMateriaPrima.Descripcion + " ,Cantidad: " + this.Cantidad + " ,Precio: " + PrecioDetalle.ToString("0,00")+ " Colones";
        }

    }
}
