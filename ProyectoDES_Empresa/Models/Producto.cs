using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDES_Empresa.Models
{
    public class Producto
    {
        public int ID { get; set; }

        [ForeignKey("Categoria")]
        [Display(Name = "Nombre de categoria")]
        public int? IdCategoria { get; set; }
        public Categoria Categoria { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
        [Display(Name = "Nombre de producto")]
        public string NombreProducto { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(200, ErrorMessage = "La descripcion debe tener un maximo de 200 caracteres")]
        [Display(Name = "Descripcion del producto")]
        public string DescripcionProducto { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Unidades disponibles")]
        public int UnidadesProducto { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Costo del producto ($)")]
        public decimal CostoProducto { get; set; }


        //Propiedad de navegacion
        public ICollection<Compra> Compras { get; set; }
    }
}
