using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Efectivo:Pago
    {

       


        public override string Tipo
        {
            get
            {
                return "Efectivo";
            }

            set
            {

            }
        }

        

    }
}
