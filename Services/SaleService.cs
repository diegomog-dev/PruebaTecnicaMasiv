using MongoDB.Driver;
using PruebaTecnicaMasiv.Models;

namespace PruebaTecnicaMasiv.Services
{
    public class SaleService
    {
        private readonly IMongoCollection<Sales> _sales;
        public SaleService(IDbSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _sales = database.GetCollection<Sales>("saleClient");
        }
        public async Task<List<Sales>> GetAllSales()
        {
            return await _sales.FindAsync(d => true).Result.ToListAsync();
        }
        public async Task<List<Sales>> GetSaleById(string id)
        {
            return await _sales.FindAsync(d => d.Id == id).Result.ToListAsync();
        }
        public async Task<List<Sales>> GetSaleByName(string nameConsole)
        {
            return await _sales.FindAsync(d => d.NameConsole == nameConsole).Result.ToListAsync();
        }
        public async Task CreateSale(Sales sale)
        {            
            await _sales.InsertOneAsync(sale);
        }
        public async Task UpdateSale(string id, Sales sale)
        {
            await _sales.ReplaceOneAsync(sale => sale.Id == id, sale);
        }
        public async Task DeleteSale(string id)
        {
            await _sales.DeleteOneAsync(d => d.Id == id);
        }
    }
}
