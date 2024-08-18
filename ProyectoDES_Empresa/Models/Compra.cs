using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDES_Empresa.Models
{
    public class Compra
    {
        public int ID { get; set; }
        public DateTime FechaCompra { get; set; }

        [ForeignKey("Proveedor")]
        public int? IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }


        [ForeignKey("Producto")]
        public int? IdProducto { get; set; }
        public Producto Producto { get; set; }


        public int UnidadesCompra { get; set; }
    }
}
