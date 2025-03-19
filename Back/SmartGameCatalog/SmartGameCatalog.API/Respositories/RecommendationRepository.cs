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
    /// Repositorio para la gestión de recomendaciones en la base de datos.
    /// </summary>
    public class RecommendationRepository
    {
        private readonly SmartGameDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de la base de datos.
        /// </summary>
        public RecommendationRepository(SmartGameDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las recomendaciones registradas.
        /// </summary>
        public async Task<IEnumerable<Recommendation>> GetAll()
        {
            return await _context.Recommendations.ToListAsync();
        }

        /// <summary>
        /// Obtiene una recomendación por su ID.
        /// </summary>
        public async Task<Recommendation?> GetById(int id)
        {
            return await _context.Recommendations.FindAsync(id);
        }

        /// <summary>
        /// Agrega una nueva recomendación a la base de datos.
        /// </summary>
        public async Task<Recommendation?> Create(Recommendation recommendation)
        {
            if (recommendation == null)
            {
                throw new ArgumentNullException(nameof(recommendation), "La recomendación no puede ser nula.");
            }

            try
            {
                _context.Recommendations.Add(recommendation);
                await _context.SaveChangesAsync();
                return recommendation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la recomendación: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Actualiza la información de una recomendación existente.
        /// </summary>
        public async Task<bool> Update(Recommendation recommendation)
        {
            if (recommendation == null)
            {
                throw new ArgumentNullException(nameof(recommendation), "La recomendación no puede ser nula.");
            }

            try
            {
                _context.Recommendations.Update(recommendation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la recomendación: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina una recomendación de la base de datos.
        /// </summary>
        public async Task<bool> Delete(int id)
        {
            var recommendation = await _context.Recommendations.FindAsync(id);
            if (recommendation == null)
            {
                return false; // No se encontró la recomendación
            }

            try
            {
                _context.Recommendations.Remove(recommendation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la recomendación: {ex.Message}");
                return false;
            }
        }
    }
}
