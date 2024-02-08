using comandaXpress_api_net.db;
using comandaXpress_api_net.Models;
using comandaXpress_api_net.Services.IService;

namespace comandaXpress_api_net.Services
{
    public class UsuarioService : IUsuarioService
    {
        readonly IAccesoDatos _accesoDatos;
        public UsuarioService(IAccesoDatos accesoDatos)
        {
            _accesoDatos = accesoDatos;
        }

        public Usuario ValidarUser(Usuario user)
        {
            Usuario usuarioEncontrado = _accesoDatos.GetById<Usuario>(@"SELECT * FROM usuarios 
                                                            WHERE usuarios.nombre = @Nombre AND usuarios.clave = @Clave", user);

            if (usuarioEncontrado is null)
                return null;

            return usuarioEncontrado;
        }
    }
}
