using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasp.Backend.Data;
using Wasp.Shared.Entities;

namespace Wasp.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _datacontext;

        public RolesController(DataContext context)
        {
            _datacontext = context;
        }

        // Método HttpPost: Agregar un nuevo rol
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] Rol roles)
        {
            _datacontext.Add(roles);
            await _datacontext.SaveChangesAsync();
            return Ok();
        }

        // Método HttpGet: Obtener todos los roles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _datacontext.roles.ToListAsync();
            return Ok(roles);
        }

        // Método HttpPatch: Actualizar parcialmente un rol por ID
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Rol updatedRole)
        {
            var existingRole = await _datacontext.roles.FindAsync(id);
            if (existingRole == null)
            {
                return NotFound($"No se encontró el rol con ID = {id}");
            }

            // Actualizar solo los campos necesarios
            if (!string.IsNullOrEmpty(updatedRole.Name))
                existingRole.Name = updatedRole.Name;

            await _datacontext.SaveChangesAsync();
            return Ok(existingRole);
        }

        // Método HttpDelete: Eliminar un rol por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _datacontext.roles.FindAsync(id);
            if (role == null)
            {
                return NotFound($"No se encontró el rol con ID = {id}");
            }

            _datacontext.roles.Remove(role);
            await _datacontext.SaveChangesAsync();
            return Ok($"Rol con ID = {id} eliminado correctamente");
        }
    }
}