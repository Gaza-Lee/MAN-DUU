using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public class Product
    {
        public Product(int id, string name, string description, string imageUrl, decimal price, string mainCategoryName, string subCategoryName, int totalSold)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            MainCategoryName = mainCategoryName;
            SubCategoryName = subCategoryName;
            TotalSold = totalSold;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public int TotalSold { get; set; } 
    }

    public class MainCategory
    {
        public string Name { get; set; }
        public string BannerImageUrl { get; set; }
    }
}
