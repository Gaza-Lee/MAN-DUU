using MANDUU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class ShopService
    {
        private List<Shop> _shops = new();
        private readonly ShopCategoryService _shopCategoryService;

        public ShopService(ShopCategoryService shopCategoryService)
        {
            _shopCategoryService = shopCategoryService;
            InitializeShops();
        }

        private async void InitializeShops()
        {
            var shopCategories = (await _shopCategoryService.GetAllShopCategoriesAsync()).ToList();

            _shops = new List<Shop>
            {
                new Shop
                {
                    Id = 1,
                    Name = "GrandMa iShop",
                    Description = "Your one-stop electronics shop with the latest gadgets and devices",
                    Logo = "grandma_ishop_logo.png",
                    CoverImage = "grandma_ishop_cover.jpg",
                    Email = "info@grandmaishop.com",
                    PhoneNumber = "+1234567890",
                    ShortLocation = "Tech District",
                    LongLocation = "123 Tech Street, Silicon Valley, CA",
                    ShopProfileImage = "grandma_shop.png",
                    MainCategoryId = 1, // Electronics
                    AdditionalCategoryIds = new List<int> { 2 }, // Also sells Fashion accessories
                    Products = new List<int> { 1, 2 },
                    CreatedAt = DateTime.UtcNow.AddDays(-30)
                },
                new Shop
                {
                    Id = 2,
                    Name = "Attract Them Saloon",
                    Description = "Premium beauty and wellness products for your self-care routine",
                    Logo = "attract_saloon_logo.png",
                    CoverImage = "attract_saloon_cover.jpg",
                    Email = "bookings@attractsaloon.com",
                    PhoneNumber = "+1987654321",
                    ShortLocation = "Beauty Plaza",
                    LongLocation = "456 Beauty Avenue, Fashion District, NY",
                    ShopProfileImage = "attractthemsaloon_shop.png",
                    MainCategoryId = 3, // Beauty
                    AdditionalCategoryIds = new List<int> { 4 }, // Also sells Health & Wellness products
                    Products = new List<int> { 3, 4 },
                    CreatedAt = DateTime.UtcNow.AddDays(-45)
                },
                new Shop
                {
                    Id = 3,
                    Name = "FashionFix",
                    Description = "Trendy fashion items, accessories, and lifestyle products",
                    Logo = "fashionfix_logo.png",
                    CoverImage = "fashionfix_cover.jpg",
                    Email = "style@fashionfix.com",
                    PhoneNumber = "+1555666777",
                    ShortLocation = "Fashion Mall",
                    LongLocation = "789 Style Boulevard, Trendy District, LA",
                    ShopProfileImage = "fashionfix_shop.jpg",
                    MainCategoryId = 2, // Fashion (primary)
                    AdditionalCategoryIds = new List<int> { 1, 3 }, // Also sells Electronics and Beauty
                    Products = new List<int> { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                    CreatedAt = DateTime.UtcNow.AddDays(-60)
                },
                new Shop
                {
                    Id = 4,
                    Name = "Emart Laptops",
                    Description = "Latest tech gadgets, electronics, and computing devices",
                    Logo = "emart_laptops_logo.png",
                    CoverImage = "emart_laptops_cover.jpg",
                    Email = "sales@emartlaptops.com",
                    PhoneNumber = "+1888999000",
                    ShortLocation = "Tech Center",
                    LongLocation = "321 Computer Road, Digital District, TX",
                    ShopProfileImage = "emart_laptops_profile.jpg",
                    MainCategoryId = 1, // Electronics
                    AdditionalCategoryIds = new List<int>(), // Only electronics
                    Products = new List<int>(),
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new Shop
                {
                    Id = 5,
                    Name = "FreshMart Groceries",
                    Description = "Fresh groceries, organic produce, and daily essentials",
                    Logo = "freshmart_logo.png",
                    CoverImage = "freshmart_cover.jpg",
                    Email = "orders@freshmart.com",
                    PhoneNumber = "+1777888999",
                    ShortLocation = "Market Square",
                    LongLocation = "654 Fresh Lane, Organic District, OR",
                    ShopProfileImage = "freshmart_profile.jpg",
                    MainCategoryId = 4, // Food & Grocery
                    AdditionalCategoryIds = new List<int> { 3 }, // Also sells Beauty products
                    Products = new List<int>(),
                    CreatedAt = DateTime.UtcNow.AddDays(-20)
                },
                new Shop
                {
                    Id = 6,
                    Name = "TechGadgets Hub",
                    Description = "Cutting-edge technology and innovative gadgets",
                    Logo = "techgadgets_logo.png",
                    CoverImage = "techgadgets_cover.jpg",
                    Email = "support@techgadgets.com",
                    PhoneNumber = "+1666777888",
                    ShortLocation = "Innovation Center",
                    LongLocation = "987 Gadget Street, Tech Park, WA",
                    ShopProfileImage = "techgadgets_shop.png",
                    MainCategoryId = 1, // Electronics
                    AdditionalCategoryIds = new List<int> { 2, 3 }, // Also sells Fashion and Beauty tech
                    Products = new List<int>(),
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                }
            };

            // Enrich shops with category objects
            foreach (var shop in _shops)
            {
                shop.MainCategory = shopCategories.FirstOrDefault(sc => sc.Id == shop.MainCategoryId);
                shop.AdditionalCategories = shopCategories
                    .Where(sc => shop.AdditionalCategoryIds.Contains(sc.Id))
                    .ToList();
            }
        }

        public async Task<IEnumerable<Shop>> GetAllShopsAsync()
        {
            return await Task.FromResult(_shops);
        }

        public async Task<Shop> GetShopByIdAsync(int id)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == id);
            return await Task.FromResult(shop);
        }

        public async Task<IEnumerable<Shop>> GetRecommendedShopsAsync(int count = 4)
        {
            return await Task.FromResult(_shops
                .OrderByDescending(s => s.Products.Count)
                .ThenByDescending(s => s.AdditionalCategoryIds.Count) // Shops with more diversity
                .ThenByDescending(s => s.CreatedAt)
                .Take(count)
                .ToList());
        }

        public async Task<IEnumerable<Shop>> GetShopsByCategoryAsync(int categoryId)
        {
            return await Task.FromResult(_shops
                .Where(s => s.MainCategoryId == categoryId || s.AdditionalCategoryIds.Contains(categoryId))
                .ToList());
        }

        public async Task<IEnumerable<Shop>> GetShopsByMainCategoryAsync(int categoryId)
        {
            return await Task.FromResult(_shops
                .Where(s => s.MainCategoryId == categoryId)
                .ToList());
        }

        public async Task<IEnumerable<Shop>> GetShopsWithAdditionalCategoriesAsync()
        {
            return await Task.FromResult(_shops
                .Where(s => s.AdditionalCategoryIds.Any())
                .ToList());
        }

        public async Task<IEnumerable<Shop>> SearchShopsAsync(string searchTerm)
        {
            return await Task.FromResult(_shops
                .Where(s => s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            s.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            s.ShortLocation.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList());
        }

        public async Task<bool> CanShopSellInCategoryAsync(int shopId, int categoryId)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == shopId);
            return shop != null && (shop.MainCategoryId == categoryId || shop.AdditionalCategoryIds.Contains(categoryId));
        }

        public async Task<IEnumerable<ShopCategory>> GetShopCategoriesAsync(int shopId)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == shopId);
            if (shop == null) return Enumerable.Empty<ShopCategory>();

            var categories = new List<ShopCategory> { shop.MainCategory };
            categories.AddRange(shop.AdditionalCategories);

            return await Task.FromResult(categories.Distinct());
        }

        public async Task<IEnumerable<Shop>> GetShopsByOwnerAsync(string ownerId)
        {
            return await Task.FromResult(_shops
                .Where(s => s.OwnerId == ownerId)
                .ToList());
        }

        public async Task<int> GetShopProductCountAsync(int shopId)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == shopId);
            return await Task.FromResult(shop?.Products.Count ?? 0);
        }

        public async Task AddShopAsync(Shop shop)
        {
            shop.Id = _shops.Max(s => s.Id) + 1;
            shop.CreatedAt = DateTime.UtcNow;

            // Enrich with category objects
            var shopCategories = (await _shopCategoryService.GetAllShopCategoriesAsync()).ToList();
            shop.MainCategory = shopCategories.FirstOrDefault(sc => sc.Id == shop.MainCategoryId);
            shop.AdditionalCategories = shopCategories
                .Where(sc => shop.AdditionalCategoryIds.Contains(sc.Id))
                .ToList();

            _shops.Add(shop);
            await Task.CompletedTask;
        }

        public async Task UpdateShopAsync(Shop updatedShop)
        {
            var index = _shops.FindIndex(s => s.Id == updatedShop.Id);
            if (index != -1)
            {
                // Enrich with category objects
                var shopCategories = (await _shopCategoryService.GetAllShopCategoriesAsync()).ToList();
                updatedShop.MainCategory = shopCategories.FirstOrDefault(sc => sc.Id == updatedShop.MainCategoryId);
                updatedShop.AdditionalCategories = shopCategories
                    .Where(sc => updatedShop.AdditionalCategoryIds.Contains(sc.Id))
                    .ToList();

                _shops[index] = updatedShop;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteShopAsync(int id)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == id);
            if (shop != null)
            {
                _shops.Remove(shop);
            }
            await Task.CompletedTask;
        }

        public async Task AddProductToShopAsync(int shopId, int productId)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == shopId);
            if (shop != null && !shop.Products.Contains(productId))
            {
                shop.Products.Add(productId);
            }
            await Task.CompletedTask;
        }

        public async Task RemoveProductFromShopAsync(int shopId, int productId)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == shopId);
            if (shop != null)
            {
                shop.Products.Remove(productId);
            }
            await Task.CompletedTask;
        }

        public async Task AddCategoryToShopAsync(int shopId, int categoryId)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == shopId);
            if (shop != null && !shop.AdditionalCategoryIds.Contains(categoryId))
            {
                shop.AdditionalCategoryIds.Add(categoryId);

                // Update category objects
                var shopCategories = (await _shopCategoryService.GetAllShopCategoriesAsync()).ToList();
                shop.AdditionalCategories = shopCategories
                    .Where(sc => shop.AdditionalCategoryIds.Contains(sc.Id))
                    .ToList();
            }
            await Task.CompletedTask;
        }

        public async Task RemoveCategoryFromShopAsync(int shopId, int categoryId)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == shopId);
            if (shop != null)
            {
                shop.AdditionalCategoryIds.Remove(categoryId);

                // Update category objects
                var shopCategories = (await _shopCategoryService.GetAllShopCategoriesAsync()).ToList();
                shop.AdditionalCategories = shopCategories
                    .Where(sc => shop.AdditionalCategoryIds.Contains(sc.Id))
                    .ToList();
            }
            await Task.CompletedTask;
        }
    }
}