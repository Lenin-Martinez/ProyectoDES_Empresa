using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDES_Empresa.Models
{
    public class Producto
    {
        public int ID { get; set; }

        [ForeignKey("Categoria")]
        public int? IdCategoria { get; set; }
        public Categoria Categoria { get; set; }

        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int UnidadesProducto { get; set; }
        public decimal CostoProducto { get; set; }


        //Propiedad de navegacion
        public ICollection<Compra> Compras { get; set; }
    }
}
