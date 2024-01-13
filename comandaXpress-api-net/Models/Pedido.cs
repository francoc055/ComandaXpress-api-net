namespace comandaXpress_api_net.Models
{

    public class Pedido
    {
        public int Id { get; set; }
        public int IdMesa { get; set; }
        public string CodigoCliente { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Estado { get; set; }
    }
}
