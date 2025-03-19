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
    /// Repositorio para la gestión de calificaciones en la base de datos.
    /// </summary>
    public class RatingRepository
    {
        private readonly SmartGameDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de la base de datos.
        /// </summary>
        public RatingRepository(SmartGameDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las calificaciones registradas.
        /// </summary>
        public async Task<IEnumerable<Rating>> GetAll()
        {
            return await _context.Ratings.ToListAsync();
        }

        /// <summary>
        /// Obtiene una calificación por su ID.
        /// </summary>
        public async Task<Rating?> GetById(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        /// <summary>
        /// Agrega una nueva calificación a la base de datos.
        /// </summary>
        public async Task<Rating?> Create(Rating rating)
        {
            if (rating == null)
            {
                throw new ArgumentNullException(nameof(rating), "La calificación no puede ser nula.");
            }

            try
            {
                _context.Ratings.Add(rating);
                await _context.SaveChangesAsync();
                return rating;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la calificación: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Actualiza la información de una calificación existente.
        /// </summary>
        public async Task<bool> Update(Rating rating)
        {
            if (rating == null)
            {
                throw new ArgumentNullException(nameof(rating), "La calificación no puede ser nula.");
            }

            try
            {
                _context.Ratings.Update(rating);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la calificación: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina una calificación de la base de datos.
        /// </summary>
        public async Task<bool> Delete(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return false; // No se encontró la calificación
            }

            try
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la calificación: {ex.Message}");
                return false;
            }
        }
    }
}
