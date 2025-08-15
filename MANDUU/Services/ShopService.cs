using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.Models;


namespace MANDUU.Services
{
    public class ShopService
    {
        private readonly List<Shop> _shops;
        private readonly List<ShopCategory> _categories;

        public ShopService(List<ShopCategory> categories)
        {
            _categories = categories;

            // Mock shops for UI
            _shops = new List<Shop>
            {
                new Shop
                {
                    Id = 1,
                    Name = "FashionFix",
                    Description = "Trendy clothes and accessories",
                    OwnerId = "owner1",
                    PhoneNumber = "024XXXXXXX",
                    MainCategoryId = 1,
                    MainCategory = _categories.First(c => c.Id == 1),
                    AdditionalCategories = new List<ShopCategory> { _categories.First(c => c.Id == 2) },
                    Products = new List<int> { 1, 2, 3 }
                },
                new Shop
                {
                    Id = 2,
                    Name = "TechHub",
                    Description = "Latest electronics",
                    OwnerId = "owner2",
                    PhoneNumber = "024XXXXXXX",
                    MainCategoryId = 2,
                    MainCategory = _categories.First(c => c.Id == 2),
                    AdditionalCategories = new List<ShopCategory>(),
                    Products = new List<int> { 4, 5 }
                }
            };
        }

        public Task<List<Shop>> GetAllShopsAsync()
        {
            return Task.FromResult(_shops);
        }

        public Task<Shop?> GetShopByIdAsync(int id)
        {
            var shop = _shops.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(shop);
        }

        public Task<List<Shop>> GetShopsByMainCategoryAsync(int mainCategoryId)
        {
            var shops = _shops.Where(s => s.MainCategoryId == mainCategoryId).ToList();
            return Task.FromResult(shops);
        }

        public Task<List<Shop>> GetShopsByAdditionalCategoryAsync(int categoryId)
        {
            var shops = _shops.Where(s => s.AdditionalCategories.Any(c => c.Id == categoryId)).ToList();
            return Task.FromResult(shops);
        }
    }
}
