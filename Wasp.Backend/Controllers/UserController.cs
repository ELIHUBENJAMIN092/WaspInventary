using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasp.Backend.Data;
using Wasp.Shared.Entities;

namespace Wasp.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _datacontext;

        public UserController(DataContext context)
        {
            _datacontext = context;
        }

        // Método HttpPost: Agregar un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            _datacontext.Add(user);
            await _datacontext.SaveChangesAsync();
            return Ok();
        }

        // Método HttpGet: Obtener todos los usuarios
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _datacontext.users.ToListAsync();
            return Ok(users);
        }

        // Método HttpGet: Obtener un usuario por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _datacontext.users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con ID = {id}");
            }

            return Ok(user);
        }

        // Método HttpPatch: Actualizar parcialmente un usuario por ID
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existingUser = await _datacontext.users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound($"No se encontró el usuario con ID = {id}");
            }

            // Actualizar solo los campos necesarios
            if (!string.IsNullOrEmpty(updatedUser.Name))
                existingUser.Name = updatedUser.Name;
            if (!string.IsNullOrEmpty(updatedUser.Email))
                existingUser.Email = updatedUser.Email;
            if (!string.IsNullOrEmpty(updatedUser.Password))
                existingUser.Password = updatedUser.Password;

            await _datacontext.SaveChangesAsync();
            return Ok(existingUser);
        }

        // Método HttpDelete: Eliminar un usuario por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _datacontext.users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con ID = {id}");
            }

            _datacontext.users.Remove(user);
            await _datacontext.SaveChangesAsync();
            return Ok($"Usuario con ID = {id} eliminado correctamente");
        }
    }
}