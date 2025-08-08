using MANDUU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class ProductService
    {
        public IEnumerable<Product> _products;
        public async ValueTask<IEnumerable<Product>> GetProductsAsync()
        {
           if (_products == null)
            {
                var products = new List<Product>
                {
                    new(1, "Smartwatch", "Feature-rich smartwatch", "smartwatch.png", 199.99m, "electronics", 9, 150),
                    new(2, "T-Shirt", "Cotton round neck", "tshirt.png", 25.00m, "clothing", 2, 250),
                    new(3, "Sneakers", "Comfortable running shoes", "sneakers.png", 89.99m, "footWear", 3, 50),
                    new(4, "Foundation", "Skin-friendly makeup base", "foundation.png", 35.00m, "beauty", 7, 120),
                    new(5, "Pizza", "Cheesy pepperoni pizza", "pizza.png", 12.99m, "food", 8, 180),
                };
            }
           return _products;
        }

        public async ValueTask<IEnumerable<Product>> GetBestSellingProductsAsync(int count = 10)
        {
            var products = await GetProductsAsync();
            return products.OrderByDescending(p => p.TotalSold).Take(count);
        }

        public async ValueTask<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await GetProductsAsync();
            return products.Where(p => p.CategoryId == categoryId);
        }
    }
}
