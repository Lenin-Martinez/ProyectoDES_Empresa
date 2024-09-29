using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDES_Empresa.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string CorreoUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string ClaveUsuario { get; set; }

        [ForeignKey("Rol")]
        public int? IdRol { get; set; }
        public Rol Rol { get; set; }
    }
}
