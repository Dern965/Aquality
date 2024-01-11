namespace Aquality_api.Model
{
    public class ProductoModel
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion { get; set; }
        public string precio { get; set; }
        public string fabricacion { get; set;}
        public string disponibilidad { get; set;}
        public string idTienda { get; set;}
    }
}
