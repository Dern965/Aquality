namespace Aquality_api.Model
{
    public class OrdenModel
    {
        public string idOrden { get; set; }
        public string idCarrito { get; set; }

        //public CarritoModel idCarrito { get; set; }
        public string fechaEntrega { get; set; }
        public string seguimiento { get; set; }
    }
}
