using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lautaro.PracticoMVC.WebApp.Controllers
{
    public class MarcasController : Controller
    {
        public JsonResult Listar()
        {
            AccesoDatos.Marcas metodos = new AccesoDatos.Marcas();
            var lista = metodos.Listar();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


    }
}