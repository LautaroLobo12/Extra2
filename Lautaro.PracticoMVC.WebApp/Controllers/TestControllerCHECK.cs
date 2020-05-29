using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Benjamin.PracticoMVC.WebApp.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public int VerPOST(Entidades.Clientes objCliente)
        {
            int r = 777;
            return r;
        }
        public int Totem(string parametro1)
        {
            return 777;
        }
    }
}