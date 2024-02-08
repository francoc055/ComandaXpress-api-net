using comandaXpress_api_net.Models;

namespace comandaXpress_api_net.Services.IService
{
    public interface IAutorizacionService
    {

        public string GenerarToken(Usuario usuario);

    }
}
