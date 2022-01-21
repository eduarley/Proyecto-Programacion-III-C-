using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class CategoriaDatos
    {
        //cargar los combobox y demás

        public List<Categoria> SeleccionarTodas()
        {
            List<Categoria> lista = new List<Categoria>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                
                string sql = "PA_SeleccionarCategoriasUsuario";
                
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
               
                SqlDataReader reader = comando.ExecuteReader();
                
                while (reader.Read())
                {
                    Categoria cat = new Categoria();
                    cat.ID = (int)reader["Id"];
                    cat.Descripcion = reader["Descripcion"].ToString();
                    lista.Add(cat);
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




        public List<Usuario> SeleccionarGerentes()
        {
            List<Usuario> lista = new List<Usuario>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();

                string sql = "PA_SeleccionarGerentes";

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





        public List<Usuario> SeleccionarCajeros()
        {
            List<Usuario> lista = new List<Usuario>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();

                string sql = "PA_SeleccionarCajeros";

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









        public Categoria SeleccionarPorId(int id)
        {
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                // Paso 2: crear la instruccion sql 
                string sql = "PA_SeleccionarCategoriaPorId";
                // Paso 3: crear un objeto para ejecutar el sql
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                // Ejecutar comando con Consulta
                SqlDataReader reader = comando.ExecuteReader();
                // El reader retorna un conjunto de tuplas
                while (reader.Read())
                {
                    Categoria cat = new Categoria();
                    cat.ID = (int)reader["Id"];
                    cat.Descripcion = reader["Descripcion"].ToString();

                    return cat;
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


















        public List<Categoria> SeleccionarTodasCategoriasProducto()
        {
            List<Categoria> lista = new List<Categoria>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();

                string sql = "PA_SeleccionarCategoriasProducto";

                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Categoria cat = new Categoria();
                    cat.ID = (int)reader["Id"];
                    cat.Descripcion = reader["Descripcion"].ToString();
                    lista.Add(cat);
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



        public Categoria SeleccionarCategoriaProductoPorId(int id)
        {
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                // Paso 2: crear la instruccion sql 
                string sql = "PA_SeleccionarCategoriasProductoPorId";
                // Paso 3: crear un objeto para ejecutar el sql
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                // Ejecutar comando con Consulta
                SqlDataReader reader = comando.ExecuteReader();
                // El reader retorna un conjunto de tuplas
                while (reader.Read())
                {
                    Categoria cat = new Categoria();
                    cat.ID = (int)reader["Id"];
                    cat.Descripcion = reader["Descripcion"].ToString();

                    return cat;
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

    }
}
