using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDES_Empresa.Models
{
    public class Venta
    {
        public int ID { get; set; }
        public DateTime FechaVenta { get; set; }

        [ForeignKey("Producto")]
        public int? IdProducto { get; set; }
        public Producto Producto { get; set; }

        public int Unidades { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }

        [ForeignKey("Empleado")]
        public int? IdEmpleado { get; set; }
        public Empleado Empleado { get; set; }

    }
}
