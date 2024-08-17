namespace ProyectoDES_Empresa.Models
{
    public class Empleado
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal ComisionVenta { get; set; }

        //Propiedad de navegacion
        public ICollection<Venta> Ventas { get; set; }
    }
}
