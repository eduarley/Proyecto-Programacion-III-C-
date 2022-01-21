using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class EnvioDatos
    {

        public List<Envio> SeleccionarTodosLosEnvios()
        {
            List<Envio> lista = new List<Envio>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();

                string sql = "PA_SeleccionarTodosLosEnvios";

                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Envio envio = new Envio();
                    
                    envio.PedidoFacturaID = Convert.ToInt32(reader["IdPedidoFactura"]);


                    if ((int)reader["Estado"] == 0)
                        envio.Estado = "No entregado";
                    else
                        envio.Estado = "Entregado";




                if (!string.IsNullOrEmpty(reader["IdEmpleadoACargo"].ToString()))
                {

                    envio.Empleado = new UsuarioDatos().SeleccionarPorId((int)reader["IdEmpleadoACargo"]).Apellido;
                    envio.oEmpleado = new UsuarioDatos().SeleccionarPorId((int)reader["IdEmpleadoACargo"]);
                }






                lista.Add(envio);

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






        public void InsertarEmpleado(int empleadoId, int pedidofacturaId)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                conn.Open();
                string sql = "PA_AsignarEmpleadoAlPedido";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idEmpleado", empleadoId);
                comando.Parameters.AddWithValue("@idPedidoFactura", pedidofacturaId);
                comando.ExecuteNonQuery();
            }

        }






        public Factura SeleccionarFacturaPorEnvio(int envioId)
        {
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                string sql = "PA_SeleccionarFacturaPorEnvio";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idPedidoFactura",envioId);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {

                    Factura fact = new FacturaDatos().SeleccionarPorId((int)reader["id"]);
                    fact.ListaDetalleFactura = new DetalleFacturaDatos().SeleccionarDetallePorFactura(fact.ID_Factura);
                    fact.oEncargo = new EncargoDatos().SeleccionarPorFactura(fact.ID_Factura);


                    return fact;
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











        public void RealizarEnvio(int pedidofacturaId)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                conn.Open();
                string sql = "PA_RealizarEnvio";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idPedidoFactura", pedidofacturaId);
                comando.ExecuteNonQuery();
            }

        }

    }
}
