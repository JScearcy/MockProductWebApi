namespace MockProductWebApi.Models
{
    public class MockProductAddRequest : RequiresApiKey
    {
        public float Price { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}
