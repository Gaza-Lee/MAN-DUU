using MANDUU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class ProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product(1, "Legion 9i", "RTX 3060\nIntel core i9", "legion.png", 25909.99m, "Electronics", "Laptops", 150),
                new Product(2, "Ipad Pro 11", "1TB", "ipad.png", 11087.99m, "Electronics", "Tablets", 100),
                new Product(3, "Beautiful Nails", "Nice design of nail painting", "nail.png", 79.99m, "Beauty", "Nails", 310)
            };
        }

        public async ValueTask<IEnumerable<Product>> GetProductsAsync()
            => await Task.FromResult(_products);

        public async ValueTask<Product?> GetProductByIdAsync(int id)
            => await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public async ValueTask<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            var products = await GetProductsAsync();
            return products.Where(p =>
                string.Equals(p.MainCategoryName, categoryName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(p.SubCategoryName, categoryName, StringComparison.OrdinalIgnoreCase));
        }

        public async ValueTask<IEnumerable<Product>> GetBestSellingProductsAsync(int count = 10)
            => await Task.FromResult(_products.OrderByDescending(p => p.TotalSold).Take(count));

        // Return main category info (banner, etc.)
        public async Task<MainCategory?> GetMainCategoryAsync(string mainCategoryName)
        {
            var mainCategories = new List<MainCategory>
            {

                //TODO: Replace with actual data retrieval logic
                new MainCategory { Name = "Fashion", BannerImageUrl = "fashion-banner.png" },
                new MainCategory { Name = "Electronics", BannerImageUrl = "electronics-banner.png" },
                new MainCategory { Name = "Beauty", BannerImageUrl = "beauty-banner.png" }
            };

            return await Task.FromResult(
                mainCategories.FirstOrDefault(c => string.Equals(c.Name, mainCategoryName, StringComparison.OrdinalIgnoreCase))
            );
        }

        public async ValueTask AddProductAsync(Product product) { _products.Add(product); await Task.CompletedTask; }
        public async ValueTask DeleteProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null) _products.Remove(product);
            await Task.CompletedTask;
        }
        public async ValueTask UpdateProductAsync(Product updatedProduct)
        {
            var index = _products.FindIndex(p => p.Id == updatedProduct.Id);
            if (index != -1) _products[index] = updatedProduct;
            await Task.CompletedTask;
        }
    }
}
