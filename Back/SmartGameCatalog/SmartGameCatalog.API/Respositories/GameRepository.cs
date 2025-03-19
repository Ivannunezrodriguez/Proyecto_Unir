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
    /// Repositorio para la gestión de los juegos en la base de datos.
    /// </summary>
    public class GameRepository
    {
        private readonly SmartGameDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de la base de datos.
        /// </summary>
        public GameRepository(SmartGameDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los juegos registrados.
        /// </summary>
        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _context.Games.ToListAsync();
        }

        /// <summary>
        /// Obtiene un juego por su ID.
        /// </summary>
        public async Task<Game?> GetById(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        /// <summary>
        /// Agrega un nuevo juego a la base de datos.
        /// </summary>
        public async Task<Game?> Create(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "El juego no puede ser nulo.");
            }

            try
            {
                _context.Games.Add(game);
                await _context.SaveChangesAsync();
                return game;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el juego: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Actualiza la información de un juego existente.
        /// </summary>
        public async Task<bool> Update(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "El juego no puede ser nulo.");
            }

            try
            {
                _context.Games.Update(game);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el juego: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un juego de la base de datos.
        /// </summary>
        public async Task<bool> Delete(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return false; // No se encontró el juego
            }

            try
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el juego: {ex.Message}");
                return false;
            }
        }
    }
}
