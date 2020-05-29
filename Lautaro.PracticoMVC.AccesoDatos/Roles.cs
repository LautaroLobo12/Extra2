using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace Lautaro.PracticoMVC.AccesoDatos
{
  public  class Roles
    {
        string cadenaConexion = Conexiones.ObtenerCadenaConexion();

        public List<Entidades.Roles> Listar()
        {
            List<Entidades.Roles> lista = new List<Entidades.Roles>();

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT ");
            consultaSQL.Append("Id, ");
            consultaSQL.Append("Descripcion ");
            consultaSQL.Append("FROM ROLES ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                lista = connection.Query<Entidades.Roles>(consultaSQL.ToString()).ToList();
            }

            return lista;
        }


    }
}
