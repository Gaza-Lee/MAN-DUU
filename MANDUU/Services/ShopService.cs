using MANDUU.Models;
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

        private void InitializeShops()
        {
            _shops = new List<Shop>
            {
                new Shop
                {
                    Id = 1,
                    Name = "ElectroHub",
                    Description = "Your one-stop electronics shop",
                    Logo = "electrohub_logo.png",
                    CoverImage = "electrohub_cover.jpg",
                    MainCategoryId = 1,
                    Products = new List<int> { 1, 2 } // Product IDs
                },
                new Shop
                {
                    Id = 2,
                    Name = "BeautyHub",
                    Description = "Premium beauty products",
                    Logo = "beautyhub_logo.png",
                    CoverImage = "beautyhub_cover.jpg",
                    MainCategoryId = 3,
                    Products = new List<int> { 3, 4 }
                },
                new Shop
                {
                    Id = 3,
                    Name = "FashionFix",
                    Description = "Trendy fashion items",
                    Logo = "fashionfix_logo.png",
                    CoverImage = "fashionfix_cover.jpg",
                    MainCategoryId = 2,
                    Products = new List<int> { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }
                },
                new Shop
                {
                    Id = 4,
                    Name = "TechGadgets",
                    Description = "Latest tech gadgets",
                    Logo = "techgadgets_logo.png",
                    CoverImage = "techgadgets_cover.jpg",
                    MainCategoryId = 1,
                    Products = new List<int>()
                },
                new Shop
                {
                    Id = 5,
                    Name = "FreshMart",
                    Description = "Fresh groceries daily",
                    Logo = "freshmart_logo.png",
                    CoverImage = "freshmart_cover.jpg",
                    MainCategoryId = 4,
                    Products = new List<int>()
                }
            };
        }

        public async Task<IEnumerable<Shop>> GetAllShopsAsync()
        {
            return await Task.FromResult(_shops);
        }

        public async Task<Shop> GetShopByIdAsync(int id)
        {
            return await Task.FromResult(_shops.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Shop>> GetRecommendedShopsAsync(int count = 4)
        {
            // Recommendation logic:
            // 1. Shops with most products
            // 2. Then by rating 
            // 3. Then newest shops
            return await Task.FromResult(_shops
                .OrderByDescending(s => s.Products.Count)
                .ThenByDescending(s => s.CreatedAt)
                .Take(count)
                .ToList());
        }

        public async Task<IEnumerable<Shop>> GetShopsByCategoryAsync(int categoryId)
        {
            return await Task.FromResult(_shops
                .Where(s => s.MainCategoryId == categoryId)
                .ToList());
        }
    }
}