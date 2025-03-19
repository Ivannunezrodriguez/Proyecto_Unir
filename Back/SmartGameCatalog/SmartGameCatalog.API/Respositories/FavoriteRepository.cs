using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Model;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de los juegos favoritos en la base de datos.
    /// </summary>
    public class FavoriteRepository
    {
        private readonly SmartGameDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de la base de datos.
        /// </summary>
        public FavoriteRepository(SmartGameDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los juegos favoritos de todos los usuarios.
        /// </summary>
        public async Task<IEnumerable<Favorite>> GetAll()
        {
            return await _context.Favorites.ToListAsync();
        }

        /// <summary>
        /// Obtiene un registro de favorito por su ID.
        /// </summary>
        public async Task<Favorite?> GetById(int id)
        {
            return await _context.Favorites.FindAsync(id);
        }

        /// <summary>
        /// Obtiene todos los juegos favoritos de un usuario específico.
        /// </summary>
        public async Task<IEnumerable<Favorite>> GetByUserId(int userId)
        {
            return await _context.Favorites.Where(f => f.UserId == userId).ToListAsync();
        }

        /// <summary>
        /// Agrega un juego a favoritos.
        /// </summary>
        public async Task<Favorite?> Create(Favorite favorite)
        {
            if (favorite == null)
            {
                throw new ArgumentNullException(nameof(favorite), "El favorito no puede ser nulo.");
            }

            try
            {
                _context.Favorites.Add(favorite);
                await _context.SaveChangesAsync();
                return favorite;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar favorito: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Actualiza un registro de favorito.
        /// </summary>
        public async Task<bool> Update(Favorite favorite)
        {
            if (favorite == null)
            {
                throw new ArgumentNullException(nameof(favorite), "El favorito no puede ser nulo.");
            }

            try
            {
                _context.Favorites.Update(favorite);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar favorito: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un juego de la lista de favoritos de un usuario.
        /// </summary>
        public async Task<bool> Delete(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return false; // No se encontró el favorito
            }

            try
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar favorito: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un juego de la lista de favoritos de un usuario específico.
        /// </summary>
        public async Task<bool> DeleteByUserAndGame(int userId, int gameId)
        {
            var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.GameId == gameId);
            if (favorite == null)
            {
                return false; // No se encontró el favorito
            }

            try
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar favorito: {ex.Message}");
                return false;
            }
        }
    }
}
