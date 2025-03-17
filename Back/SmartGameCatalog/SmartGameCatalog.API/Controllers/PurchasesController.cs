using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    // Define la ruta base de la API para este controlador como "api/Purchases".
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        // Repositorio que maneja la lógica de acceso a datos para las compras.
        private readonly PurchaseRepository _repository;

        // Constructor que inyecta el repositorio de compras.
        public PurchasesController(PurchaseRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todas las compras registradas.
        /// </summary>
        /// <returns>Una lista de objetos Purchase.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
        {
            var purchases = await _repository.GetPurchasesAsync();
            return Ok(purchases);
        }

        /// <summary>
        /// Registra una nueva compra en la base de datos.
        /// </summary>
        /// <param name="purchase">El objeto Purchase a registrar.</param>
        /// <returns>Un resultado con el estado de la operación.</returns>
        [HttpPost]
        public async Task<ActionResult> CreatePurchase(Purchase purchase)
        {
            // Genera un nuevo identificador único para la compra.
            purchase.Id_Purchase = Guid.NewGuid();
            // Agrega la compra a la base de datos.
            await _repository.AddPurchaseAsync(purchase);
            // Devuelve una respuesta 201 Created con la ubicación del nuevo recurso.
            return CreatedAtAction(nameof(GetPurchases), new { id = purchase.Id_Purchase }, purchase);
        }
    }
}