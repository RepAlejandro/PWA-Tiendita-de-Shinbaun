using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoAPIWeb.Models;
using MongoAPIWeb.Repositories;

namespace MongoAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductCollection db = new ProductCollection();

        [HttpGet]
        public async Task<IActionResult> GetProductDetails()
        {
        return Ok(await db.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetproductDetails(string id)
        {
            return Ok(await db.GetProductById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Products products)
        {
            if (products == null)
                return BadRequest();
            if (products.NombreP == string.Empty)
            {
                ModelState.AddModelError("NombreP", "El producto no debe estar vacio");

            }
            await db.InserProduct(products);

            return Created("Created", true);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Products products, string id)
        {
            if (products == null)
                return BadRequest();
            if (products.NombreP == string.Empty)
            {
                ModelState.AddModelError("NombreP", "El producto no debe estar vacio");

            }
            products.ID_Productos = new MongoDB.Bson.ObjectId(id);
            await db.UpdateProduct(products);

            return Created("Created", true);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await db.DeleteProduct(id);
            return NoContent();
        }

    }
}
