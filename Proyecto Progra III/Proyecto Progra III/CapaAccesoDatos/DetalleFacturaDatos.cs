using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class DetalleFacturaDatos
    {



        public void Insertar(DetalleFactura detallefactura, Factura factura)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {

                conn.Open();
                string sql = "PA_InsertarDetalleFactura";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idFactura", factura.ID_Factura);   //aqui ver como sacar el ID identity de la factura
                comando.Parameters.AddWithValue("@idproducto", detallefactura.Producto.ID);
                comando.Parameters.AddWithValue("@cantidad", detallefactura.Cantidad);
                comando.Parameters.AddWithValue("@precio", detallefactura.PrecioDetalle());
                comando.ExecuteNonQuery();
            }
        }










        public List<DetalleFactura> SeleccionarDetallePorFactura(int facturaId)
        {

            List<DetalleFactura> lista = new List<DetalleFactura>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                string sql = "PA_SeleccionarDetalleFacturaPorFactura";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idFactura", facturaId);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DetalleFactura det = new DetalleFactura();

                    det.Producto = new ProductoDatos().SeleccionarPorId((int)reader["IdProductoFinal"]);
                    det.Cantidad = (int)reader["Cantidad"];



                    lista.Add(det);
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }










        }
    }
}
