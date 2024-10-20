using MongoAPIWeb.Models;

namespace MongoAPIWeb.Repositories
{
    public interface IProductCollection
    {
        Task InserProduct(Products products);
        Task UpdateProduct(Products products);
        Task DeleteProduct(string id);


        Task<List<Products>> GetAllProducts();
        Task<Products> GetProductById(string id);
    }
}
