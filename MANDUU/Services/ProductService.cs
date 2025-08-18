using MANDUU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class ProductService
    {
        private short nextId = 0;
        private readonly List<Product> _products;
        private readonly ProductCategoryService _productCategoryService;
        private readonly List<Shop> _shops;

        private short NextProductId() => nextId++;

        public ProductService(ProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;

            // Initialize shops
            _shops = new List<Shop>
            {
                new Shop { Id = 1, Name = "ElectroHub" },
                new Shop { Id = 2, Name = "BeautyHub" },
                new Shop { Id = 3, Name = "FashionFix" }
            };

            // Initialize products
            _products = new List<Product>();
            InitializeProducts();
        }

        private async void InitializeProducts()
        {
            // Get categories from ProductCategoryService
            var mainCategories = await _productCategoryService.GetAllMainCategoriesAsync();
            var subCategories = await _productCategoryService.GetAllSubCategoriesAsync();

            // Helper functions to get category IDs
            int GetMainCategoryId(string name) => mainCategories.First(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).Id;
            int GetSubCategoryId(string name) => subCategories.First(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).Id;

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Legion 9i",
                Description = "Powerful gaming laptop featuring RTX 3060 graphics, Intel Core i9 processor, high refresh rate display, and premium cooling system.",
                ImageUrl = "legion.png",
                Gallery = new List<string> { "legion1.png", "legion2.png", "legion3.png" },
                Price = 25909.99m,
                TotalSold = 150,
                MainCategoryId = GetMainCategoryId("Electronics"),
                SubCategoryId = GetSubCategoryId("Laptops"),
                ShopId = _shops.First(s => s.Name == "ElectroHub").Id
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "iPad Pro 11",
                Description = "Apple's iPad Pro with 1TB storage, M1 chip, Liquid Retina display, and support for Apple Pencil & Magic Keyboard.",
                ImageUrl = "ipad.png",
                Gallery = new List<string> { "ipad1.png", "ipad2.png", "ipad3.png" },
                Price = 11087.99m,
                TotalSold = 100,
                MainCategoryId = GetMainCategoryId("Electronics"),
                SubCategoryId = GetSubCategoryId("Tablets"),
                ShopId = _shops.First(s => s.Name == "ElectroHub").Id
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Butterfly Locs",
                Description = "Beautiful, soft, and lightweight butterfly locs hair extensions — perfect for stylish protective hairstyles.",
                ImageUrl = "butterflylocks.png",
                Gallery = new List<string> { "butterflylocks1.png", "butterflylocks2.png", "butterflylocks3.png" },
                Price = 59.99m,
                TotalSold = 200,
                MainCategoryId = GetMainCategoryId("Beauty"),
                SubCategoryId = GetSubCategoryId("Hair Care"),
                ShopId = _shops.First(s => s.Name == "BeautyHub").Id
            });

            // Add all other products similarly...
            // [Rest of your product initialization code]
        }

        // --- Enhanced CRUD operations with category info ---
        public async Task<IEnumerable<Product>> GetProductsAsync() => await Task.FromResult(_products);

        public async Task<Product?> GetProductByIdAsync(int id)
            => await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public async Task<IEnumerable<Product>> GetProductsByMainCategoryAsync(int mainCategoryId)
            => await Task.FromResult(_products.Where(p => p.MainCategoryId == mainCategoryId));

        public async Task<IEnumerable<Product>> GetProductsBySubCategoryAsync(int subCategoryId)
            => await Task.FromResult(_products.Where(p => p.SubCategoryId == subCategoryId));

        public async Task<IEnumerable<Product>> GetProductsByShopAsync(int shopId)
            => await Task.FromResult(_products.Where(p => p.ShopId == shopId));

        public async Task<IEnumerable<Product>> GetBestSellingProductsAsync(int count = 10)
            => await Task.FromResult(_products.OrderByDescending(p => p.TotalSold).Take(count));

        // New methods to get products with full category information
        public async Task<IEnumerable<ProductWithCategories>> GetProductsWithCategoriesAsync()
        {
            var mainCategories = await _productCategoryService.GetAllMainCategoriesAsync();
            var subCategories = await _productCategoryService.GetAllSubCategoriesAsync();

            return _products.Select(p => new ProductWithCategories
            {
                Product = p,
                MainCategory = mainCategories.FirstOrDefault(mc => mc.Id == p.MainCategoryId),
                SubCategory = subCategories.FirstOrDefault(sc => sc.Id == p.SubCategoryId),
                Shop = _shops.FirstOrDefault(s => s.Id == p.ShopId)
            });
        }

        public async Task<ProductWithCategories?> GetProductWithCategoriesByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return null;

            var mainCategories = await _productCategoryService.GetAllMainCategoriesAsync();
            var subCategories = await _productCategoryService.GetAllSubCategoriesAsync();

            return new ProductWithCategories
            {
                Product = product,
                MainCategory = mainCategories.FirstOrDefault(mc => mc.Id == product.MainCategoryId),
                SubCategory = subCategories.FirstOrDefault(sc => sc.Id == product.SubCategoryId),
                Shop = _shops.FirstOrDefault(s => s.Id == product.ShopId)
            };
        }

        public async Task AddProductAsync(Product product)
        {
            _products.Add(product);
            await Task.CompletedTask;
        }

        public async Task UpdateProductAsync(Product updatedProduct)
        {
            var index = _products.FindIndex(p => p.Id == updatedProduct.Id);
            if (index != -1)
                _products[index] = updatedProduct;
            await Task.CompletedTask;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                _products.Remove(product);
            await Task.CompletedTask;
        }
    }

    public class ProductWithCategories
    {
        public Product Product { get; set; }
        public MainCategory MainCategory { get; set; }
        public SubCategory SubCategory { get; set; }
        public Shop Shop { get; set; }
    }
}