using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class MateriaPrimaDatos
    {

        public List<MateriaPrima> SeleccionarTodosLasMateriasPrimas()
        {
            List<MateriaPrima> lista = new List<MateriaPrima>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
              try
               {
            conn.Open();
            
            string sql = "PA_SeleccionarTodosLasMateriasPrimas";
            
            SqlCommand comando = new SqlCommand(sql, conn);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            
            SqlDataReader reader = comando.ExecuteReader();
           
            while (reader.Read())
            {
               MateriaPrima mat = new MateriaPrima();
               mat.ID = (int)reader["Id"];
               //mat.Proveedor.ID = (int)reader["Proveedor"];      //ARREGLAR ESTO
               mat.Descripcion =reader["Descripcion"].ToString();
               mat.Precio=  Convert.ToDecimal(reader["PrecioUnitario"]);
               mat.Existencias = Convert.ToInt32(reader["Existencias"]);
               mat.oProveedor = new ProveedorDatos().SeleccionarPorId((int)reader["IdProveedor"]);
               mat.Proveedor = reader["Proveedor"].ToString();




                    if (Convert.ToInt32(reader["Estado"]) == 1)
                        mat.EstadoActivo = true;
                    else
                        mat.EstadoActivo = false;


                    lista.Add(mat);
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










        public MateriaPrima SeleccionarPorId(int id)
        {
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                // Paso 2: crear la instruccion sql 
                string sql = "PA_SeleccionarMateriaPrimaPorId";
                // Paso 3: crear un objeto para ejecutar el sql
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                // Ejecutar comando con Consulta
                SqlDataReader reader = comando.ExecuteReader();
                // El reader retorna un conjunto de tuplas
                while (reader.Read())
                {
                    MateriaPrima mat = new MateriaPrima()
                    {
                        Descripcion=reader["Descripcion"].ToString(),
                        Existencias = (int)reader["Existencias"],
                        ID=(int)reader["ID"],
                        Proveedor= reader["ProveedorNombre"].ToString(),
                        Precio=(decimal)reader["PrecioUnitario"]
                        //AQUI NO HE SETIADO EL PROVEEDOR!!!!!
                    };

                    if (Convert.ToInt32(reader["Estado"]) == 1)
                        mat.EstadoActivo = true;
                    else
                        mat.EstadoActivo = false;


                    return mat;
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












        public void Insertar(MateriaPrima mat)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {


                conn.Open();
                string sql = "PA_InsertarMateria";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", mat.ID);
                comando.Parameters.AddWithValue("@descripcion", mat.Descripcion);
                comando.Parameters.AddWithValue("@preciounitario", mat.Precio);
                comando.Parameters.AddWithValue("@Idproveedor", mat.oProveedor.ID);
                comando.Parameters.AddWithValue("@Existencias", mat.Existencias);
                if (mat.EstadoActivo)
                    comando.Parameters.AddWithValue("@Estado", 1);
                else
                    comando.Parameters.AddWithValue("@Estado", 0);
                comando.ExecuteNonQuery();
            }
        }



        public void Modificar(MateriaPrima mat)
        {

            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            try
            {
                conexion.Open();
                string sql = "PA_ActualizarMateria";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@id", mat.ID);
                comando.Parameters.AddWithValue("@descripcion", mat.Descripcion);
                comando.Parameters.AddWithValue("@preciounitario", mat.Precio);
                comando.Parameters.AddWithValue("@Idproveedor", mat.oProveedor.ID);
                comando.Parameters.AddWithValue("@Existencias", mat.Existencias);
                if (mat.EstadoActivo)
                    comando.Parameters.AddWithValue("@Estado", 1);
                else
                    comando.Parameters.AddWithValue("@Estado", 0);

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



        public void ActivarDesactivar(MateriaPrima mat)
        {

            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            try
            {
                conexion.Open();
                string sql;
                if (mat.EstadoActivo)
                    sql = "PA_DesactivarMateriaPrima";
                else
                    sql = "PA_ActivarMateriaPRima";




                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@id", mat.ID);



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
