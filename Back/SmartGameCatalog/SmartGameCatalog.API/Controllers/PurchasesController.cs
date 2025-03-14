using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly PurchaseRepository _repository;

        public PurchasesController(PurchaseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
        {
            var purchases = await _repository.GetPurchasesAsync();
            return Ok(purchases);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePurchase(Purchase purchase)
        {
            purchase.Id_Purchase = Guid.NewGuid();
            await _repository.AddPurchaseAsync(purchase);
            return CreatedAtAction(nameof(GetPurchases), new { id = purchase.Id_Purchase }, purchase);
        }
    }
}
