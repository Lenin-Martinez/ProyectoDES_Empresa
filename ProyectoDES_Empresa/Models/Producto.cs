using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDES_Empresa.Models
{
    public class Producto
    {
        public int ID { get; set; }

        [ForeignKey("Categoria")]
        public int? IdCategoria { get; set; }
        public Categoria Categoria { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Unidades { get; set; }
        public decimal Costo { get; set; }


        //Propiedad de navegacion
        public ICollection<Compra> Compras { get; set; }
    }
}
