using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Envio
    {
        public int PedidoFacturaID { get; set; }
        public Usuario oEmpleado {internal get; set; }
        public string Estado { get; set; }
        public string Empleado { get; set; }


       

        public Envio()
        {
            Empleado = "Sin asignar";
            oEmpleado = null;
        }

    }
}
