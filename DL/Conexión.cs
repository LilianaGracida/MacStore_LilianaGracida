using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DL
{
    public class Conexión
    {
        public static string GetConnectionString()
        {
            string cadenaConexion = "Data Source=.;Initial Catalog=MacStoreLilianaGracida;User ID=sa;Password=pass@word1;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            return cadenaConexion;
        }
    }
}