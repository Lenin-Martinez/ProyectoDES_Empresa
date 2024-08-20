using System.ComponentModel.DataAnnotations;

namespace ProyectoDES_Empresa.Models
{
    public class Proveedor
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
        [Display(Name = "Nombre de proveedor")]
        public string NombreProveedor { get; set; }
    }
}
