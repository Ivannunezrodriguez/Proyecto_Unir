using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartGameCatalog.API.Model;
using SmartGameCatalog.API.Data;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de usuarios en la base de datos.
    /// </summary>
    public class UserRepository
    {
        private readonly SmartGameDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de la base de datos.
        /// </summary>
        public UserRepository(SmartGameDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        public async Task<User?> Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
            }

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el usuario: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        public async Task<bool> Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
            }

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el usuario: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un usuario de la base de datos.
        /// </summary>
        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false; // No se encontró el usuario
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el usuario: {ex.Message}");
                return false;
            }
        }
    }
}
