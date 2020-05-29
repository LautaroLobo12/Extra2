using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Dapper;


namespace Lautaro.PracticoMVC.AccesoDatos
{
    public class Clientes
    {
        string cadenaConexion = Conexiones.ObtenerCadenaConexion();

        public int Test()
        {

            int filasAfectadas = 0;

            StringBuilder consultaSQL = new StringBuilder();

            SqlConnection conexion = new SqlConnection(cadenaConexion);






            /*
 UPDATE Productos
 SET Nombre = Nombre, Descripcion = Descripcion, IdMarca = IdMarca, 
 PrecioUnitario = PrecioUnitario, Activo = Activo, UrlImange = UrlImange
 WHERE Codigo = 1005
         */

            consultaSQL.Append(" ");
            consultaSQL.Append(" ");
            consultaSQL.Append(" ");
            consultaSQL.Append(" ");
            consultaSQL.Append(" ");
            consultaSQL.Append(" ");
            consultaSQL.Append(" ");


            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                conexion.Open();



                filasAfectadas = conexion.Execute(consultaSQL.ToString(),
                   new
                   {
                       //CodigoParametro = obj.Codigo,
                       //NombreParametro = obj.Nombre,
                       //DescripcionParametro = obj.Descripcion,
                       //IdMarcaParametro = obj.IdMarca,
                       //PrecioUnitarioParametro = obj.PrecioUnitario,
                       //ActivoParametro = obj.Activo,
                       //UrlImangeParametro = obj.UrlImange
                   });



                transaccion.Commit();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                filasAfectadas = 0;

            }
            finally
            {

                conexion.Close();
            }


            return filasAfectadas;
        }

    }
}
