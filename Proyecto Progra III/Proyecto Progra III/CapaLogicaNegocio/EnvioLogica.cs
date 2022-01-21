using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class EnvioLogica
    {

        public List<Envio> SeleccionarTodosLosEnvios()
        {
            EnvioDatos datos = new EnvioDatos();
            return datos.SeleccionarTodosLosEnvios();
        }

        public void AgregarEmpleado(int empleadoId, int pedidoFacturaId)
        {
            EnvioDatos datos = new EnvioDatos();
            datos.InsertarEmpleado(empleadoId, pedidoFacturaId);
        }




        public Factura SeleccionarFacturaPorEnvio(int envioId)
        {
            EnvioDatos datos = new EnvioDatos();
            return datos.SeleccionarFacturaPorEnvio(envioId);
        }



        public void RealizarEnvio(Envio envio)
        {
            EnvioDatos datos = new EnvioDatos();
            datos.RealizarEnvio(envio.PedidoFacturaID);
        }
    }
}
