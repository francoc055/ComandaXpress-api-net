﻿using comandaXpress_api_net.db;
using comandaXpress_api_net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
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

        [Authorize(Roles = "mozo")]
        [HttpGet("mesas")]
        public IActionResult GetMesas()
        {
            IEnumerable<Mesa> mesas = new List<Mesa>();

            mesas = _IAccesoDatos.GetAll<Mesa>("SELECT * FROM mesas WHERE mesas.activo = 1");
            return Ok(mesas);
        }

        [HttpGet("obtenerMesa/{id}")]
        public IActionResult GetMesaById(int id)
        {
            Mesa mesa = _IAccesoDatos.GetById<Mesa>("SELECT * FROM mesas WHERE mesas.id = @Id", new { Id = id });

            if (mesa is null)
                return BadRequest();

            return Ok(mesa);
        }

        [Authorize(Roles = "mozo")]
        [HttpPost("agregar")]
        public IActionResult AddMesa()
        {
            int filasAfectadas = _IAccesoDatos.Insert("INSERT INTO MESAS (estado) VALUES ('vacia')");

            if (filasAfectadas == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize(Roles = "mozo")]
        [HttpPut("actualizarEstado/{id}")]
        public IActionResult UpdateMesa(int id, [FromBody] string nuevoEstado)
        {
            
            int filasAfectadas = _IAccesoDatos.UpdateRemove("UPDATE mesas SET mesas.estado = @Estado WHERE mesas.id = @Id", new { Estado = nuevoEstado, Id = id});

            if (filasAfectadas == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }


        [Authorize(Roles = "mozo")]
        [HttpDelete("eliminarMesa/{id}")]
        public IActionResult RemoveMesa(int id)
        {
            int filasAfectadas = _IAccesoDatos.UpdateRemove("UPDATE mesas SET mesas.activo = 0 WHERE mesas.id = @Id", new { Id = id });

            if (filasAfectadas == 0)
            {
                return BadRequest();
            }

            return Ok();
        }


        [Authorize(Roles = "mozo")]
        [HttpGet("MesasVacias")]
        public IActionResult GetMesasVacias()
        {
            IEnumerable<Mesa> mesas = new List<Mesa>();

            mesas = _IAccesoDatos.GetAll<Mesa>("SELECT * FROM mesas WHERE mesas.activo = 1 AND mesas.estado = 'vacia'");

            if (mesas is null) return NotFound();
            
            return Ok(mesas);
        }
    }
}
