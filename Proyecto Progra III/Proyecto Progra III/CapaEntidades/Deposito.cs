using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Deposito : Pago
    {
        public override string Tipo
        {
            get
            {
                return "Depósito bancario";
            }

            set
            {
                
            }
        }

        public string CodigoIBAN { get; set; }

    }
}
