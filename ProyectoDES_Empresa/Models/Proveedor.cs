namespace ProyectoDES_Empresa.Models
{
    public class Proveedor
    {
        public int ID { get; set; }
        public string NombreProveedor { get; set; }

        //Propiedad de navegacion
        public ICollection<Compra> Compras { get; set; }
    }
}
