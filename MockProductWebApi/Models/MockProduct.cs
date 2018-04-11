using System.Collections.Generic;

namespace MockProductWebApi.Models
{
    public class MockProduct
    {
        public int MockProductId { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public List<MockProductReview> Reviews { get; set; }
    }
}
