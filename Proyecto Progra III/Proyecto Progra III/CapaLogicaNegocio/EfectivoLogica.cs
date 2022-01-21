using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class EfectivoLogica
    {


        public double Vuelto(Pago pago,double efectivo)
        {
            double m = 0d;
            if (efectivo >= pago.MontoPagar)
            {
                m += efectivo - pago.MontoPagar;
            }
            else
            {
                return -1;
            }


            return m;
        }
    }
}
