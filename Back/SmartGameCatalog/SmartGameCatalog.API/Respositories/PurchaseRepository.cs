using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    public class PurchaseRepository
    {
        private readonly Database _database;

        public PurchaseRepository(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<Purchase>("SELECT * FROM Purchase");
        }

        public async Task<int> AddPurchaseAsync(Purchase purchase)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO Purchase (id_purchase, id_user, id_videogame, store, price, link) VALUES (@Id_Purchase, @Id_User, @Id_VideoGame, @Store, @Price, @Link)";
            return await connection.ExecuteAsync(query, purchase);
        }
    }
}
