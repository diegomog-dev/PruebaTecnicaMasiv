using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PruebaTecnicaMasiv.Models;
using PruebaTecnicaMasiv.Services;
using MongoDB.Driver.Linq;
using System.Text.Json.Nodes;

namespace PruebaTecnicaMasiv.Services
{
    public class SaleService
    {
        private readonly IMongoCollection<Sales> _sales;
        private readonly IMongoCollection<Discount> _discount;
        public SaleService(IDbSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _sales = database.GetCollection<Sales>("saleClient");
            _discount = database.GetCollection<Discount>("discount");
        }        
        public async Task<List<Sales>> GetAllSales()
        {
            return await _sales.FindAsync(d => true).Result.ToListAsync();
        }
        public async Task<List<Sales>> GetSaleById(string id)
        {
            return await _sales.FindAsync(d => d.Id == id).Result.ToListAsync();
        }
        public async Task<Sales> GetSaleByName(string nameConsole)
        {
            return await _sales.FindAsync(d => d.NameConsole == nameConsole).Result.FirstOrDefaultAsync();
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
        public Discount? FindDiscount(string nameConsole)
        {
            return _discount.Find(s => s.Console == nameConsole).FirstOrDefault();
        }        
        public int TotalDiscount()
        {
            var totalDiscount = _sales.AsQueryable<Sales>();
            var res = (from s in totalDiscount
                      select s).ToList();
            var resulTotal = res.Sum(z => z.DiscountValue);
            return resulTotal;
        }
    }
}
