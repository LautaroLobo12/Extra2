using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lautaro.PracticoMVC.Entidades
{
    public class Join_UsuariosClientes
    {
        public int ID_USUARIO { get; set; }

        public string ID_ROL { get; set; }

        public string USERNAME { get; set; }

        public string NOMBRES { get; set; }

        public string APELLIDOS { get; set; }

        public string RAZON_SOCIAL { get; set; }

        public DateTime FECHA_CREACION { get; set; }

        public bool ACTIVO { get; set; }
    }
}
