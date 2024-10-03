using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDES_Empresa.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El formato del correo electrónico no es válido.")]
        [Display(Name ="Correo de usuario")]
        public string CorreoUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string ClaveUsuario { get; set; }

        [ForeignKey("Rol")]
        public int? IdRol { get; set; }
        public Rol Rol { get; set; }
    }
}
