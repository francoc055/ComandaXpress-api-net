using comandaXpress_api_net.db;
using comandaXpress_api_net.Services.IService;

namespace comandaXpress_api_net.Services
{
    public class PedidoService : IPedidoService
    {
        readonly IAccesoDatos _AccesoDatos;

        public PedidoService(IAccesoDatos accesoDatos)
        {
            _AccesoDatos = accesoDatos;
        }

        public string GenerarCodigoCliente()
        {
            const string letras = "abcdefghijklmnopqrstuvwxyz";
            const string numeros = "0123456789";

            Random random = new Random();

            char[] codigo = new char[5];
            codigo[0] = letras[random.Next(letras.Length)];
            codigo[1] = numeros[random.Next(numeros.Length)];
            codigo[2] = letras[random.Next(letras.Length)];
            codigo[3] = numeros[random.Next(numeros.Length)];
            codigo[4] = letras[random.Next(letras.Length)];

            return new String(codigo);
        }

        public void AltaPedido()
        {

        }
    }
}
