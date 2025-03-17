using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de compras de videojuegos en la base de datos.
    /// </summary>
    public class PurchaseRepository
    {
        private readonly Database _database;

        public PurchaseRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene la lista de todas las compras registradas en la base de datos.
        /// </summary>
        /// <returns>Lista de compras realizadas por los usuarios.</returns>
       public async Task<IEnumerable<Purchase>> GetPurchasesAsync()
{
    using var connection = _database.CreateConnection();
    return await connection.QueryAsync<Purchase>(
        "SELECT id AS Id_Purchase, user_id AS Id_User, videogame_id AS Id_VideoGame, purchase_date, store, price FROM purchases"
    );
}


        /// <summary>
        /// Registra una nueva compra en la base de datos.
        /// </summary>
        /// <param name="purchase">Objeto que representa la compra a registrar.</param>
        /// <returns>Número de filas afectadas.</returns>
        public async Task<int> AddPurchaseAsync(Purchase purchase)
        {
            using var connection = _database.CreateConnection();
            var query = @"
                INSERT INTO purchases (id, user_id, videogame_id, purchase_date, store, price) 
                VALUES (@Id_Purchase, @Id_User, @Id_VideoGame, @Purchase_Date, @Store, @Price)";
            return await connection.ExecuteAsync(query, purchase);
        }
    }
}
