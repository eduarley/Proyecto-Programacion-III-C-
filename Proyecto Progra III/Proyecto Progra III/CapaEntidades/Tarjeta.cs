using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Tarjeta:Pago
    {

        public long Numero { get; set; }
        public int Codigo { get; set; }
        public string Marca { get; set; }
        public override string Tipo
        {
            get
            {
                return "Tarjeta";
            }

            set
            {

            }
        }

        
       
    }
}
