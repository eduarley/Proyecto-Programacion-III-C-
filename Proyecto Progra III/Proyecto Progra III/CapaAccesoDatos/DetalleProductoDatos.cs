using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class DetalleProductoDatos
    {







        public List<DetalleProducto> SeleccionarPorProducto(int productoId)
        {

            List<DetalleProducto> lista = new List<DetalleProducto>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                string sql = "PA_SeleccionarDetalleProductoPorProducto";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idProducto", productoId);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                DetalleProducto det = new DetalleProducto();


                    det.Cantidad = (int)reader["Cantidad"];
                    det.oMateriaPrima = new MateriaPrimaDatos().SeleccionarPorId((int)reader["IdMateriaPrima"]);
                        

                    lista.Add(det);
                }
                //return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            //return null;
            return lista;
        }




        public void Insertar(DetalleProducto det, int IdProducto, int IdMateriaPrima)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {



                conn.Open();
                string sql = "PA_InsertarDetalleProducto";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdProducto", IdProducto);
                comando.Parameters.AddWithValue("@IdMateriaPrima", IdMateriaPrima);
                comando.Parameters.AddWithValue("@Cantidad", det.Cantidad);
                comando.Parameters.AddWithValue("@PrecioDetalle", det.PrecioDetalle);

                comando.ExecuteNonQuery();
            }
        }


        public void Eliminar(int IdProducto, int IdMateriaPrima)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {



                conn.Open();
                string sql = "PA_EliminarDetalleProducto";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdProducto", IdProducto);
                comando.Parameters.AddWithValue("@IdMateria", IdMateriaPrima);


                comando.ExecuteNonQuery();
            }
        }


    }
}
