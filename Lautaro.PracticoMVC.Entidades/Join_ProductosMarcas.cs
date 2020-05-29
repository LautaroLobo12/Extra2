using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lautaro.PracticoMVC.Entidades
{
    public class Join_ProductosMarcas
    {
        public int CODIGO { get; set; }

        public string NOMBRE { get; set; }

        public string MARCA { get; set; }

        public decimal PRECIO { get; set; }
        
        public bool ACTIVO { get; set; }
    }
}
