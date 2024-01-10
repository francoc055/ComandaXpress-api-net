using comandaXpress_api_net.db;
using comandaXpress_api_net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Security.Policy;

namespace comandaXpress_api_net.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MesaController : Controller
    {
        private IAccesoDatos _IAccesoDatos;

        public MesaController(IAccesoDatos _IaccesoDatos)
        {

            _IAccesoDatos = _IaccesoDatos;

        }

        [HttpGet]
        [Route("mesas")]
        public IActionResult GetMesas()
        {
            IEnumerable<Mesa> mesas = new List<Mesa>();

            mesas = _IAccesoDatos.QueryGetAll<Mesa>("SELECT * FROM mesas");
            return Json(mesas);
        }

        [HttpGet("obtenerMesa/{id}")]
        public IActionResult GetMesaById(int id)
        {
            Mesa mesa = _IAccesoDatos.QueryGetById<Mesa>("SELECT * FROM mesas WHERE mesas.id = @Id", new { Id = id });

            if (mesa is null)
                return BadRequest();

            return Ok(mesa);
        }


        [HttpPost]
        [Route("agregar")]
        public IActionResult AddMesa()
        {
            int filasAfectadas = _IAccesoDatos.Query("INSERT INTO MESAS (estado) VALUES ('vacia')");

            if (filasAfectadas == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("actualizarEstado/{id}")]
        public IActionResult UpdateMesa(int id, [FromBody] string nuevoEstado)
        {
            
            int filasAfectadas = _IAccesoDatos.Query("UPDATE mesas SET mesas.estado = @Estado WHERE mesas.id = @Id", new { Estado = nuevoEstado, Id = id});

            if (filasAfectadas == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("eliminarMesa/{id}")]
        public IActionResult RemoveMesa(int id)
        {
            int filasAfectadas = _IAccesoDatos.Query("DELETE FROM mesas WHERE mesas.id = @Id", new { Id = id });

            if (filasAfectadas == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
