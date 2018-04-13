using MockProductWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MockProductWebApi.Services
{
    public interface IMockProductService
    {
        Task<List<MockProduct>> GetAllMockProductsAsync(bool includeReviews);
        Task<MockProduct> GetMockProductByIdAsync(int id, bool includeReviews);
        Task<List<MockProduct>> SearchMockProductsAsync(int? id, string name, bool includeReviews);
        Task<int> AddMockProductAsync(MockProductAddRequest addRequest);
        Task<bool> DeleteMockProductAsync(MockProductDeleteRequest deleteRequest);
        Task<bool> UpdateMockProductAsync(MockProductUpdateRequest updateRequest);
    }
}
