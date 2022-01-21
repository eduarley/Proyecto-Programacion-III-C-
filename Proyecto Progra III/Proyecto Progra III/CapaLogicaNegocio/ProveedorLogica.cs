using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class ProveedorLogica
    {



        public void Nuevo(Proveedor prov)
        {
            ProveedorDatos datos = new ProveedorDatos();
            datos.Insertar(prov);
        }





        public List<Proveedor> ObtenerProveedores()
        {
            ProveedorDatos datos = new ProveedorDatos();
            return datos.SeleccionarTodosLosProveedores();
        }



        public void Guardar(Proveedor prov)
        {
            ProveedorDatos datos = new ProveedorDatos();
            if (datos.SeleccionarPorId(prov.ID) == null)
                datos.Insertar(prov);
            else
                datos.Modificar(prov);
        }







        public void ActivarODesactivar(Proveedor prov)
        {
            ProveedorDatos datos = new ProveedorDatos();
            datos.ActivarDesactivar(prov);
        }



















    }
}
