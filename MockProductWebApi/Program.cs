using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MockProductWebApi.Models;

namespace MockProductWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new MockProductContext())
            {
                RandomProductInfo randomProductInfo = new RandomProductInfo();
                int currentMockProductCount = db.MockProducts.Count();
                if (currentMockProductCount <= 100)
                {
                    var words = new HashSet<string>();
                    while (words.Count < 100)
                    {
                        var randomProductName = randomProductInfo.GenerateRandomAdjectiveNoun();
                        if (!words.Contains(randomProductName))
                        {
                            Console.WriteLine(randomProductName);
                            words.Add(randomProductName);
                            var mockProduct = randomProductInfo.GenerateMockProduct(randomProductName);
                            db.MockProducts.Add(mockProduct);
                        }
                    }
                    currentMockProductCount = db.SaveChanges();
                }
                if (currentMockProductCount > 0 && db.MockProducts.First().ImgUrl == null)
                {
                    // Fill ImgUrls for non existant. These will be randomly sized square images
                    var mockProducts = db.MockProducts.ToList();
                    mockProducts.Select(product =>
                    {
                        product.ImgUrl = randomProductInfo.GenerateImgUrl();
                        return product;
                    }).ToList();
                    db.SaveChanges();
                }
                Console.WriteLine("{0} MockProducts in database", currentMockProductCount);

                Console.WriteLine();
                Console.WriteLine("All products in database:");
                foreach (var product in db.MockProducts)
                {
                    Console.WriteLine(" - {0}", product.Name);
                }
            }
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
