using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Dapper;

namespace Lautaro.PracticoMVC.AccesoDatos
{
   public class Pedidos
    {
        string cadenaConexion = Conexiones.ObtenerCadenaConexion();

        public List<Entidades.DetallesPedidos> ListaDetallePedido(int idPedido) {

            var lista = new List<Entidades.DetallesPedidos>();

            StringBuilder consultaSQL = new StringBuilder();
            consultaSQL.Append("SELECT ");
            consultaSQL.Append("DetallesPedidos.NumeroItem AS ITEM, ");
            consultaSQL.Append("Marcas.Nombre AS MARCA, ");
            consultaSQL.Append("Productos.Nombre AS PRODUCTO, ");
            consultaSQL.Append("DetallesPedidos.PrecioUnitario AS PRECIO_UNITARIO, ");
            consultaSQL.Append("DetallesPedidos.Cantidad AS CANTIDAD, ");
            consultaSQL.Append("(DetallesPedidos.PrecioUnitario * DetallesPedidos.Cantidad) AS SUBTOTAL ");
            consultaSQL.Append("FROM DetallesPedidos ");
            consultaSQL.Append("INNER JOIN Pedidos ON ");
            consultaSQL.Append("DetallesPedidos.NumeroPedido = Pedidos.NumeroPedido ");
            consultaSQL.Append("INNER JOIN Productos ON ");
            consultaSQL.Append("DetallesPedidos.CodigoProducto = Productos.Codigo ");
            consultaSQL.Append("INNER JOIN Marcas ON ");
            consultaSQL.Append("Productos.IdMarca = Marcas.Id  ");
            consultaSQL.Append("WHERE Pedidos.NumeroPedido = @idPedidoParametro ");
            consultaSQL.Append("ORDER BY DetallesPedidos.NumeroItem ASC ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                lista = connection.Query<Entidades.DetallesPedidos>(consultaSQL.ToString(),

                     new
                     {
                         idPedidoParametro = idPedido
                     
                     }).ToList();
            }

            return lista;

        }


        public int CalcularPrecioSegunCantidad(int idPedido, int nroItem, int cantidad)
        {
            int filasAfectadas = 0;

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("UPDATE DetallesPedidos ");
            consultaSQL.Append("SET Cantidad = @cantidadParametro ");
            consultaSQL.Append("WHERE NumeroPedido = @idPedidoParametro ");
            consultaSQL.Append("AND NumeroItem = @nroItemParametro ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                filasAfectadas = connection.Execute(consultaSQL.ToString(),
                   new
                   {
                       idPedidoParametro = idPedido,
                       nroItemParametro = nroItem,
                       cantidadParametro = cantidad
                   });


            }

            return filasAfectadas;
        }

        public int EliminarItemPedido(int idPedido, int nroItem)
        {
            int filasAfectadas = 0;

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("DELETE FROM DetallesPedidos ");
            consultaSQL.Append("WHERE NumeroPedido = @numeroPedidoParametro ");
            consultaSQL.Append("AND NumeroItem = @numeroItemParametro ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                filasAfectadas = connection.Execute(consultaSQL.ToString(),
                   new
                   {
                       numeroPedidoParametro = idPedido,
                       numeroItemParametro = nroItem
                   });


            }

            return filasAfectadas;
        }


        public List<Entidades.Pedidos> MisPedidos(int idCliente) {

            var lista = new List<Entidades.Pedidos>();

            StringBuilder consultaSQL = new StringBuilder();
            consultaSQL.Append("SELECT ");
            consultaSQL.Append("NumeroPedido AS ID_PEDIDO, ");
            consultaSQL.Append("Fecha AS FECHA_PEDIDO, ");
            consultaSQL.Append("Observacion AS OBSERVACIONES ");
            consultaSQL.Append("FROM Pedidos ");
            consultaSQL.Append("WHERE CodigoCliente = @idClienteParametro ");
            consultaSQL.Append("ORDER BY Fecha DESC ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                lista = connection.Query<Entidades.Pedidos>(consultaSQL.ToString(),

                     new
                     {
                         idClienteParametro = idCliente

                     }).ToList();
            }

            return lista;


        }



       



    }
}
