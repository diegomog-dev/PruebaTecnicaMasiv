using MongoDB.Driver;
using PruebaTecnicaMasiv.Models;

namespace PruebaTecnicaMasiv.Services
{
    public class DiscountService
    {
        private IMongoCollection<Discount> _discount;
        public DiscountService(IDbSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _discount = database.GetCollection<Discount>(settings.Collection);
        }
        public List<Discount> Get()
        {
            return _discount.Find(d => true).ToList();
        }
        public Discount Create(Discount discount)
        {
            _discount.InsertOne(discount);
            return discount;
        }
        public void Update(string id,Discount discount)
        {
            _discount.ReplaceOne(discount => discount.Id == id, discount);
        }
        public void Delete(string id)
        {
            _discount.DeleteOne(d => d.Id == id);
        }
    }
}
