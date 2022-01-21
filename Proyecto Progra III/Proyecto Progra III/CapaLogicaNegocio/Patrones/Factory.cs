using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio.Patrones
{
    public class Factory
    {


        public static DetalleFactura CrearDetalleFactura(Producto prod, int cantidad)
        {
            DetalleFactura detallefactura = new DetalleFactura()
            {
                Producto = prod,
                Cantidad = cantidad
            };
            return detallefactura;
        }


        public static Factura CrearFactura(int id,Usuario cliente,DateTime fecha, string estado,Usuario empleado,Pago tipo,Encargo encargo,decimal monto)
        {
            Factura factura = new Factura()
            {
                Cliente=cliente,
                Empleado=empleado,
                Estado=estado,
                Fecha=fecha,
                ID_Factura=id,
                Monto=monto,
                oEncargo=encargo,
                Tipo=tipo
            };
            return factura;
        }


        public static Pago CrearPago(string tipo)
        {



            Pago pago = null;


            if (tipo == "Efectivo")
            {
                pago = new Efectivo();
            }
                
            if (tipo == "Tarjeta")
            {
                pago = new Tarjeta();
            }
                
            if (tipo == "Depósito")
            {
                pago = new Deposito();
            }


            return pago;

        }




        public static MateriaPrima CrearMateriaPrima(int id, string descripcion, Proveedor oproveedor,string proveedor,decimal precio,int existencias, bool estadoactivo)
        {
            MateriaPrima mat = new MateriaPrima()
            {
                Descripcion = descripcion,
                EstadoActivo = estadoactivo,
                Existencias = existencias,
                ID = id,
                oProveedor = oproveedor,
                Precio = precio,
                Proveedor = proveedor
            };

            return mat;
        }


        public static Proveedor CrearProveedor(int id,string nombre,string direccion, int telefono,string correo, bool estado)
        {
            Proveedor prov = new Proveedor()
            {
                Correo = correo,
                Direccion = direccion,
                EstadoActivo = estado,
                ID = id,
                Nombre = nombre,
                Telefono = telefono

            };
            return prov;
        }

    }
}
