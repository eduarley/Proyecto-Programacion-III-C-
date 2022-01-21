using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public abstract class Usuario
    {
        public bool EstadoActivo { get; set; }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        //public abstract string Tipo { get; set; }
        public Categoria Categoria { get; set; }
        public string Direccion { get; set; }

        public int IdCategoria { internal get; set; }


        public long Telefono { get; set; }

        public string Correo { get; set; }
        public string Contrasena {  get;  set; }
        //AMBOS PUEDEN: 
        //gestionar clientes
        //gestionar envios
        //ver reportes

        public override string ToString()
        {
            return ID.ToString(); //"Empleado Id: " + ID + "Nombre: " + Nombre + Apellido + Tipo + Contrasena + Direccion + Telefono + Correo + EstadoActivo;
        }

    }
}
