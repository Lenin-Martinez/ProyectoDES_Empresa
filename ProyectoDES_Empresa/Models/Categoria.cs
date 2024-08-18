namespace ProyectoDES_Empresa.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        public string NombreCategoria { get; set; }
        public string? DescripcionCategoria { get; set; }

        //Propiedad de navegacion
        public ICollection<Producto> Productos { get; set; }
    }
}
