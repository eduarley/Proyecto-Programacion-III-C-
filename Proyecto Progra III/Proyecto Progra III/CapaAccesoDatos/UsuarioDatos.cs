using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class UsuarioDatos
    {
        public void Insertar(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                conn.Open();
                string sql = "PA_InsertarUsuario";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", usuario.ID);
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                comando.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                comando.Parameters.AddWithValue("@Estado", usuario.EstadoActivo);
                //comando.Parameters.AddWithValue("@Tipo", usuario.Tipo);
                comando.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                comando.Parameters.AddWithValue("@Correo", usuario.Correo);
                comando.Parameters.AddWithValue("@IdCategoria", usuario.Categoria.ID);
                comando.ExecuteNonQuery();
            }

        }









        public Usuario Login(int ID, string pass )
        {
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();               
                string sql = "PA_LoginUsuario";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", ID);
                comando.Parameters.AddWithValue("@contrasena", pass);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {

                    Usuario usuario = null;

                    switch ((int)reader["IdCategoria"])
                    {
                        case 0: usuario = new Cliente(); break;
                        case 1: usuario = new Cajero(); break;
                        case 2: usuario = new Gerente(); break;
                    }



                    usuario.ID = Convert.ToInt32(reader["ID"]);
                    usuario.Nombre = reader["Nombre"].ToString();
                    usuario.Apellido = reader["Apellido"].ToString();
                    //usuario.Tipo= reader["Tipo"].ToString();
                    usuario.Direccion = reader["Direccion"].ToString();
                    usuario.Categoria = new CategoriaDatos().SeleccionarPorId((int)reader["IdCategoria"]);

                    if (reader["Estado"].ToString() == "1")
                        usuario.EstadoActivo = true;
                    else
                        usuario.EstadoActivo = false;

                    usuario.Telefono = long.Parse(reader["Telefono"].ToString());
                    usuario.Correo = reader["Correo"].ToString();
                    usuario.IdCategoria = (int)reader["IdCategoria"];




                    return usuario;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return null;
        }




        
        public List<Usuario> SeleccionarTodosLosUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();

                string sql = "PA_SeleccionarTodosLosUsuarios";

                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    
                    Usuario usuario = null;
              
                    switch ((int)reader["IdCategoria"])
                    {
                        case 0: usuario = new Cliente(); break;
                        case 1: usuario = new Cajero(); break;
                        case 2: usuario = new Gerente(); break;
                    }



                    usuario.ID = Convert.ToInt32(reader["ID"]);
                    usuario.Nombre = reader["Nombre"].ToString();
                    usuario.Apellido = reader["Apellido"].ToString();
                    //usuario.Tipo= reader["Tipo"].ToString();
                    usuario.Direccion= reader["Direccion"].ToString();
                    usuario.Categoria = new CategoriaDatos().SeleccionarPorId((int)reader["IdCategoria"]);

                    if (reader["Estado"].ToString() == "1")
                        usuario.EstadoActivo = true;
                    else
                        usuario.EstadoActivo = false;

                    usuario.Telefono = long.Parse(reader["Telefono"].ToString());
                    usuario.Correo = reader["Correo"].ToString();
                    usuario.IdCategoria = (int)reader["IdCategoria"];


                   // usuario.Contrasena = reader["contrasena"].ToString();


                    lista.Add(usuario);
                }
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return lista;
        }



        
        public Usuario SeleccionarPorId(int id)
        {
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                string sql = "PA_SeleccionarUsuarioPorId";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = null;
                    switch ((int)reader["IdCategoria"])
                    {
                        case 0: usuario = new Cliente(); break;
                        case 1: usuario = new Cajero(); break;
                        case 2: usuario = new Gerente(); break;
                    }



                    usuario.ID = Convert.ToInt32(reader["ID"]);
                    usuario.Nombre = reader["Nombre"].ToString();
                    usuario.Apellido = reader["Apellido"].ToString();
                    //usuario.Tipo= reader["Tipo"].ToString();
                    usuario.Direccion = reader["Direccion"].ToString();
                    usuario.Categoria = new CategoriaDatos().SeleccionarPorId((int)reader["IdCategoria"]);

                    if (reader["Estado"].ToString() == "1")
                        usuario.EstadoActivo = true;
                    else
                        usuario.EstadoActivo = false;

                    usuario.Telefono = long.Parse(reader["Telefono"].ToString());
                    usuario.Correo = reader["Correo"].ToString();
                    usuario.IdCategoria = (int)reader["IdCategoria"];

                    return usuario;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return null;
        }




        

        public void Modificar(Usuario usuario)
        {

            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            try
            {
                conexion.Open();
                string sql = "PA_ActualizarUsuario";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@id", usuario.ID);
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                comando.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                comando.Parameters.AddWithValue("@Estado", usuario.EstadoActivo);
                //comando.Parameters.AddWithValue("@Tipo", usuario.Tipo);
                comando.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                comando.Parameters.AddWithValue("@Correo", usuario.Correo);
                comando.Parameters.AddWithValue("@Idcategoria", usuario.Categoria.ID);

                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }

        }





        



        public void ActivarDesactivar(Usuario usuario)
        {

            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            try
            {
                conexion.Open();
                string sql;
                if (usuario.EstadoActivo)
                    sql = "PA_DesactivarUsuario";
                else
                    sql = "PA_ActivarUsuario";




                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@id", usuario.ID);



                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }

        }
















        public List<Usuario> SeleccionarClientes()
        {
            List<Usuario> lista = new List<Usuario>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();

                string sql = "PA_SeleccionarClientes";

                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {

                    Usuario usuario = null;

                    switch ((int)reader["IdCategoria"])
                    {
                        case 0: usuario = new Cliente(); break;
                        case 1: usuario = new Cajero(); break;
                        case 2: usuario = new Gerente(); break;
                    }



                    usuario.ID = Convert.ToInt32(reader["ID"]);
                    usuario.Nombre = reader["Nombre"].ToString();
                    usuario.Apellido = reader["Apellido"].ToString();
                    //usuario.Tipo= reader["Tipo"].ToString();
                    usuario.Direccion = reader["Direccion"].ToString();
                    usuario.Categoria = new CategoriaDatos().SeleccionarPorId((int)reader["IdCategoria"]);

                    if (reader["Estado"].ToString() == "1")
                        usuario.EstadoActivo = true;
                    else
                        usuario.EstadoActivo = false;

                    usuario.Telefono = long.Parse(reader["Telefono"].ToString());
                    usuario.Correo = reader["Correo"].ToString();
                    usuario.IdCategoria = (int)reader["IdCategoria"];


                    // usuario.Contrasena = reader["contrasena"].ToString();


                    lista.Add(usuario);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return lista;
        }






    }
}
