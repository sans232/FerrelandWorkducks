using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Compra
    {
        public int idCompra { get; set; }
        public decimal precioCompra { get; set; }
        public string precioCompraTexto { get; set; }
        public Producto oProducto { get; set; }
        public Usuario oUsuario { get; set; }
        public int cantidad { get; set; }
        public bool Activo { get; set; }
        //public string Base64 { get; set; }
    }
}
