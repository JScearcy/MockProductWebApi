using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MockProductWebApi.Models;
using System.Collections.Generic;
using System.Linq;

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

        public List<MockProduct> GetAllMockProducts(bool includeReviews)
        {
            if (includeReviews)
            {
                return dbContext.MockProducts.Include(p => p.Reviews).ToList();
            } 
            else
            {
                return dbContext.MockProducts.ToList();
            }
        }

        public MockProduct GetMockProductById(int id, bool includeReviews)
        {
            if (includeReviews)
            {
                return dbContext.MockProducts.Include(p => p.Reviews).FirstOrDefault(product => product.MockProductId == id);
            }
            else
            {
                return dbContext.MockProducts.FirstOrDefault(product => product.MockProductId == id);
            }
        }

        public List<MockProduct> SearchMockProducts(int? id, string name, bool includeReviews)
        {
            IQueryable<MockProduct> dbQuery;
            if (includeReviews)
            {
                dbQuery = dbContext.MockProducts.Include(p => p.Reviews).Where(p => p != null);
            }
            else
            {
                dbQuery = dbContext.MockProducts.Where(p => p != null);
            }
            if (id != null)
            {
                dbQuery = dbQuery.Where(p => p.MockProductId == id);
            }
            if (name != "")
            {
                dbQuery = dbQuery.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }
            return dbQuery.ToList();
        }

        public int AddMockProduct(MockProductAddRequest addRequest)
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
                dbContext.SaveChanges();
                return newProduct.MockProductId;
            }
            else
            {
                return -1;
            }
        } 

        public bool DeleteMockProduct(MockProductDeleteRequest deleteRequest)
        {
            if (deleteRequest.ApiKey == Configuration["MockProductApiKey"])
            {
                var elementToDelete = dbContext.MockProducts.FirstOrDefault(product => product.MockProductId == deleteRequest.MockProductId);
                if (elementToDelete != null)
                {
                    dbContext.MockProducts.Remove(elementToDelete);
                    dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool UpdateMockProduct(MockProductUpdateRequest updateRequest)
        {
            if (updateRequest.ApiKey == Configuration["MockProductApiKey"])
            {
                var productToUpdate = dbContext.MockProducts.FirstOrDefault(product => product.MockProductId == updateRequest.MockProductId);
                if (productToUpdate != null)
                {
                    productToUpdate.ImgUrl = updateRequest.ImgUrl ?? productToUpdate.ImgUrl;
                    productToUpdate.Name = updateRequest.Name ?? productToUpdate.Name;
                    productToUpdate.Price = updateRequest.Price ?? productToUpdate.Price;
                    dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}
