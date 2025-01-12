using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasp.Shared.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        private string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; }

        public Rol? Rol { get; set; }

        [Required]
        public int RolId { get; set; }

        // Constructor para inicializar la propiedad Password
        public User(string password)
        {
            Password = password;
        }
    }
}