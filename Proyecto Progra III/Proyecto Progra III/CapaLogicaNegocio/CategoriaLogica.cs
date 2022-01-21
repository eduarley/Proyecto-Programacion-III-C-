using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CategoriaLogica
    {


        public List<Categoria> ObtenerCategorias()
        {
            CategoriaDatos datos = new CategoriaDatos();
            return datos.SeleccionarTodas();
        }

        public List<Usuario> ObtenerGerentes()
        {
            CategoriaDatos datos = new CategoriaDatos();
            return datos.SeleccionarGerentes();
        }

        public List<Usuario> ObtenerClientes()
        {
            CategoriaDatos datos = new CategoriaDatos();
            return datos.SeleccionarClientes();
        }
        public List<Usuario> ObtenerCajeros()
        {
            CategoriaDatos datos = new CategoriaDatos();
            return datos.SeleccionarCajeros();
        }

        public List<Categoria> ObtenerCategoriasConTodos()
        {
            CategoriaDatos datos = new CategoriaDatos();
            var lista = datos.SeleccionarTodas();

            Categoria todas = new Categoria();
            todas.Descripcion = "Todos";
            todas.ID = -1;

            lista.Insert(0, todas);

            return lista;
        }


        public List<Categoria> ObtenerCategoriasProducto()
        {
            CategoriaDatos datos = new CategoriaDatos();
            return datos.SeleccionarTodasCategoriasProducto();
        }

    }
}
