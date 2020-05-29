using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace Lautaro.PracticoMVC.AccesoDatos
{
    public class Productos
    {
        string cadenaConexion = Conexiones.ObtenerCadenaConexion();

        public List<Entidades.Join_ProductosMarcas> Listar()
        {
            List<Entidades.Join_ProductosMarcas> lista = new List<Entidades.Join_ProductosMarcas>();

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT ");
            consultaSQL.Append("Productos.Codigo as CODIGO, ");
            consultaSQL.Append("Productos.Nombre AS NOMBRE, ");
            consultaSQL.Append("Marcas.Nombre AS MARCA, ");
            consultaSQL.Append("Productos.PrecioUnitario AS PRECIO, ");
            consultaSQL.Append("Productos.Activo AS ACTIVO ");
            consultaSQL.Append("FROM Productos ");
            consultaSQL.Append("INNER JOIN Marcas ON  ");
            consultaSQL.Append("Productos.IdMarca = Marcas.Id ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                lista = connection.Query<Entidades.Join_ProductosMarcas>(consultaSQL.ToString()).ToList();
            }

            return lista;
        }

        public Entidades.Productos Detalle(int id)
        {
            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT ");
            consultaSQL.Append("Codigo, ");
            consultaSQL.Append("Nombre, ");
            consultaSQL.Append("Descripcion, ");
            consultaSQL.Append("IdMarca, ");
            consultaSQL.Append("PrecioUnitario, ");
            consultaSQL.Append("Activo,  ");
            consultaSQL.Append("UrlImange ");
            consultaSQL.Append("FROM Productos ");
            consultaSQL.Append("WHERE Codigo = @codigoParametro ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                var obj = connection.QuerySingleOrDefault<Entidades.Productos>(consultaSQL.ToString(), new { codigoParametro = id });

                return obj;
            }

        }

        public void Crear(Entidades.Productos obj)
        {
            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("INSERT INTO Productos(Nombre, Descripcion, IdMarca, PrecioUnitario, Activo, UrlImange)  ");
            consultaSQL.Append("VALUES (@NombreParametro, @DescripcionParametro, @IdMarcaParametro, @PrecioUnitarioParametro, @ActivoParametro, @UrlImangeParametro) ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                var filasAfectadas = connection.Execute(consultaSQL.ToString(),
                    new
                    {
                        NombreParametro = obj.Nombre,
                        DescripcionParametro = obj.Descripcion,
                        IdMarcaParametro = obj.IdMarca,
                        PrecioUnitarioParametro = obj.PrecioUnitario,
                        ActivoParametro = obj.Activo,
                        UrlImangeParametro = obj.UrlImange,

                    });


            }



        }

        public int Editar(Entidades.Productos obj)
        {
            int filasAfectadas = 0;

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("UPDATE Productos ");
            consultaSQL.Append("SET Nombre = @NombreParametro, Descripcion = @DescripcionParametro, ");
            consultaSQL.Append("IdMarca = @IdMarcaParametro, PrecioUnitario = @PrecioUnitarioParametro, ");
            consultaSQL.Append("Activo = @ActivoParametro, UrlImange = @UrlImangeParametro ");
            consultaSQL.Append("WHERE Codigo = @CodigoParametro ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                filasAfectadas = connection.Execute(consultaSQL.ToString(),
                   new
                   {
                       CodigoParametro = obj.Codigo,
                       NombreParametro = obj.Nombre,
                       DescripcionParametro = obj.Descripcion,
                       IdMarcaParametro = obj.IdMarca,
                       PrecioUnitarioParametro = obj.PrecioUnitario,
                       ActivoParametro = obj.Activo,
                       UrlImangeParametro = obj.UrlImange
                   });


            }

            return filasAfectadas;
        }

        public List<Entidades.Join_Cards> ListarCards()
        {
            List<Entidades.Join_Cards> lista = new List<Entidades.Join_Cards>();

            StringBuilder consultaSQL = new StringBuilder();


            consultaSQL.Append("SELECT ");
            consultaSQL.Append("Productos.Codigo AS CODIGO, ");
            consultaSQL.Append("Productos.UrlImange AS UBICACION, "); 
            consultaSQL.Append("Productos.Nombre AS NOMBRE, ");
            consultaSQL.Append("Marcas.Nombre AS MARCA, ");
            consultaSQL.Append("Productos.Descripcion AS  DESCRIPCION, ");
            consultaSQL.Append("Productos.PrecioUnitario AS PRECIO_UNITARIO ");
            consultaSQL.Append("FROM Productos ");
            consultaSQL.Append("INNER JOIN Marcas ON ");
            consultaSQL.Append("Productos.IdMarca = Marcas.Id ");
            consultaSQL.Append("WHERE Productos.Activo = 1 "); 

            using (var connection = new SqlConnection(cadenaConexion))
            {
                lista = connection.Query<Entidades.Join_Cards>(consultaSQL.ToString()).ToList();
            }

            return lista;

        }

    }
}
