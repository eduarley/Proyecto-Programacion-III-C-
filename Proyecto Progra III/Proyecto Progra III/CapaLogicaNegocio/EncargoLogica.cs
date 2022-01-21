using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class EncargoLogica
    {

        public void Nuevo(Encargo encargo)
        {
            EncargoDatos datos = new EncargoDatos();
            datos.Insertar(encargo);
        }


        public Encargo SeleccionarEncargoPorFactura(Factura fact)
        {
            EncargoDatos datos = new EncargoDatos();
            return datos.SeleccionarPorFactura(fact.ID_Factura);
        }

    }
}
