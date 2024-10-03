using System.ComponentModel.DataAnnotations;

namespace ProyectoDES_Empresa.ViewModels
{
    public class UsuarioVM
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El formato del correo electrónico no es válido.")]
        [Display(Name = "Correo de usuario")]
        public string CorreoUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string ClaveUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string ConfirmarClaveUsuario { get; set; }
    }
}
