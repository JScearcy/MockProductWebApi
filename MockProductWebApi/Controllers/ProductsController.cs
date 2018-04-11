using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public JsonResult Get(bool includeReviews = false)
        {
            var mockProducts = mockProductService.GetAllMockProducts(includeReviews);

            return new JsonResult(new { success = true, mockProducts });
        }

        // GET: api/products/5
        [HttpGet("{id}", Name = "Get")]
        public JsonResult Get(int id, bool includeReviews = false)
        {
            var mockProduct = mockProductService.GetMockProductById(id, includeReviews);
            
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
        public JsonResult Search(int? id, string name="", bool includeReviews = false)
        {
            var mockProducts = mockProductService.SearchMockProducts(id, name, includeReviews);

            return new JsonResult(new { success = true, mockProducts });
        }

        //// POST: api/products
        [HttpPost]
        public JsonResult Post([FromBody]MockProductAddRequest request)
        {
            var newProductId = mockProductService.AddMockProduct(request);
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
        public JsonResult Put(int id, [FromBody]MockProductUpdateRequest productUpdate)
        {
            var result = mockProductService.UpdateMockProduct(productUpdate);

            return new JsonResult(new { success = result });
        }

        //// DELETE: api/ApiWithActions/5
        [HttpDelete]
        public JsonResult Delete([FromBody]MockProductDeleteRequest request)
        {
            var deleteSuccess = mockProductService.DeleteMockProduct(request);
            return new JsonResult(new { success = deleteSuccess });
        }
    }
}
