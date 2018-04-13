using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MockProductWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockProductWebApi.Services
{
    public class MockProductService : IMockProductService
    {
        private IMockProductDbContext dbContext;
        private IConfiguration Configuration;

        public MockProductService(IMockProductDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.Configuration = configuration;
        }

        public Task<List<MockProduct>> GetAllMockProductsAsync(bool includeReviews)
        {
            Task<List<MockProduct>> mockProductsQuery;
            if (includeReviews)
            {
                mockProductsQuery = dbContext.MockProducts.Include(p => p.Reviews).ToListAsync();
            } 
            else
            {
                mockProductsQuery = dbContext.MockProducts.ToListAsync();
            }

            return mockProductsQuery;
        }

        public Task<MockProduct> GetMockProductByIdAsync(int id, bool includeReviews)
        {
            Task<MockProduct> mockProduct;
            if (includeReviews)
            {
                mockProduct = dbContext.MockProducts
                        .Include(p => p.Reviews)
                        .FirstOrDefaultAsync(product => product.MockProductId == id);
            }
            else
            {
                mockProduct = dbContext.MockProducts
                        .FirstOrDefaultAsync(product => product.MockProductId == id);
            }

            return mockProduct;
        }

        public Task<List<MockProduct>> SearchMockProductsAsync(int? id, string name, bool includeReviews)
        {
            IQueryable<MockProduct> dbQuery;
            if (includeReviews)
            {
                dbQuery = dbContext.MockProducts.Include(p => p.Reviews).AsQueryable();
            }
            else
            {
                dbQuery = dbContext.MockProducts.AsQueryable();
            }

            if (id != null)
            {
                dbQuery = dbQuery.Where(p => p.MockProductId == id);
            }

            if (name != "")
            {
                dbQuery = dbQuery.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            return dbQuery.ToListAsync();
        }

        public Task<int> AddMockProductAsync(MockProductAddRequest addRequest)
        {
            if (addRequest.ApiKey == Configuration["MockProductApiKey"])
            {
                var newProduct = new MockProduct()
                {
                    Name = addRequest.Name,
                    Price = addRequest.Price,
                    ImgUrl = addRequest.ImgUrl
                };
                dbContext.MockProducts.Add(newProduct);
                return dbContext.SaveChangesAsync().ContinueWith(changeCount => newProduct.MockProductId);
            }
            else
            {
                return Task.Run(() => -1);
            }
        } 

        public Task<bool> DeleteMockProductAsync(MockProductDeleteRequest deleteRequest)
        {
            if (deleteRequest.ApiKey == Configuration["MockProductApiKey"])
            {
                var elementToDelete = dbContext.MockProducts.FirstOrDefault(product => product.MockProductId == deleteRequest.MockProductId);
                if (elementToDelete != null)
                {
                    dbContext.MockProducts.Remove(elementToDelete);
                    return dbContext.SaveChangesAsync().ContinueWith(changeCount => changeCount.Result > 0);
                }
            }

            return Task.Run(() => false);
        }

        public Task<bool> UpdateMockProductAsync(MockProductUpdateRequest updateRequest)
        {
            if (updateRequest.ApiKey == Configuration["MockProductApiKey"])
            {
                var productToUpdate = dbContext.MockProducts.FirstOrDefault(product => product.MockProductId == updateRequest.MockProductId);
                if (productToUpdate != null)
                {
                    productToUpdate.ImgUrl = updateRequest.ImgUrl ?? productToUpdate.ImgUrl;
                    productToUpdate.Name = updateRequest.Name ?? productToUpdate.Name;
                    productToUpdate.Price = updateRequest.Price ?? productToUpdate.Price;
                    return dbContext.SaveChangesAsync().ContinueWith(changeCount => changeCount.Result > 0);
                }
            }
            return Task.Run(() => false);
        }
    }
}
