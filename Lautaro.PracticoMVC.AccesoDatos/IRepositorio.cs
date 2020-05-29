using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lautaro.PracticoMVC.AccesoDatos
{
    interface IRepositorio<Entidad> where Entidad : class
    {
        //Lista generica de cada entidad
        List<Entidad> Listar();

        //Detalles del Objeto
        Entidad Detalle(object id);

        //Crear Objeto - Insertar en tabla
        void Crear(Entidad objEntidad);

        //Editar Objeto - Actualizar tabla
        void Editar(Entidad objEntidad);

        //Eliminar Objeto - Eliminar Registro Tabla
        void Eliminar(object id);

        //Deshabilitar Objeto - Dejar en estado Inactivo
        void Deshabilitar(object id);

        //Eliminacion confirmada
        bool ConfirmarEliminacion(object id);

        //Guardar cambios - confirmar cambios
        void Guardar();

        // Dispose()
        void Desechar();


    }
}
