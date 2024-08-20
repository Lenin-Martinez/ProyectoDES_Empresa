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
        public int UnidadesVenta { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Costo del producto ($)")]
        public decimal PrecioUnitarioVenta { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Valor total ($)")]
        public decimal PrecioTotalVenta { get; set; }

        [ForeignKey("Empleado")]
        [Display(Name = "Nombre de empleado")]
        public int? IdEmpleado { get; set; }
        public Empleado Empleado { get; set; }

    }
}
