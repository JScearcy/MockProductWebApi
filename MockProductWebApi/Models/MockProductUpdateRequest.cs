namespace MockProductWebApi.Models
{
    public class MockProductUpdateRequest : RequiresApiKey
    {
        public int MockProductId { get; set; }
        public float? Price { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}
