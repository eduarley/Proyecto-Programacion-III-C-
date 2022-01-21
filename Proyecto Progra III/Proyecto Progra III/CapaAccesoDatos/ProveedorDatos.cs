using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class ProveedorDatos
    {




        public void Insertar(Proveedor prov)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                conn.Open();
                string sql = "PA_InsertarProveedor";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", prov.ID);
                comando.Parameters.AddWithValue("@nombre", prov.Nombre);
                comando.Parameters.AddWithValue("@Direccion", prov.Direccion);
                comando.Parameters.AddWithValue("@Telefono", prov.Telefono);
                comando.Parameters.AddWithValue("@Correo", prov.Correo);
                comando.Parameters.AddWithValue("@Estado", prov.EstadoActivo);
                comando.ExecuteNonQuery();
            }

        }


        public List<Proveedor> SeleccionarTodosLosProveedores()
        {
            List<Proveedor> lista = new List<Proveedor>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();

                string sql = "PA_SeleccionarTodosLosProveedores";

                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {

                    Proveedor prov = new Proveedor()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Direccion= reader["Direccion"].ToString(),
                        Correo= reader["Correo"].ToString(),
                        Telefono = Convert.ToInt32(reader["telefono"]),

                    };
                    if (reader["Estado"].ToString() == "1")
                        prov.EstadoActivo = true;
                    else
                        prov.EstadoActivo = false;

                    lista.Add(prov);
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




        public Proveedor SeleccionarPorId(int id)
        {
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                string sql = "PA_SeleccionarProveedorPorId";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Proveedor prov = new Proveedor()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Nombre = reader["Nombre"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = Convert.ToInt32(reader["telefono"]),

                    };
                    if (reader["Estado"].ToString() == "1")
                        prov.EstadoActivo = true;
                    else
                        prov.EstadoActivo = false;
                    return prov;
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






        public void Modificar(Proveedor prov)
        {

            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            try
            {
                conexion.Open();
                string sql = "PA_ActualizarProveedor";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@id", prov.ID);
                comando.Parameters.AddWithValue("@nombre", prov.Nombre);
                comando.Parameters.AddWithValue("@Direccion", prov.Direccion);
                comando.Parameters.AddWithValue("@Telefono", prov.Telefono);
                comando.Parameters.AddWithValue("@Correo", prov.Correo);
                comando.Parameters.AddWithValue("@Estado", prov.EstadoActivo);

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









        public void ActivarDesactivar(Proveedor prov)
        {

            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            try
            {
                conexion.Open();
                string sql;
                if (prov.EstadoActivo)
                    sql = "PA_DesactivarProveedor";
                else
                    sql = "PA_ActivarProveedor";




                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@id", prov.ID);



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







    }
}
