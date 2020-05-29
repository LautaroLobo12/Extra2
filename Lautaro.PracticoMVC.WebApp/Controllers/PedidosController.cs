using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lautaro.PracticoMVC.WebApp.Controllers
{
    public class PedidosController : Controller
    {
        public ActionResult MisPedidos()
        {
            return View();
        }
        public JsonResult JsonMisPedidos()
        {
            int idCliente = Convert.ToInt32(Session["ID_CLIENTE"]);
            var metodos = new AccesoDatos.Pedidos();
            var lista = metodos.MisPedidos(idCliente);
            return (Json(lista, JsonRequestBehavior.AllowGet));
        }
        public ActionResult Carrito(int idPedido)
        {
            ViewBag.idPedido = idPedido;
            return View();
        }
        public JsonResult JsonObtenerDetallesPedido(int idPedido)
        {
            var metodos = new AccesoDatos.Pedidos();
            var lista = metodos.ListaDetallePedido(idPedido);
            return (Json(lista, JsonRequestBehavior.AllowGet));
        }
        public int Recalcular(Entidades.DetallesPedidos obj)
        {
            var metodos = new AccesoDatos.Pedidos();           
            int filasAfectadas = metodos.CalcularPrecioSegunCantidad(obj.ID_PEDIDO, obj.ITEM, obj.CANTIDAD);
            return filasAfectadas;
        }
        public int EliminarItemPedido(Entidades.DetallesPedidos obj)
        {
            var metodos = new AccesoDatos.Pedidos();
            int filasAfectadas = metodos.EliminarItemPedido(obj.ID_PEDIDO, obj.ITEM);
            return filasAfectadas;
        }
    }
}