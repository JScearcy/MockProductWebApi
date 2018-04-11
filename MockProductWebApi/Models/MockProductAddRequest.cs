using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockProductWebApi.Models
{
    public class MockProductAddRequest : RequiresApiKey
    {
        public float Price { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}
