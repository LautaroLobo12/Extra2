using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lautaro.PracticoMVC.WebApp.Controllers
{
    public class ProductosController : Controller
    {
        public ActionResult ABM()
        {
            return View();
        }
        public ActionResult Cards()
        {
            return View();
        }
        public ActionResult Cards2()
        {
            return View();
        }
        public JsonResult Listar()
        {
            AccesoDatos.Productos metodos = new AccesoDatos.Productos();
            var lista = metodos.Listar();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarCards()
        {
            AccesoDatos.Productos metodos = new AccesoDatos.Productos();
            var lista = metodos.ListarCards();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Detalle(int id)
        {
            AccesoDatos.Productos metodos = new AccesoDatos.Productos();

            Entidades.Productos obj = metodos.Detalle(id);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public int Guardar(Entidades.Productos obj)
        {
            int retorno = -1;
            if (obj.Codigo == 0)
            {
                AccesoDatos.Productos metodos = new AccesoDatos.Productos();
                metodos.Crear(obj);
                retorno = 1;
            }
            else 
            {
                AccesoDatos.Productos metodos = new AccesoDatos.Productos();
                int filasAfectadas = metodos.Editar(obj);
                if (filasAfectadas == 1)
                {
                    retorno = 2;
                }
            }
            return retorno;
        }


    }
}