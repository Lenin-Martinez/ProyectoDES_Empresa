namespace ProyectoDES_Empresa.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }

        //Propiedad de navegacion
        public ICollection<Producto> Productos { get; set; }
    }
}
