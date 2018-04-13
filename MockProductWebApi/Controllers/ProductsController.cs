using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MockProductWebApi.Models;
using MockProductWebApi.Services;

namespace MockProductWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        IMockProductService mockProductService;

        public ProductsController(IMockProductService mockProductService)
        {
            this.mockProductService = mockProductService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<JsonResult> Get(bool includeReviews = false)
        {
            var mockProducts = await mockProductService.GetAllMockProductsAsync(includeReviews);

            return new JsonResult(new { success = true, mockProducts });
        }

        // GET: api/products/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<JsonResult> Get(int id, bool includeReviews = false)
        {
            var mockProduct = await mockProductService.GetMockProductByIdAsync(id, includeReviews);
            
            if (mockProduct != null)
            {
                return new JsonResult(new { success = true, mockProduct });
            }
            else
            {
                return new JsonResult(new { success = false });

            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<JsonResult> Search(int? id, string name="", bool includeReviews = false)
        {
            var mockProducts = await mockProductService.SearchMockProductsAsync(id, name, includeReviews);

            return new JsonResult(new { success = true, mockProducts });
        }

        //// POST: api/products
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]MockProductAddRequest request)
        {
            var newProductId = await mockProductService.AddMockProductAsync(request);
            if (newProductId != -1)
            {
                return new JsonResult(new { success = true, mockProductId = newProductId });
            }
            else
            {
                return new JsonResult(new { success = false });
            }

        }

        //// PUT: api/Product/5
        [HttpPut]
        public async Task<JsonResult> Put(int id, [FromBody]MockProductUpdateRequest productUpdate)
        {
            var updateSuccess = await mockProductService.UpdateMockProductAsync(productUpdate);

            return new JsonResult(new { success = updateSuccess });
        }

        //// DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<JsonResult> Delete([FromBody]MockProductDeleteRequest request)
        {
            var deleteSuccess = await mockProductService.DeleteMockProductAsync(request);

            return new JsonResult(new { success = deleteSuccess });
        }
    }
}
