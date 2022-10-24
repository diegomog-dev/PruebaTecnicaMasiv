using MongoDB.Driver;
using PruebaTecnicaMasiv.Models;
using SpreadsheetLight;
using System.Collections.Generic;

namespace PruebaTecnicaMasiv.Services
{
    public class DiscountService
    {
        private readonly IMongoCollection<Discount> _discount;
        public DiscountService(IDbSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _discount = database.GetCollection<Discount>("discount");
        }
        public async Task<List<Discount>> GetAllDiscount()
        {
            return await _discount.FindAsync(d => true).Result.ToListAsync();
        }
        public async Task<List<Discount>> GetDiscountById(string id)
        {
            return await _discount.FindAsync(d => d.Id == id).Result.ToListAsync();
        }
        public async Task<Discount> GetDiscountByName(string console)
        {
            return await _discount.FindAsync(d => d.Console == console).Result.FirstOrDefaultAsync();
        }
        public async Task CreateDiscount(Discount discount)
        {
            await _discount.InsertOneAsync(discount);
        }
        public async Task UpdateDiscount(string id,Discount discount)
        {
            await _discount.ReplaceOneAsync(discount => discount.Id == id, discount);
        }
        public async Task DeleteDiscount(string id)
        {
            await _discount.DeleteOneAsync(d => d.Id == id);
        }
        public void ImportDataExcel(string path)
        {
            SLDocument sl = new SLDocument(path);            
            int iRow = 2;
            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
            {
                Discount importData = new Discount();
                string console = sl.GetCellValueAsString(iRow, 1);
                int pricemin = sl.GetCellValueAsInt32(iRow, 2);
                int pricemax = sl.GetCellValueAsInt32(iRow, 3);
                int DiscountValue = sl.GetCellValueAsInt32(iRow, 4);
                importData.Console = console;
                importData.PriceMin = pricemin;
                importData.PriceMax = pricemax;
                importData.DiscountValue= DiscountValue;
                _discount.InsertOne(importData);
                iRow++;
            }            
        }
    }
}
