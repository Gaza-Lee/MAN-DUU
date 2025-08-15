using MANDUU.Models;

namespace MANDUU.Services
{
    public class ProductService
    {
        private short nextId = 0;
        private readonly List<Product> _products;
        private readonly List<MainCategory> _mainCategories;
        private readonly List<SubCategory> _subCategories;
        private readonly List<Shop> _shops;

        private short NextProductId() => nextId++;

        public ProductService()
        {
            // Initialize main categories
            _mainCategories = new List<MainCategory>
            {
                new MainCategory { Id = 1, Name = "Electronics" },
                new MainCategory { Id = 2, Name = "Beauty" },
                new MainCategory { Id = 3, Name = "Fashion" }
            };

            // Initialize subcategories
            _subCategories = new List<SubCategory>
            {
                new SubCategory { Id = 1, Name = "Laptops", MainCategoryId = 1 },
                new SubCategory { Id = 2, Name = "Tablets", MainCategoryId = 1 },
                new SubCategory { Id = 3, Name = "Hair Care", MainCategoryId = 2 },
                new SubCategory { Id = 4, Name = "Nails", MainCategoryId = 2 },
                new SubCategory { Id = 5, Name = "Accessories", MainCategoryId = 3 },
                new SubCategory { Id = 6, Name = "FootWear", MainCategoryId = 3 },
                new SubCategory { Id = 7, Name = "Clothing", MainCategoryId = 3 },
                new SubCategory { Id = 8, Name = "Hats", MainCategoryId = 3 }
            };

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

        private void InitializeProducts()
        {

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Legion 9i",
                Description = "Powerful gaming laptop featuring RTX 3060 graphics, Intel Core i9 processor, high refresh rate display, and premium cooling system.",
                ImageUrl = "legion.png",
                Gallery = new List<string> { "legion1.png", "legion2.png", "legion3.png" },
                Price = 25909.99m,
                TotalSold = 150,
                MainCategoryId = _mainCategories.First(c => c.Name == "Electronics").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "Laptops").Id,
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
                    MainCategoryId = _mainCategories.First(c => c.Name == "Electronics").Id,
                    SubCategoryId = _subCategories.First(s => s.Name == "Tablets").Id,
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
                MainCategoryId = _mainCategories.First(c => c.Name == "Beauty").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "Hair Care").Id,
                ShopId = _shops.First(s => s.Name == "BeautyHub").Id
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Beautiful Nails",
                Description = "Acrylic nail set with elegant designs, perfect for any occasion. Durable and fashionable.",
                ImageUrl = "acrylic.png",
                Gallery = new List<string> { "acrylic1.png", "acrylic2.png", "acrylic3.png" },
                Price = 79.99m,
                TotalSold = 310,
                MainCategoryId = _mainCategories.First(c => c.Name == "Beauty").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "Nails").Id,
                ShopId = _shops.First(s => s.Name == "BeautyHub").Id
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Belt",
                Description = "Classic leather belt with premium buckle, perfect for both casual and formal wear.",
                ImageUrl = "belts.png",
                Gallery = new List<string> { "belts1.png", "belts2.png", "belts3.png" },
                Price = 70.8m,
                TotalSold = 240,
                MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "Accessories").Id,
                ShopId = _shops.First(s => s.Name == "FashionFix").Id
            });

            _products.Add(new Product
                {
                    Id = NextProductId(),
                    Name = "Flats",
                    Description = "Comfortable women's flats with cushioned insole, suitable for daily casual wear.",
                    ImageUrl = "flats.png",
                    Gallery = new List<string> { "flats1.png", "flats2.png", "flats3.png" },
                    Price = 70.8m,
                    TotalSold = 230,
                    MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                    SubCategoryId = _subCategories.First(s => s.Name == "FootWear").Id,
                    ShopId = _shops.First(s => s.Name == "FashionFix").Id
                });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Heels",
                Description = "Stylish high heels with sleek finish, perfect for parties and formal events.",
                ImageUrl = "heels.png",
                Gallery = new List<string> { "heels1.png", "heels2.png", "heels3.png" },
                Price = 70.8m,
                TotalSold = 240,
                MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "FootWear").Id,
                ShopId = _shops.First(s => s.Name == "FashionFix").Id
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Hijab",
                Description = "Elegant hijab made from breathable fabric, suitable for all-day wear.",
                ImageUrl = "hijab.png",
                Gallery = new List<string> { "hijab1.png", "hijab2.png", "hijab3.png" },
                Price = 70.8m,
                TotalSold = 240,
                MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "Hats").Id,
                ShopId = _shops.First(s => s.Name == "FashionFix").Id
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Necklace",
                Description = "Beautiful pendant necklace crafted from stainless steel, perfect gift for loved ones.",
                ImageUrl = "necklace.png",
                Gallery = new List<string> { "necklace1.png", "necklace2.png", "necklace3.png" },
                Price = 70.8m,
                TotalSold = 240,
                MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "Accessories").Id,
                ShopId = _shops.First(s => s.Name == "FashionFix").Id
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Nike Long Sleeves",
                Description = "Premium Nike long sleeve shirt made from breathable and durable fabric.",
                ImageUrl = "nikesleeves.png",
                Gallery = new List<string> { "nikesleeves1.png", "nikesleeves2.png", "nikesleeves3.png" },
                Price = 70.8m,
                TotalSold = 240,
                MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "Clothing").Id,
                ShopId = _shops.First(s => s.Name == "FashionFix").Id
            });

            _products.Add(new Product
                {
                    Id = NextProductId(),
                    Name = "Sneakers",
                    Description = "Trendy sneakers with comfortable fit and stylish design for casual wear.",
                    ImageUrl = "sneakers.png",
                    Gallery = new List<string> { "sneakers1.png", "sneakers2.png", "sneakers3.png" },
                    Price = 70.8m,
                    TotalSold = 240,
                    MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                    SubCategoryId = _subCategories.First(s => s.Name == "FootWear").Id,
                    ShopId = _shops.First(s => s.Name == "FashionFix").Id
                });

            _products.Add(new Product
                {
                    Id = NextProductId(),
                    Name = "Women Casual Clothes",
                    Description = "Lightweight, breathable women’s casual clothing set for everyday comfort.",
                    ImageUrl = "womencasualwear.png",
                    Gallery = new List<string> { "womencasualwear1.png", "womencasualwear2.png", "womencasualwear3.png" },
                    Price = 70.8m,
                    TotalSold = 240,
                    MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                    SubCategoryId = _subCategories.First(s => s.Name == "Clothing").Id,
                    ShopId = _shops.First(s => s.Name == "FashionFix").Id
                });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Women Official Wear",
                Description = "Stylish formal wear for women, ideal for office and corporate events.",
                ImageUrl = "womenofficialwear.png",
                Gallery = new List<string> { "womenofficialwear1.png", "womenofficialwear2.png", "womenofficialwear3.png" },
                Price = 70.8m,
                TotalSold = 240,
                MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "Clothing").Id,
                ShopId = _shops.First(s => s.Name == "FashionFix").Id
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Watches",
                Description = "Elegant wristwatch with leather strap and water resistance.",
                ImageUrl = "wristwatch.png",
                Gallery = new List<string> { "wristwatch1.png", "wristwatch2.png", "wristwatch3.png" },
                Price = 70.8m,
                TotalSold = 240,
                MainCategoryId = _mainCategories.First(c => c.Name == "Fashion").Id,
                SubCategoryId = _subCategories.First(s => s.Name == "Accessories").Id,
                ShopId = _shops.First(s => s.Name == "FashionFix").Id
            });
        }

        // --- Async CRUD operations ---
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
}
