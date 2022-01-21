using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class TarjetaLogica
    {



 

        public bool ValidarTarjeta(Tarjeta tarjeta)
        {
            return tarjeta.Numero.ToString().All(char.IsDigit) && tarjeta.Numero.ToString().Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                    ? thisNum
                    : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                ).Sum() % 10 == 0;
        }

    }
}
