using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lautaro.PracticoMVC.Entidades
{
    public class Clientes
    {


        public int Codigo { get; set; }
        public string RazonSocial { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuario { get; set; }

    }
}
