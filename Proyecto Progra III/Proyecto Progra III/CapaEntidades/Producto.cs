using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Producto
    {
        public int ID { get; set; }
        
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
       // public string Categoria { get; set; }       
        public Categoria Categoria {  get; set; }
        public bool EstadoActivo { get; set; }
        public List<DetalleProducto> ListaDetalle { get; set; }
        public List<ImagenProducto> ListaImagen { get; set; }

        public decimal MontoExtra {internal get; set; }
        

        //public List<System.Windows.Forms.PictureBox> Imagenes { get; set; }


        public Producto()
        {
            ListaDetalle = new List<DetalleProducto>();
            ListaImagen = new List<ImagenProducto>();
           // Imagenes = new List<System.Windows.Forms.PictureBox>();
        }


        public void AgregarDetalle(DetalleProducto det)
        {
            ListaDetalle.Add(det);
        }

        public decimal Subtotal()
        {
            decimal monto = 0;
            foreach (DetalleProducto det in ListaDetalle)
            {
                monto += det.PrecioDetalle;
            }
            Precio = (decimal)monto;

            return monto+MontoExtra;
        }



        public override string ToString()
        {
            string hilera = "\nMateriales:\n";
            foreach (var item in ListaDetalle)
            {
                hilera += item.ToString();
            }
            

            hilera += "\n--------------------------------"+
            "\nMonto extra: " + MontoExtra.ToString("0,00")+
            "\nTotal: " + Subtotal().ToString("0,00")+" Colones";
            //hilera += "\nPrecio Final: " + PrecioFinal();


            return Descripcion+"\nCategoria: "+Categoria.Descripcion + hilera;
        }
    }
}
