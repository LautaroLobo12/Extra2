using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lautaro.PracticoMVC.Entidades
{
  public  class Sesion
    {
        public int ID_USUARIO { get; set; }

        public string USERNAME { get; set; }

        public string ID_ROL { get; set; }

        public string ROL_DESCRIPCION { get; set; }

        public int ID_CLIENTE { get; set; }

        public string NOMBRES { get; set; }

        public string APELLIDOS { get; set; }

        public bool ONLINE { get; set; }
    }
}
