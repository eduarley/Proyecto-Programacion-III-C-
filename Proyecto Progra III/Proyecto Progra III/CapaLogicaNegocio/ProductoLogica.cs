using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class ProductoLogica
    {
        public void Nuevo(Producto prod)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.Insertar(prod);
        }

        


        public void Guardar(Producto prod)
        {
            ProductoDatos datos = new ProductoDatos();
            if (datos.SeleccionarPorId(prod.ID) == null)
                datos.Insertar(prod);
            else
                datos.Modificar(prod);
        }




        public List<Producto> ObtenerProductos()
        {
            ProductoDatos datos = new ProductoDatos();
            return datos.SeleccionarTodosLosProductos();
        }

        public List<Producto> ObtenerProductosActivos()
        {
            ProductoDatos datos = new ProductoDatos();
            return datos.SeleccionarProductosActivos();
        }


        public void ActivarODesactivar(Producto prod)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.ActivarDesactivar(prod);
        }














        public void AgregarImagen(Producto prod,string descripcionImagen, System.Windows.Forms.PictureBox pbImg)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.InsertarImagen(prod.ID, descripcionImagen, pbImg);
        }




        public void VerImagen(System.Windows.Forms.PictureBox ptbImagen, string descripcion)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.verImagen(ptbImagen, descripcion);
        }



        public void CargarImagenes(System.Windows.Forms.ComboBox cmbImagen, int idProducto)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.CargarImagenes(cmbImagen,idProducto);
        }



        public void EliminarImagen(string descripcion)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.EliminarImagen(descripcion);
        }


        public void ManoObra(int idProd, decimal monto)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.ManoObra(idProd, monto);
        }


    }



}

