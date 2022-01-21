using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class UsuarioLogica
    {

        public void Nuevo(Usuario usuario)
        {
            UsuarioDatos datos = new UsuarioDatos();
            datos.Insertar(usuario);
        }





        public Usuario Login(int id, string pass)
        {
            UsuarioDatos datos = new UsuarioDatos();

            return datos.Login(id, pass);

        }



        

        public void Guardar(Usuario usuario)
        {
            UsuarioDatos datos = new UsuarioDatos();
            if (datos.SeleccionarPorId(usuario.ID) == null)
                datos.Insertar(usuario);
            else
                datos.Modificar(usuario);
        }


    

        public List<Usuario> ObtenerUsuarios()
        {
            UsuarioDatos datos = new UsuarioDatos();
            return datos.SeleccionarTodosLosUsuarios();
        }
        public List<Usuario> ObtenerClientes()
        {
            UsuarioDatos datos = new UsuarioDatos();
            return datos.SeleccionarClientes();
        }


        public void ActivarODesactivar(Usuario usuario)
        {
            UsuarioDatos datos = new UsuarioDatos();
            datos.ActivarDesactivar(usuario);
        }
        
    }
}
