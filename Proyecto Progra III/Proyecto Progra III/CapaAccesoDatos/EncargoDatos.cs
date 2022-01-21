using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class EncargoDatos
    {



        public void Insertar(Encargo encargo)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {



                conn.Open();
                string sql = "PA_InsertarPedido";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idfactura", encargo.Factura.ID_Factura);
                comando.Parameters.AddWithValue("@direccion", encargo.Direccion);
                comando.Parameters.AddWithValue("@telefono", encargo.Telefono);
                //comando.Parameters.AddWithValue("@Idencargado", encargo.Encargado.ID);
                comando.Parameters.AddWithValue("@fecha", encargo.Fecha);
                comando.ExecuteNonQuery();
            }
        }



        public Encargo SeleccionarPorFactura(int facturaId)
        {

           
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            Encargo encargo = null;
            try
            {
                conn.Open();
                string sql = "PA_SeleccionarPedidoPorFactura";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idfactura", facturaId);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    encargo = new Encargo();
                    //encargo.Factura=
                    encargo.Direccion = reader["direccion"].ToString();
                    //if(reader["idencargado"]!=null)
                    //encargo.Encargado = new UsuarioDatos().SeleccionarPorId((int)reader["idencargado"]);
                    encargo.Telefono= reader["telefono"].ToString();
                    encargo.Fecha = Convert.ToDateTime(reader["fecha"]);

                    return encargo;
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

            return encargo;
            
        }


    }
}
