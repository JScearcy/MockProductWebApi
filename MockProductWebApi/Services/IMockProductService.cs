using MockProductWebApi.Models;
using System.Collections.Generic;

namespace MockProductWebApi.Services
{
    public interface IMockProductService
    {
        List<MockProduct> GetAllMockProducts(bool includeReviews);
        MockProduct GetMockProductById(int id, bool includeReviews);
        List<MockProduct> SearchMockProducts(int? id, string name, bool includeReviews);
        int AddMockProduct(MockProductAddRequest addRequest);
        bool DeleteMockProduct(MockProductDeleteRequest deleteRequest);
        bool UpdateMockProduct(MockProductUpdateRequest updateRequest);
    }
}
