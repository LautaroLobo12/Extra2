using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lautaro.PracticoMVC.Entidades
{
   public class Join_UsuariosRoles
    {
        public int ID { get; set; }

        public string USUARIO { get; set; }

        public string ROL { get; set; }

        public string NOMBRES { get; set; }

        public string APELLIDOS { get; set; }

        public DateTime FECHA_ALTA { get; set; }
        
        public string ESTADO { get; set; }

    }
}
