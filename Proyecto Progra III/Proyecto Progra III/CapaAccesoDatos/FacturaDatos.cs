using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class FacturaDatos
    {



        public void Insertar(Factura factura)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {

                conn.Open();
                string sql = "PA_InsertarFactura";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", factura.ID_Factura);
                comando.Parameters.AddWithValue("@fecha",factura.Fecha);
                comando.Parameters.AddWithValue("@IdCliente", factura.Cliente.ID);
                comando.Parameters.AddWithValue("@Monto", factura.MontoTotal());
                //comando.Parameters.AddWithValue("@TipoPago", factura.Pago.Tipo);
                comando.Parameters.AddWithValue("@Estado", factura.Estado);
                //comando.Parameters.AddWithValue("@IdFuncionario", factura.Empleado.ID);
                comando.ExecuteNonQuery();
            }
        }



        public int UltimoId()
        {
            int UltimoId=0;
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {

                conn.Open();
                string sql = "PA_SeleccionarUltimoIDFactura";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
               
                while (reader.Read())
                {
                    UltimoId = (int)reader["ID"];
                }
                //comando.ExecuteNonQuery();
                return UltimoId;
                
            }
        }



        public List<Factura> SeleccionarTodasLasFacturasPendientes()
        {
            List<Factura> lista = new List<Factura>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {

                conn.Open();

                string sql = "PA_SeleccionarTodasLasFacturasPendientes";

                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Factura fact = new Factura();
                    fact.ID_Factura = (int)reader["ID"];
                    fact.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    fact.Cliente = new UsuarioDatos().SeleccionarPorId((int)reader["IdCliente"]);
                //fact.Empleado = new UsuarioDatos().SeleccionarPorId((int)reader["IdFuncionario"]);
                //fact.MontoTotal = Convert.ToDecimal(reader["Monto"]);
                fact.Monto = Convert.ToDecimal(reader["Monto"]);

                    if (reader["Pago"].ToString() == "Efectivo")
                        fact.Tipo = new Efectivo();
                    if (reader["Pago"].ToString() == "Deposito")
                        fact.Tipo = new Deposito();
                    if (reader["Pago"].ToString() == "Tarjeta")
                        fact.Tipo = new Tarjeta();
                    



                if (reader["Estado"].ToString() == "Pendiente")
                    fact.Estado = "Pendiente";
                if (reader["Estado"].ToString() == "Pagado")
                    fact.Estado = "Pagado";



                // fact.Empleado =new               reader["idFuncionario"];


                lista.Add(fact);

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










        public void Actualizar(Factura factura)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {

                conn.Open();
                string sql = "PA_PagarFactura";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", factura.ID_Factura);
                comando.Parameters.AddWithValue("@TipoPago", factura.Tipo.Tipo);
                comando.Parameters.AddWithValue("@IdFuncionario", factura.Empleado.ID);
                comando.ExecuteNonQuery();
            }
        }




        public Factura SeleccionarPorId(int id)
        {
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                // Paso 2: crear la instruccion sql 
                string sql = "PA_SeleccionarFacturaPorId";
                // Paso 3: crear un objeto para ejecutar el sql
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                // Ejecutar comando con Consulta
                SqlDataReader reader = comando.ExecuteReader();
                // El reader retorna un conjunto de tuplas
                while (reader.Read())
                {
                    //Factura fact = new Factura();
                    //fact.Cliente = new UsuarioDatos().SeleccionarPorId((int)reader["IdCliente"]);
                    //fact.Empleado= new UsuarioDatos().SeleccionarPorId((int)reader["IdFuncionario"]);
                    //fact.Estado = reader["Estado"].ToString();
                    //fact.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    //fact.ID_Factura = Convert.ToInt32(reader["Id"]);
                    //fact.Monto = Convert.ToDecimal(reader["monto"]);

                    Factura fact = new Factura();
                    fact.ID_Factura = (int)reader["ID"];
                    fact.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    fact.Cliente = new UsuarioDatos().SeleccionarPorId((int)reader["IdCliente"]);


                    fact.Monto = Convert.ToDecimal(reader["Monto"]);

                    if (reader["Pago"].ToString() == "Efectivo")
                        fact.Tipo = new Efectivo();
                    if (reader["Pago"].ToString() == "Deposito")
                        fact.Tipo = new Deposito();
                    if (reader["Pago"].ToString() == "Tarjeta")
                        fact.Tipo = new Tarjeta();




                    if (reader["Estado"].ToString() == "Pendiente")
                        fact.Estado = "Pendiente";
                    if (reader["Estado"].ToString() == "Pagado")
                        fact.Estado = "Pagado";




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









        public void ActualizarTipoPago(int idFactura, string tipoPago)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {

                conn.Open();
                string sql = "PA_ActualizarTipoPago";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idFactura", idFactura);
                comando.Parameters.AddWithValue("@pago", tipoPago);
                comando.ExecuteNonQuery();
            }
        }


    }
}
