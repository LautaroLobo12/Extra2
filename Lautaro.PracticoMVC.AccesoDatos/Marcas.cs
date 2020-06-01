using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace Lautaro.PracticoMVC.AccesoDatos
{
   public class Marcas
    {
        string cadenaConexion = Conexiones.ObtenerCadenaConexion();

        public List<Entidades.Marcas> Listar()
        {
            List<Entidades.Marcas> lista = new List<Entidades.Marcas>();

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT  Id, Nombre ");
            consultaSQL.Append("FROM Marcas ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                lista = connection.Query<Entidades.Marcas>(consultaSQL.ToString()).ToList();
            }
            return lista;
        }
    }
}
