using System;

namespace CapaEntidades
{
    public class Encargo
    {
        public Factura Factura { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public Usuario Encargado { get; set; }
        public DateTime Fecha { get; set; }

       

        public override string ToString()
        {

            string usuarioEncargado = "";

            if (Encargado != null)
                usuarioEncargado += Encargado.Nombre + " " + Encargado.Apellido;
            else
                usuarioEncargado += "Sin asignar";
            return 
                "\nSe solicitó por encargo."+
                "\nDirección: " + Direccion +
                "\nTeléfono disponible al recoger el encargo: " + Telefono +
                "\nFecha del envío: "+ Fecha+
               "\nEncargado de entrega: "+usuarioEncargado;
        }
    }
}
