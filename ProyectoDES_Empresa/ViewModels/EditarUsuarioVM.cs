using System.ComponentModel.DataAnnotations;

namespace ProyectoDES_Empresa.ViewModels
{
    public class EditarUsuarioVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El formato del correo electrónico no es válido.")]
        [Display(Name = "Correo de usuario")]
        public string CorreoUsuario { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        public string ClaveAntigua { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        public string NuevaClaveUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string ConfirmarNuevaClaveUsuario { get; set; }
    }

}
