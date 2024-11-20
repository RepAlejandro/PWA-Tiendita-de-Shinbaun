using MongoAPIWeb.Model;


namespace Proyectotiendita.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetAllProducts();
        Task<Products> GetProductDetails(string id);
        Task SaveProduct(Products product);
        Task DeleteProduct(string id);

    }
}
