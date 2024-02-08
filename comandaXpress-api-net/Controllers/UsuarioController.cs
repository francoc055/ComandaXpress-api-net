using comandaXpress_api_net.db;
using comandaXpress_api_net.Models;
using comandaXpress_api_net.Models.Dtos;
using comandaXpress_api_net.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace comandaXpress_api_net.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        readonly IAccesoDatos _accesoDatos;
        readonly IAutorizacionService _autorizacionService;
        readonly IUsuarioService _usuarioService;

        public UsuarioController(IAccesoDatos _IaccesoDatos, IAutorizacionService iAutorizacionService, IUsuarioService usuarioService)
        {
            _accesoDatos = _IaccesoDatos;
            _autorizacionService = iAutorizacionService;
            _usuarioService = usuarioService;

        }

        [HttpPost("agregar")]
        public IActionResult AddUsuario([FromBody] dynamic data)
        {
            Usuario user = JsonConvert.DeserializeObject<Usuario>(data.ToString());

            user.FechaAlta = DateTime.Now;

            int filasAfectadas = _accesoDatos.Insert(@"INSERT INTO usuarios (nombre, clave, rol, fechaAlta)
                                                        VALUES (@Nombre, @Clave, @Rol, @FechaAlta)", user);

            if (user is null || filasAfectadas == 0)
                return BadRequest();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] dynamic data)
        {
            Usuario user = JsonConvert.DeserializeObject<Usuario>(data.ToString());
            Usuario userEncontrado = _usuarioService.ValidarUser(user);
            if (userEncontrado is null)
                return BadRequest();

            string token = _autorizacionService.GenerarToken(userEncontrado);

            return Ok(token);
        }

    }
}
