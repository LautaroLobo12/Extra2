using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lautaro.PracticoMVC.WebApp.Controllers
{
    public class UsuariosController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }
        public int CodigoLogin(Entidades.Login obj)
        {
            bool usuarioClaveIguales;

            if (obj.USUARIO == obj.CLAVE)
            {
                usuarioClaveIguales = true;
            }
            else
            {
                usuarioClaveIguales = false;
            }
            AccesoDatos.Usuarios metodos = new AccesoDatos.Usuarios();
            if (metodos.ValidarLogin(obj.USUARIO, obj.CLAVE) == true)
            {
                Entidades.Sesion objSesion = metodos.ObtenerUsuarioSesion(obj.USUARIO);
                objSesion.ONLINE = true;
                Session["ID_USUARIO"] = objSesion.ID_USUARIO;
                Session["USERNAME"] = objSesion.USERNAME;
                Session["ID_ROL"] = objSesion.ID_ROL;
                Session["ROL_DESCRIPCION"] = objSesion.ROL_DESCRIPCION;
                Session["ID_CLIENTE"] = objSesion.ID_CLIENTE;
                Session["NOMBRES"] = objSesion.NOMBRES;
                Session["APELLIDOS"] = objSesion.APELLIDOS;
                Session["ONLINE"] = objSesion.ONLINE;
                if (usuarioClaveIguales == true)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else
                return 0;
            }

        }
        public int ValidarClaveActual(string claveActual)
        {
            Entidades.Login obj = new Entidades.Login();
            obj.USUARIO = Session["USUARIO"].ToString();
            obj.CLAVE = claveActual;
            AccesoDatos.Usuarios metodos = new AccesoDatos.Usuarios();
            int codigo = metodos.ValidarClaveActual(obj.USUARIO, obj.CLAVE);
            return codigo;
        }
        public int CodigoCambiarClave(Entidades.Login obj)
        {
            obj.USUARIO = Session["USUARIO"].ToString();
            AccesoDatos.Usuarios metodos = new AccesoDatos.Usuarios();
            int codigo = metodos.ActualizarPassword(obj.USUARIO, obj.CLAVE);
            return codigo;
        }
        public int CodigoResetearClave(int idUsuario)
        {
            AccesoDatos.Usuarios metodos = new AccesoDatos.Usuarios();
            int codigoResetClave = metodos.ResetearClave(idUsuario);
            return codigoResetClave;
        }
        public JsonResult Listar()
        {
            AccesoDatos.Usuarios obj = new AccesoDatos.Usuarios();
            var lista = obj.Listar();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Detalle(int id)
        {
            AccesoDatos.Usuarios metodos = new AccesoDatos.Usuarios();
            var userSeleccionado = metodos.Detalle(id);
            return Json(userSeleccionado, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ABM()
        {
            return View();
        }
        public int Guardar3(Entidades.Usuarios obj)
        {
            int retorno = -1;
            if (obj.Id == 0)
            {
                AccesoDatos.Usuarios metodos = new AccesoDatos.Usuarios();
                retorno = 1;
            }
            else 
            {
                AccesoDatos.Usuarios metodos = new AccesoDatos.Usuarios();
            }
            return retorno;
        }
        public int Guardar(Entidades.Join_UsuariosClientes obj_Usuario_Cliente)
        {
            int retorno = -1;
            if (obj_Usuario_Cliente.ID_USUARIO == 0)
            {
                AccesoDatos.Usuarios metodos = new AccesoDatos.Usuarios();
                retorno = metodos.Crear(obj_Usuario_Cliente);
            }
            else 
            {
                AccesoDatos.Usuarios metodos = new AccesoDatos.Usuarios();
                int filasAfectadas = metodos.Editar(obj_Usuario_Cliente);
                if (filasAfectadas == 1)
                {
                    retorno = 2;
                }
            }
            return retorno;
        }
    }
}