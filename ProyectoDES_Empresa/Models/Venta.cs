using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDES_Empresa.Models
{
    public class Venta
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de venta")]
        public DateTime FechaVenta { get; set; }


        [ForeignKey("Producto")]
        [Display(Name = "Nombre de producto")]
        public int? IdProducto { get; set; }
        public Producto Producto { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Unidades vendidas")]
        [Range(1, int.MaxValue, ErrorMessage = "Las unidades vendidas deben ser mayores a 0")]
        public int UnidadesVenta { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Precio venta unitario ($)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El costo unitario es invalido")]
        public decimal PrecioUnitarioVenta { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Valor venta total ($)")]
        public decimal PrecioTotalVenta { get; set; }

        [ForeignKey("Empleado")]
        [Display(Name = "Nombre de empleado")]
        public int? IdEmpleado { get; set; }
        public Empleado Empleado { get; set; }

    }
}
