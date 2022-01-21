using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class MateriaPrima
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        
        public string Proveedor { get; set; }       
        public decimal Precio { get; set; }
        //existencias
        public int Existencias { get; set; }
        public bool EstadoActivo { get; set; }

        public Proveedor oProveedor { get; set; }  // con el internal, le quito que aparezca en el DataGidView
        
    }
}
