using AutoMapper;
using comandaXpress_api_net.db;
using comandaXpress_api_net.Models;
using comandaXpress_api_net.Models.Dto;
using comandaXpress_api_net.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace comandaXpress_api_net.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PedidoController : Controller
    {
        readonly IAccesoDatos _AccesoDatos;
        readonly IPedidoService _PedidoService;
        readonly IMapper _mapper;

        public PedidoController(IAccesoDatos accesoDatos, IPedidoService pedidoService, IMapper mapper)
        {
            _AccesoDatos = accesoDatos;
            _PedidoService = pedidoService;
            _mapper = mapper;
        }

        //[HttpGet("pedidos")]
        //public IActionResult GetProductos()
        //{
        //    IEnumerable<Pedido> pedidos = new List<Pedido>();

        //    pedidos = _AccesoDatos.QueryGetAll<Pedido>("SELECT * FROM pedidos WHERE pedidos.id = 1");
        //    return Json(pedidos);
        //}


        //[HttpPost("agregar")] 
        //public IActionResult AddProducto([FromBody] dynamic data)
        //{

        //    PedidoProducto modelo = JsonConvert.DeserializeObject<PedidoProducto>(data.ToString());

        //    return Ok(modelo);
        //}



        //[HttpDelete("eliminarPedido/{id}")]
        //public IActionResult RemoveMesa(int id)
        //{
        //    int filasAfectadas = _AccesoDatos.Query("UPDATE pedidos SET pedidos.activo = 0 WHERE pedidos.id = @Id", new { Id = id });

        //    if (filasAfectadas == 0)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok();
        //}



    }
}
