using System;
using System.Collections.Generic;

namespace MockProductWebApi.Models
{
    public class RandomProductInfo
    {
        public Random randomGenerator = new Random();
        public List<string> Adjectives = new List<string>
        {
           "Blue", "Green", "Red", "Yellow", "Orange", "Shiny", "Dull", "Used", "New",
           "Slick", "Lustrous", "Improved", "Fragile", "Sturdy", "Heavy", "Light",
           "Gleaming", "Cooling", "Warming", "Sparkling", "Polarizing", "Hidden" 
        };

        public List<string> Nouns = new List<string>
        {
            "Chair", "Desk", "Monitor", "Laptop", "Motherboard", "CPU", "Video Card", 
            "Case", "RAM", "LEDs", "Fan", "Heat Sink", "Operating System", "Cardboard Box",
            "Book", "Mouse", "Keyboard", "Car", "Truck", "Van", "SUV", "Camera", "Phone"
        };

        public string GenerateRandomAdjectiveNoun()
        {
            var randomAdjectiveIndex = randomGenerator.Next(0, Adjectives.Count);
            var randomNounIndex = randomGenerator.Next(0, Nouns.Count);

            return String.Format("{0} {1}", Adjectives[randomAdjectiveIndex], Nouns[randomNounIndex]);
        }

        public MockProduct GenerateMockProduct(string name)
        {
            var randomPrice = randomGenerator.Next(1, 1000) + 0.99;
            List<MockProductReview> mockReviews = new List<MockProductReview>();
            var randomReviewNumbers = randomGenerator.Next(0, 5);
            for (var index = 0; index < randomReviewNumbers; index++)
            {
                var mockProductReview = GenerateMockProductReview();
                mockReviews.Add(mockProductReview);
            }
            var mockProduct = new MockProduct()
            {
                Name = name,
                Price = (float)randomPrice,
                Reviews = mockReviews
            };

            return mockProduct;
        }

        public MockProductReview GenerateMockProductReview()
        {
            var randomRating = randomGenerator.Next(0, 5);
            var randomReview = "It was terrible!";
            if (randomRating == 3)
            {
                randomReview = "It was ok";
            }
            else if (randomRating > 3)
            {
                randomReview = "It was great!";
            }
       
            var mockProductReview = new MockProductReview()
            {
                MockProductRating = randomRating,
                MockProductReviewContent = randomReview
            };

            return mockProductReview;
        }

        public string GenerateImgUrl()
        {
            var randomSquareSize = randomGenerator.Next(50, 450);

            return $@"http://via.placeholder.com/{randomSquareSize}x{randomSquareSize}";
        }
    }
}
