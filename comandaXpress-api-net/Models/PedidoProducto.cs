namespace comandaXpress_api_net.Models
{
    public class PedidoProducto
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        public int TiempoPreparacion { get; set; }
        public DateTime FechaPreparacion { get; set; }


    }
}
