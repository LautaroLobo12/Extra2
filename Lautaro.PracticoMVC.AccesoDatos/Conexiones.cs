using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lautaro.PracticoMVC.AccesoDatos
{
    public class Conexiones
    {
        public static string ObtenerCadenaConexion()
        {
            return @"Data Source=NOTEBENJA;Initial Catalog=db_practico_lautaro;Integrated Security=True";
        }

    }
}
