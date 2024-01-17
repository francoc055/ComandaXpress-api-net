namespace comandaXpress_api_net.Models.Dtos
{
    public class PedidoProductoPostDto
    {
        public int IdMesa { get; set; }
        public List<PedidoProducto> Productos { get; set; }
    }
}
