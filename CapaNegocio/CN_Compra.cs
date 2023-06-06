using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Compra
    {
        private CD_Compra objCapaDato = new CD_Compra();

        public List<Compra> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Compra obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            if ((obj.precioCompra == 0))
            {
                Mensaje = "Debe ingrear el precio del Compra";
            }
            else if (obj.oProducto.IdProducto == 0)
            {
                Mensaje = "Debe seleccionar un producto";
            }
            else if (obj.oUsuario.IdUsuario == 0)
            {
                Mensaje = "Debe seleccionar un usuario";
            }

            else if (obj.cantidad == 0)
            {

                Mensaje = "Debe ingresar la cantidad de Compra";
            }



            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Registrar(obj, out Mensaje);

            }
            else
            {

                return 0;
            }



        }
    }
}
