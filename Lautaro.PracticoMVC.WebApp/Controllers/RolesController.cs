using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lautaro.PracticoMVC.WebApp.Controllers
{
    public class RolesController : Controller
    {

        public JsonResult Listar()
        {
            AccesoDatos.Roles obj = new AccesoDatos.Roles();
            var lista = obj.Listar();


            return Json(lista, JsonRequestBehavior.AllowGet);
        }


     

    }
}