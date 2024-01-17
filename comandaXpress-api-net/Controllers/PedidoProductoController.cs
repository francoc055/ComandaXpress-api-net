using AutoMapper;
using comandaXpress_api_net.db;
using comandaXpress_api_net.Models;
using comandaXpress_api_net.Models.Dtos;
using comandaXpress_api_net.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;

namespace comandaXpress_api_net.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PedidoProductoController : Controller
    {
        readonly IAccesoDatos _AccesoDatos;
        readonly IPedidoService _PedidoService;

        public PedidoProductoController(IAccesoDatos accesoDatos, IPedidoService pedidoService)
        {
            _AccesoDatos = accesoDatos;
            _PedidoService = pedidoService;
        }

        /*
        {
          "idMesa": 2,
          "productos":[
              {  
              "IdProducto": 2,
              "Cantidad": 2 
              },
              {
              "IdProducto": 4,
              "Cantidad": 1
              }
            ]
        }
        */
        [HttpPost("agregar")]
        public IActionResult AddPedidoController([FromBody] dynamic data)
        {

            try
            {

                PedidoProductoPostDto pedidoProducto = JsonConvert.DeserializeObject<PedidoProductoPostDto>(data.ToString());

                int idMesa = pedidoProducto.IdMesa;
                string codigoCliente = _PedidoService.GenerarCodigoCliente();
                DateTime fechaAlta = DateTime.Now;

                if (pedidoProducto.Productos.Count() <= 0 || idMesa <= 0)
                    return BadRequest();

                int idPedido = _AccesoDatos.Insert("INSERT INTO pedidos(idMesa, codigoCliente, fechaAlta) OUTPUT INSERTED.id VALUES (@IdMesa, @CodigoCliente, @FechaAlta)", new { IdMesa = idMesa, CodigoCliente = codigoCliente, FechaAlta = fechaAlta});

      

                Console.WriteLine(idPedido);
                _AccesoDatos.MultipleInsert(pedidoProducto.Productos, idPedido);



                return Ok(pedidoProducto);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al procesar la solicitud: " + ex.Message);
            }
        }

        /*
        {
        "Id": 1
        "Estado": "preparacion",
        "TiempoPreparacion": 15
        } 
        */
        [HttpPut("ActualizarAPreparacion")]
        public IActionResult UpdatePreparacion([FromBody] dynamic data)
        {
            PedidoProducto pedidoProducto = JsonConvert.DeserializeObject<PedidoProducto>(data.ToString());

            if (pedidoProducto is null)
                return BadRequest();

            pedidoProducto.FechaPreparacion = DateTime.Now;

            int filasAfectadas = _AccesoDatos.UpdateRemove(@"UPDATE pedidos_productos SET 
                                                            pedidos_productos.estado = @Estado,
                                                            pedidos_productos.tiempoPreparacion = @TiempoPreparacion,
                                                            pedidos_productos.fechaPreparacion = @FechaPreparacion
                                                            WHERE pedidos_productos.id = @Id", pedidoProducto);

            if(filasAfectadas == 0) return BadRequest();


            return NoContent();

        }

        /*
        {
        "Id": 1
        "Estado": "servir"
        }
        */
        [HttpPut("ActualizarAServir")]
        public IActionResult UpdateServir([FromBody] dynamic data)
        {
            PedidoProducto pedidoProducto = JsonConvert.DeserializeObject<PedidoProducto>(data.ToString());

            if (pedidoProducto is null) return BadRequest();

            int filasAfectadas = _AccesoDatos.UpdateRemove(@"UPDATE pedidos_productos SET 
                                                            pedidos_productos.estado = @Estado
                                                            WHERE pedidos_productos.id = @Id", pedidoProducto);

            if (filasAfectadas == 0) return BadRequest();


            return NoContent();
        }




    }
}
