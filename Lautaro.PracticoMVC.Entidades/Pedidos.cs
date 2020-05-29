using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lautaro.PracticoMVC.Entidades
{
    public class Pedidos
    {
        public int ID_PEDIDO { get; set; }

        public int ID_CLIENTE { get; set; }

        public DateTime FECHA_PEDIDO { get; set; }

        public string OBSERVACIONES { get; set; }
    }
}
