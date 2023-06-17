using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objCapaDato = new CD_Reporte();

        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion) {
            return objCapaDato.Ventas(fechainicio,fechafin,idtransaccion);
        }


        public DashBoard VerDashBoard()
        {
            return objCapaDato.VerDashBoard();
        }
        public List<Top5Productos> top5Productos()
        {
            List<Top5Productos> topProductos = objCapaDato.ObtenerTopProductos();
            return topProductos;
        }

    }
}
