using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class DetalleProductoLogica
    {



        public List<DetalleProducto> ObtenerMateriasPrimasPorProducto(Producto prod)
        {
            DetalleProductoDatos datos = new DetalleProductoDatos();
            return datos.SeleccionarPorProducto(prod.ID);
        }


        public void NuevoDetalleProducto(DetalleProducto det, Producto Idproducto, MateriaPrima idMateriaPrima)
        {
            DetalleProductoDatos datos = new DetalleProductoDatos();
            datos.Insertar(det, Idproducto.ID, idMateriaPrima.ID);
        }

        public void EliminarDetalle(Producto Idproducto, MateriaPrima idMateriaPrima)
        {
            DetalleProductoDatos datos = new DetalleProductoDatos();
            datos.Eliminar(Idproducto.ID, idMateriaPrima.ID);
        }

    }
}
