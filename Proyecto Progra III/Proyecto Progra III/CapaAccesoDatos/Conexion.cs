using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class Conexion
    {
        public static string Cadena
        {
            get
            {
                string name = "UI.Properties.Settings.SqlServer";
                return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
        }



    }
}
