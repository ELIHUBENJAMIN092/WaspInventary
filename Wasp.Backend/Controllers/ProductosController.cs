using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasp.Backend.Data;
using Wasp.Shared.Entities;

namespace Wasp.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductosController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crearProducto")]
        public async Task<IActionResult> CreateProduct(Productos productos)
        {
            //guardar el producto en la base de datos
            await _context.productos.AddAsync(productos);
            await _context.SaveChangesAsync();

            //devolver un mensaje de exito
            return Ok();
        }

        [HttpGet]
        [Route("listaProducto")]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProducts()
        {
            //Obten la lista de productos de la base de datos
            var productos = await _context.productos.ToListAsync();

            //devuelve una lista de productos
            return Ok(productos);
        }

        [HttpGet]
        [Route("verProducto")]
        public async Task<IActionResult> GetProduct(int id)
        {
            //obtener el producto de la base de datos
            Productos producto = await _context.productos.FindAsync(id);

            //devolver el producto
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPut]
        [Route("editarProducto")]
        public async Task<IActionResult> UpdateProduct(int id, Productos producto)
        {
            //Actualizar el producto en la base de datos
            var productoExistente = await _context.productos.FindAsync(id);
            productoExistente!.Nombre = producto.Nombre;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Precio = producto.Precio;

            await _context.SaveChangesAsync();

            //devolver un mensaje de exito
            return Ok();
        }

        [HttpDelete]
        [Route("eliminarProducto")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //Eliminar producto de la base de datos
            var productoBorrado = await _context.productos.FindAsync(id);
            _context.productos.Remove(productoBorrado!);

            await _context.SaveChangesAsync();

            //Devolver un mensaje de exito
            return Ok();
        }
    }
}