using comandaXpress_api_net.db;
using comandaXpress_api_net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comandaXpress_api_net.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private IAccesoDatos _IAccesoDatos;

        public ProductoController(IAccesoDatos _IaccesoDatos)
        {

            _IAccesoDatos = _IaccesoDatos;

        }

        [HttpGet("productos")]
        public IActionResult GetProductos()
        {
            IEnumerable<Producto> productos = new List<Producto>();

            productos = _IAccesoDatos.QueryGetAll<Producto>("SELECT * FROM productos");
            return Json(productos);
        }

        [HttpGet("obtenerProducto/{id}")]
        public IActionResult GetProductoById(int id)
        {
            Producto producto = _IAccesoDatos.QueryGetById<Producto>("SELECT * FROM productos WHERE productos.id = @Id", new { Id = id });

            if (producto is null)
                return BadRequest();

            return Ok(producto);
        }

        [HttpPost("agregar")]
        public IActionResult AddProducto([FromBody] Producto producto)
        {
            int filasAfectadas = _IAccesoDatos.Query("INSERT INTO productos (nombre, sector, precio) VALUES (@Nombre, @Sector, @Precio)", producto);

            if (filasAfectadas == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("actualizarProducto/{id}")]
        public IActionResult UpdateMesa(int id, [FromBody] Producto producto)
        {

            int filasAfectadas = _IAccesoDatos.Query("UPDATE productos SET productos.nombre = @Nombre, productos.precio = @Precio WHERE productos.id = @Id", producto);

            if (filasAfectadas == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("eliminarProducto/{id}")]
        public IActionResult RemoveMesa(int id)
        {
            int filasAfectadas = _IAccesoDatos.Query("DELETE FROM productos WHERE productos.id = @Id", new { Id = id });

            if (filasAfectadas == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
