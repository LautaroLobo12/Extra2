using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lautaro.PracticoMVC.Entidades
{
    public class DetallesPedidos
    {
        public int ID_PEDIDO { get; set; }


        public int ITEM { get; set; }

        public int ID_PRODUCTO { get; set; }

        public string MARCA { get; set; }

        public string PRODUCTO { get; set; }

        public decimal PRECIO_UNITARIO { get; set; }

        public int CANTIDAD { get; set; }

        public decimal SUBTOTAL { get; set; }

    }
}
