using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class MateriaPrimaLogica
    {

        public List<MateriaPrima> ObtenerProductos()
        {
            MateriaPrimaDatos datos = new MateriaPrimaDatos();
            return datos.SeleccionarTodosLasMateriasPrimas();
        }



        public void Guardar(MateriaPrima mat)
        {
            MateriaPrimaDatos datos = new MateriaPrimaDatos();
            if (datos.SeleccionarPorId(mat.ID) == null)
                datos.Insertar(mat);
            else
                datos.Modificar(mat);

        }


        public void ActivarODesactivar(MateriaPrima mat)
        {
            MateriaPrimaDatos datos = new MateriaPrimaDatos();
            datos.ActivarDesactivar(mat);
        }










        


    }
}
