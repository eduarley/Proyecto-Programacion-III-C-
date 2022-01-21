using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class ProductoDatos
    {


        public void Insertar(Producto prod)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {



                conn.Open();
                string sql = "PA_InsertarProducto";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", prod.ID);
                comando.Parameters.AddWithValue("@descripcion", prod.Descripcion);
                comando.Parameters.AddWithValue("@precio", prod.Precio);
                comando.Parameters.AddWithValue("@Idcategoria", prod.Categoria.ID);
                comando.Parameters.AddWithValue("@Estado", prod.EstadoActivo);
                comando.ExecuteNonQuery();
            }
        }


       



        public List<Producto> SeleccionarTodosLosProductos()
        {
            List<Producto> lista = new List<Producto>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
                
                string sql = "PA_SeleccionarTodosLosProductos";
                
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
         
                SqlDataReader reader = comando.ExecuteReader();
   
                while (reader.Read())
                {
                    Producto prod = new Producto();
                    prod.ID = (int)reader["Id"];
                    prod.Descripcion = reader["Descripcion"].ToString();

                    prod.Precio = Convert.ToDecimal(reader["Precio"]);
                    //prod.Categoria = reader["Categoria"].ToString();  /////////////////////////////ARREGLAR ESTO///////////////
                    //prod.Categoria = reader["DescripcionCategoria"].ToString();
                    if (reader["Estado"].ToString() == "1")
                        prod.EstadoActivo = true;
                    else
                        prod.EstadoActivo = false;


                    prod.Categoria= new CategoriaDatos().SeleccionarCategoriaProductoPorId((int)reader["IdCategoriaProducto"]);


                    lista.Add(prod);
                    
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

        public List<Producto> SeleccionarProductosActivos()
        {
            List<Producto> lista = new List<Producto>();
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();

                string sql = "PA_SeleccionarProductosActivos";

                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Producto prod = new Producto();
                    prod.ID = (int)reader["Id"];
                    prod.Descripcion = reader["Descripcion"].ToString();

                    prod.Precio = Convert.ToDecimal(reader["Precio"]);
                    //prod.Categoria = reader["Categoria"].ToString();  /////////////////////////////ARREGLAR ESTO///////////////
                    //prod.Categoria = reader["DescripcionCategoria"].ToString();
                    if (reader["Estado"].ToString() == "1")
                        prod.EstadoActivo = true;
                    else
                        prod.EstadoActivo = false;


                    prod.Categoria = new CategoriaDatos().SeleccionarCategoriaProductoPorId((int)reader["IdCategoriaProducto"]);


                    lista.Add(prod);

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


        public Producto SeleccionarPorId(int id)
        {
            SqlConnection conn = new SqlConnection(Conexion.Cadena);
            try
            {
                conn.Open();
               
                string sql = "PA_SeleccionarProductoPorId";
            
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
               
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Producto prod = new Producto();
                    prod.ID = (int)reader["Id"];
                    prod.Descripcion = reader["Descripcion"].ToString();
                    prod.Precio = Convert.ToDecimal(reader["Precio"]);
                    //prod.Categoria = reader["Categoria"].ToString();

                    return prod;
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




        

        public void Modificar(Producto prod)
        {

            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            try
            {
                conexion.Open();
                string sql = "PA_ActualizarProducto";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@id", prod.ID);
                comando.Parameters.AddWithValue("@descripcion", prod.Descripcion);
                comando.Parameters.AddWithValue("@precio", prod.Precio);
                comando.Parameters.AddWithValue("@IdCategoria", prod.Categoria.ID);
                //comando.Parameters.AddWithValue("@categoria", prod.Categoria);


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







        

        public void ActivarDesactivar(Producto prod)
        {

            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            try
            {
                conexion.Open();
                string sql;
                if (prod.EstadoActivo)
                    sql = "PA_DesactivarProducto";
                else
                    sql = "PA_ActivarProducto";




                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@id", prod.ID);



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








       ////////////////////////////////IMAGEN DEL PRODUCTO



        public void InsertarImagen(int idProducto,string descripcionImagen, System.Windows.Forms.PictureBox pbImagen)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                conn.Open();
                string sql = "PA_InsertarImagenProducto";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = CommandType.StoredProcedure;


                comando.Parameters.Add("@IdProductoFinal", SqlDbType.Int);
                comando.Parameters.Add("@Descripcion", SqlDbType.NChar);
                comando.Parameters.Add("@Img", SqlDbType.Image);


                comando.Parameters["@IdProductoFinal"].Value = idProducto;
                comando.Parameters["@Descripcion"].Value = descripcionImagen;


                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                pbImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                comando.Parameters["@Img"].Value = ms.GetBuffer();
                comando.ExecuteNonQuery();
            }
        }












        public void verImagen(System.Windows.Forms.PictureBox pbFoto, string descripcion)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
                {

                    SqlDataAdapter da = new SqlDataAdapter("select img from ImagenProducto where Descripcion= '" + descripcion + "'", conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "IMG");
                    byte[] datos = new byte[0];
                    DataRow dr = ds.Tables["IMG"].Rows[0];
                    datos = (byte[])dr["img"];
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(datos);
                    pbFoto.Image = System.Drawing.Bitmap.FromStream(ms);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }









        public void CargarImagenes(System.Windows.Forms.ComboBox cmbImg, int idProducto)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                   

                    conn.Open();

                string sql = "PA_SeleccionarDescripcionImagen";

                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idproducto", idProducto);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    cmbImg.Items.Add(reader["Descripcion"]);
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
        }


        }













        public void EliminarImagen(string descripcion)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {



                conn.Open();
                string sql = "PA_EliminarImagen";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Descripcion", descripcion);
                comando.ExecuteNonQuery();
            }
        }


        public void ManoObra(int idProd, decimal monto)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {



                conn.Open();
                string sql = "PA_AgregarManoObra";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdProducto", idProd);
                comando.Parameters.AddWithValue("@Monto", monto);
                comando.ExecuteNonQuery();
            }
        }



    }
    }
