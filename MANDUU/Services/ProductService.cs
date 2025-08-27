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
        private readonly ShopService _shopService;

        private short NextProductId() => nextId++;

        public ProductService(ProductCategoryService productCategoryService, ShopService shopService)
        {
            _productCategoryService = productCategoryService;
            _shopService = shopService;

            // Initialize products
            _products = new List<Product>();
            InitializeProducts();
        }

        private async void InitializeProducts()
        {
            // Get categories from ProductCategoryService
            var mainCategories = await _productCategoryService.GetAllMainCategoriesAsync();
            var subCategories = await _productCategoryService.GetAllSubCategoriesAsync();

            // Get shops from ShopService
            var allShops = (await _shopService.GetAllShopsAsync()).ToList();

            // Helper functions to get category and shop IDs
            int GetMainCategoryId(string name) => mainCategories.First(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).Id;
            int GetSubCategoryId(string name) => subCategories.First(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).Id;
            int GetShopId(string name) => allShops.First(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).Id;

            // GrandMa iShop Products (Electronics + Fashion accessories)
            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Legion 9i Gaming Laptop",
                Description = "Powerful gaming laptop featuring RTX 3060 graphics, Intel Core i9 processor, high refresh rate display, and premium cooling system.",
                ImageUrl = "legion.png",
                Gallery = new List<string> { "legion1.png", "legion2.png", "legion3.png" },
                Price = 25909.99m,
                TotalSold = 150,
                MainCategoryId = GetMainCategoryId("Electronics"),
                SubCategoryId = GetSubCategoryId("Laptops"),
                ShopId = GetShopId("GrandMa iShop")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "iPad Pro 11 (1TB)",
                Description = "Apple's iPad Pro with 1TB storage, M1 chip, Liquid Retina display, and support for Apple Pencil & Magic Keyboard.",
                ImageUrl = "ipad.png",
                Gallery = new List<string> { "ipad1.png", "ipad2.png", "ipad3.png" },
                Price = 11087.99m,
                TotalSold = 100,
                MainCategoryId = GetMainCategoryId("Electronics"),
                SubCategoryId = GetSubCategoryId("Tablets"),
                ShopId = GetShopId("GrandMa iShop")
            });

            // Attract Them Saloon Products (Beauty + Health & Wellness)
            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Butterfly Locs Hair Extensions",
                Description = "Beautiful, soft, and lightweight butterfly locs hair extensions — perfect for stylish protective hairstyles.",
                ImageUrl = "butterflylocks.png",
                Gallery = new List<string> { "butterflylocks1.png", "butterflylocks2.png", "butterflylocks3.png" },
                Price = 59.99m,
                TotalSold = 200,
                MainCategoryId = GetMainCategoryId("Beauty"),
                SubCategoryId = GetSubCategoryId("Hair Care"),
                ShopId = GetShopId("Attract Them Saloon")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Premium Acrylic Nails Set",
                Description = "Professional acrylic nail set with elegant designs, perfect for any occasion. Durable and fashionable.",
                ImageUrl = "acrylic.png",
                Gallery = new List<string> { "acrylic1.png", "acrylic2.png", "acrylic3.png" },
                Price = 79.99m,
                TotalSold = 310,
                MainCategoryId = GetMainCategoryId("Beauty"),
                SubCategoryId = GetSubCategoryId("Nails"),
                ShopId = GetShopId("Attract Them Saloon")
            });

            // FashionFix Products (Fashion + Electronics + Beauty)
            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Classic Leather Belt",
                Description = "Premium leather belt with stylish buckle, perfect for both casual and formal wear.",
                ImageUrl = "belt.png",
                Gallery = new List<string> { "belt1.png", "belt2.png", "belt3.png" },
                Price = 45.99m,
                TotalSold = 180,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Accessories"),
                ShopId = GetShopId("FashionFix")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Women's Comfort Flats",
                Description = "Comfortable women's flats with cushioned insole, suitable for daily casual wear.",
                ImageUrl = "flats.png",
                Gallery = new List<string> { "flats1.png", "flats2.png", "flats3.png" },
                Price = 89.99m,
                TotalSold = 240,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Footwear"),
                ShopId = GetShopId("FashionFix")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Elegant High Heels",
                Description = "Stylish high heels with sleek finish, perfect for parties and formal events.",
                ImageUrl = "heels.png",
                Gallery = new List<string> { "heels1.png", "heels2.png", "heels3.png" },
                Price = 120.99m,
                TotalSold = 150,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Footwear"),
                ShopId = GetShopId("FashionFix")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Premium Silk Hijab",
                Description = "Elegant hijab made from breathable silk fabric, suitable for all-day wear.",
                ImageUrl = "hijab.png",
                Gallery = new List<string> { "hijab1.png", "hijab2", "hijab3.png" },
                Price = 35.99m,
                TotalSold = 300,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Hats"),
                ShopId = GetShopId("FashionFix")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Gold Pendant Necklace",
                Description = "Beautiful pendant necklace crafted from stainless steel with gold finish, perfect gift for loved ones.",
                ImageUrl = "necklace.png",
                Gallery = new List<string> { "necklace1.png", "necklace2.png", "necklace3.png" },
                Price = 199.99m,
                TotalSold = 95,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Accessories"),
                ShopId = GetShopId("FashionFix")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Nike Long Sleeve Shirt",
                Description = "Premium Nike long sleeve shirt made from breathable and durable fabric.",
                ImageUrl = "nikesleeves.png",
                Gallery = new List<string> { "nikesleeves1.png", "nikesleeves2.png", "nikesleeves3.png" },
                Price = 70.8m,
                TotalSold = 240,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Clothing"),
                ShopId = GetShopId("FashionFix")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Trendy Sneakers",
                Description = "Trendy sneakers with comfortable fit and stylish design for casual wear.",
                ImageUrl = "sneakers.png",
                Gallery = new List<string> { "sneakers1.png", "sneakers2.png", "sneakers3.png" },
                Price = 120.5m,
                TotalSold = 180,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Footwear"),
                ShopId = GetShopId("FashionFix")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Women's Casual Outfit Set",
                Description = "Lightweight, breathable women's casual clothing set for everyday comfort.",
                ImageUrl = "womencasualwear.png",
                Gallery = new List<string> { "womencasualwear1.png", "womencasualwear2.png", "womencasualwear3.png" },
                Price = 85.99m,
                TotalSold = 160,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Clothing"),
                ShopId = GetShopId("FashionFix")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Women's Formal Office Wear",
                Description = "Stylish formal wear for women, ideal for office and corporate events.",
                ImageUrl = "womenofficialwear.png",
                Gallery = new List<string> { "womenofficialwear1.png", "womenofficialwear2.png", "womenofficialwear3.png" },
                Price = 129.99m,
                TotalSold = 110,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Clothing"),
                ShopId = GetShopId("FashionFix")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Elegant Wristwatch",
                Description = "Elegant wristwatch with leather strap and water resistance.",
                ImageUrl = "wristwatch.png",
                Gallery = new List<string> { "wristwatch1.png", "wristwatch2.png", "wristwatch3.png" },
                Price = 159.99m,
                TotalSold = 90,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Accessories"),
                ShopId = GetShopId("FashionFix")
            });

            // FashionFix also sells Electronics (additional category)
            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Wireless Smart Earphones",
                Description = "Stylish wireless earphones with premium sound quality and comfortable fit.",
                ImageUrl = "earphones.png",
                Gallery = new List<string> { "earphones1.png", "earphones2.png", "earphones3.png" },
                Price = 89.99m,
                TotalSold = 120,
                MainCategoryId = GetMainCategoryId("Electronics"),
                SubCategoryId = GetSubCategoryId("Accessories"),
                ShopId = GetShopId("FashionFix")
            });

            // FashionFix also sells Beauty (additional category)
            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Designer Perfume Set",
                Description = "Elegant perfume set with long-lasting fragrance, perfect for special occasions.",
                ImageUrl = "perfume.png",
                Gallery = new List<string> { "perfume1.png", "perfume2.png", "perfume3.png" },
                Price = 75.99m,
                TotalSold = 85,
                MainCategoryId = GetMainCategoryId("Beauty"),
                SubCategoryId = GetSubCategoryId("Fragrances"),
                ShopId = GetShopId("FashionFix")
            });

            // Emart Laptops Products (Electronics only)
            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Gaming Desktop PC",
                Description = "High-performance gaming desktop with RGB lighting and liquid cooling.",
                ImageUrl = "gaming_pc.png",
                Gallery = new List<string> { "gaming_pc1.png", "gaming_pc2.png", "gaming_pc3.png" },
                Price = 1899.99m,
                TotalSold = 75,
                MainCategoryId = GetMainCategoryId("Electronics"),
                SubCategoryId = GetSubCategoryId("Gaming"),
                ShopId = GetShopId("Emart Laptops")
            });

            // FreshMart Products (Grocery + Beauty)
            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Organic Fresh Apples (1kg)",
                Description = "Fresh organic apples from local farm, crisp and sweet.",
                ImageUrl = "apples.png",
                Gallery = new List<string> { "apples1.png", "apples2.png", "apples3.png" },
                Price = 4.99m,
                TotalSold = 500,
                MainCategoryId = GetMainCategoryId("Grocery"),
                SubCategoryId = GetSubCategoryId("Fruits & Vegetables"),
                ShopId = GetShopId("FreshMart Groceries")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Farm Fresh Milk (1L)",
                Description = "Fresh dairy milk from grass-fed cows, pasteurized and homogenized.",
                ImageUrl = "milk.png",
                Gallery = new List<string> { "milk1.png", "milk2.png", "milk3.png" },
                Price = 3.49m,
                TotalSold = 420,
                MainCategoryId = GetMainCategoryId("Grocery"),
                SubCategoryId = GetSubCategoryId("Dairy Products"),
                ShopId = GetShopId("FreshMart Groceries")
            });

            // FreshMart also sells Beauty products (additional category)
            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Organic Face Cream",
                Description = "Natural organic face cream with moisturizing properties for all skin types.",
                ImageUrl = "face_cream.png",
                Gallery = new List<string> { "face_cream1.png", "face_cream2.png", "face_cream3.png" },
                Price = 24.99m,
                TotalSold = 180,
                MainCategoryId = GetMainCategoryId("Beauty"),
                SubCategoryId = GetSubCategoryId("Skin Care"),
                ShopId = GetShopId("FreshMart Groceries")
            });

            // TechGadgets Hub Products (Electronics + Fashion + Beauty tech)
            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Smart Fitness Tracker",
                Description = "Advanced fitness tracker with heart rate monitoring and sleep tracking.",
                ImageUrl = "fitness_tracker.png",
                Gallery = new List<string> { "fitness_tracker1.png", "fitness_tracker2.png", "fitness_tracker3.png" },
                Price = 129.99m,
                TotalSold = 210,
                MainCategoryId = GetMainCategoryId("Electronics"),
                SubCategoryId = GetSubCategoryId("Accessories"),
                ShopId = GetShopId("TechGadgets Hub")
            });

            _products.Add(new Product
            {
                Id = NextProductId(),
                Name = "Bluetooth Beanie Hat",
                Description = "Warm beanie hat with built-in Bluetooth headphones for music on the go.",
                ImageUrl = "bluetooth_beanie.png",
                Gallery = new List<string> { "bluetooth_beanie1.png", "bluetooth_beanie2.png", "bluetooth_beanie3.png" },
                Price = 49.99m,
                TotalSold = 95,
                MainCategoryId = GetMainCategoryId("Fashion"),
                SubCategoryId = GetSubCategoryId("Hats"),
                ShopId = GetShopId("TechGadgets Hub")
            });

            // POPULATE SHOP INFORMATION FOR ALL PRODUCTS
            foreach (var product in _products)
            {
                var shop = allShops.FirstOrDefault(s => s.Id == product.ShopId);
                if (shop != null)
                {
                    product.ShopName = shop.Name;
                    product.ShopLocation = shop.ShortLocation;
                }

                // Ensure Gallery is never null
                product.Gallery ??= new List<string>();

                // Ensure main image is always in the gallery
                if (!string.IsNullOrEmpty(product.ImageUrl) &&
                    !product.Gallery.Contains(product.ImageUrl))
                {
                    product.Gallery.Insert(0, product.ImageUrl);
                }
            }
        }

        // --- Category Validation Methods ---
        public async Task<OperationResult> ValidateProductCategoryAsync(Product product)
        {
            var canSell = await _shopService.CanShopSellInCategoryAsync(product.ShopId, product.MainCategoryId);

            if (!canSell)
            {
                var shop = await _shopService.GetShopByIdAsync(product.ShopId);
                return new OperationResult
                {
                    Success = false,
                    Message = $"Shop '{shop.Name}' cannot sell products in this category. " +
                             $"Allowed categories: {shop.MainCategory.Name} and " +
                             string.Join(", ", shop.AdditionalCategories.Select(c => c.Name))
                };
            }

            return new OperationResult { Success = true };
        }

        public async Task<OperationResult> AddProductWithValidationAsync(Product product)
        {
            var validation = await ValidateProductCategoryAsync(product);
            if (!validation.Success)
                return validation;

            await AddProductAsync(product);
            return new OperationResult { Success = true, Message = "Product added successfully" };
        }

        public async Task<OperationResult> UpdateProductWithValidationAsync(Product updatedProduct)
        {
            var validation = await ValidateProductCategoryAsync(updatedProduct);
            if (!validation.Success)
                return validation;

            await UpdateProductAsync(updatedProduct);
            return new OperationResult { Success = true, Message = "Product updated successfully" };
        }

        // --- CRUD Operations ---
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

        // --- Enhanced Methods with Full Category and Shop Information ---
        public async Task<IEnumerable<ProductWithCategories>> GetProductsWithCategoriesAsync()
        {
            var mainCategories = await _productCategoryService.GetAllMainCategoriesAsync();
            var subCategories = await _productCategoryService.GetAllSubCategoriesAsync();
            var allShops = await _shopService.GetAllShopsAsync();

            return _products.Select(p => new ProductWithCategories
            {
                Product = p,
                MainCategory = mainCategories.FirstOrDefault(mc => mc.Id == p.MainCategoryId),
                SubCategory = subCategories.FirstOrDefault(sc => sc.Id == p.SubCategoryId),
                Shop = allShops.FirstOrDefault(s => s.Id == p.ShopId)
            });
        }

        public async Task<ProductWithCategories?> GetProductWithCategoriesByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return null;

            var mainCategories = await _productCategoryService.GetAllMainCategoriesAsync();
            var subCategories = await _productCategoryService.GetAllSubCategoriesAsync();
            var allShops = await _shopService.GetAllShopsAsync();

            return new ProductWithCategories
            {
                Product = product,
                MainCategory = mainCategories.FirstOrDefault(mc => mc.Id == product.MainCategoryId),
                SubCategory = subCategories.FirstOrDefault(sc => sc.Id == product.SubCategoryId),
                Shop = allShops.FirstOrDefault(s => s.Id == product.ShopId)
            };
        }

        public async Task<IEnumerable<ProductWithCategories>> GetProductsByShopWithCategoriesAsync(int shopId)
        {
            var shopProducts = _products.Where(p => p.ShopId == shopId).ToList();
            var mainCategories = await _productCategoryService.GetAllMainCategoriesAsync();
            var subCategories = await _productCategoryService.GetAllSubCategoriesAsync();
            var allShops = await _shopService.GetAllShopsAsync();

            return shopProducts.Select(p => new ProductWithCategories
            {
                Product = p,
                MainCategory = mainCategories.FirstOrDefault(mc => mc.Id == p.MainCategoryId),
                SubCategory = subCategories.FirstOrDefault(sc => sc.Id == p.SubCategoryId),
                Shop = allShops.FirstOrDefault(s => s.Id == p.ShopId)
            });
        }

        // --- Write Operations ---
        public async Task AddProductAsync(Product product)
        {
            // Populate shop info when adding new products
            var allShops = await _shopService.GetAllShopsAsync();
            var shop = allShops.FirstOrDefault(s => s.Id == product.ShopId);
            if (shop != null)
            {
                product.ShopName = shop.Name;
                product.ShopLocation = shop.ShortLocation;
            }

            product.Id = NextProductId();
            _products.Add(product);
            await Task.CompletedTask;
        }

        public async Task UpdateProductAsync(Product updatedProduct)
        {
            // Populate shop info when updating products
            var allShops = await _shopService.GetAllShopsAsync();
            var shop = allShops.FirstOrDefault(s => s.Id == updatedProduct.ShopId);
            if (shop != null)
            {
                updatedProduct.ShopName = shop.Name;
                updatedProduct.ShopLocation = shop.ShortLocation;
            }

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

        // --- Utility Methods ---
        public async Task<int> GetProductCountByShopAsync(int shopId)
            => await Task.FromResult(_products.Count(p => p.ShopId == shopId));

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await Task.FromResult(_products
                .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                           p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList());
        }

        public async Task<IEnumerable<Product>> GetProductsInPriceRangeAsync(decimal minPrice, decimal maxPrice)
            => await Task.FromResult(_products.Where(p => p.Price >= minPrice && p.Price <= maxPrice));

        public async Task<IEnumerable<Product>> GetProductsByShopAndCategoryAsync(int shopId, int categoryId)
        {
            return await Task.FromResult(_products
                .Where(p => p.ShopId == shopId && p.MainCategoryId == categoryId)
                .ToList());
        }

        // Get products by shop location ---
        public async Task<IEnumerable<Product>> GetProductsByShopLocationAsync(string location)
        {
            return await Task.FromResult(_products
                .Where(p => p.ShopLocation != null && p.ShopLocation.Equals(location, StringComparison.OrdinalIgnoreCase))
                .ToList());
        }

        // Get products by shop name ---
        public async Task<IEnumerable<Product>> GetProductsByShopNameAsync(string shopName)
        {
            return await Task.FromResult(_products
                .Where(p => p.ShopName != null && p.ShopName.Equals(shopName, StringComparison.OrdinalIgnoreCase))
                .ToList());
        }
    }

    public class ProductWithCategories
    {
        public Product Product { get; set; }
        public MainCategory MainCategory { get; set; }
        public SubCategory SubCategory { get; set; }
        public Shop Shop { get; set; }
    }

    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}