using System.ComponentModel.DataAnnotations;

namespace ProyectoDES_Empresa.Models
{
    public class Empleado
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
        [Display(Name = "Nombre de empleado")]
        public string NombreEmpleado { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El apellido debe tener entre 1 y 100 caracteres")]
        [Display(Name = "Apellido de empleado")]
        public string ApellidoEmpleado { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(0,5, ErrorMessage = "El porcentaje no puede ser menor a 0% ni superar el 5%")]
        [Display(Name = "Porcentaje de ganancia (%)")]
        public decimal ComisionVentaEmpleado { get; set; }
    }
}
