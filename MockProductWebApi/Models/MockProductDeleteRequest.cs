namespace MockProductWebApi.Models
{
    public class MockProductDeleteRequest : RequiresApiKey
    {
        public int MockProductId { get; set; }
    }
}
