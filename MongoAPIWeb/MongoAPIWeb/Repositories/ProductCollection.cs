
using MongoAPIWeb.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoAPIWeb.Repositories
{
    public class ProductCollection : IProductCollection
    {

        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Products> Collection;

        public ProductCollection() 
        {
            Collection = _repository.db.GetCollection<Products>("Productos");
        }
        public async Task DeleteProduct(string id)
        {
            var filter = Builders<Products>.Filter.Eq(s => s.ID_Productos, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<List<Products>> GetAllProducts()
        {
           return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Products> GetProductById(string id)
        {
           return await Collection.FindAsync(
               new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task InserProduct(Products products)
        {
            await Collection.InsertOneAsync(products);
        }

        public async Task UpdateProduct(Products products)
        {
           var filter = Builders<Products>
                .Filter
                .Eq(s => s.ID_Productos, products.ID_Productos);

            await Collection.ReplaceOneAsync(filter, products);
        }
    }
}
