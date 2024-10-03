using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDES_Empresa.Models
{
    public class Compra
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de compra")]
        public DateTime FechaCompra { get; set; }


        [ForeignKey("Proveedor")]
        [Display(Name = "Nombre de proveedor")]
        public int? IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }


        [ForeignKey("Producto")]
        [Display(Name = "Nombre de producto")]
        public int? IdProducto { get; set; }
        public Producto Producto { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Unidades compradas")]
        [Range(1, int.MaxValue, ErrorMessage = "Las unidades deben ser mayores a 0")]
        public int UnidadesCompra { get; set; }
    }
}
