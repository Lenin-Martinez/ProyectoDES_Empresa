using System.ComponentModel.DataAnnotations;

namespace ProyectoDES_Empresa.Models
{
    public class Categoria
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
        [Display(Name = "Nombre de categoria")]
        public string NombreCategoria { get; set; }


        [StringLength(200, ErrorMessage = "La descripcion debe tener un maximo de 200 caracteres")]
        [Display(Name = "Descripcion de categoria")]
        public string? DescripcionCategoria { get; set; }
    }
}
