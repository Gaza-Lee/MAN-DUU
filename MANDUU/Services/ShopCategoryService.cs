using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.Models;

namespace MANDUU.Services
{
    public class ShopCategoryService
    {
        private readonly List<ShopCategory> _categories;

        public ShopCategoryService()
        {
            _categories = new List<ShopCategory>
        {
            new ShopCategory { Id = 1, Name = "Fashion", Description = "Clothes, shoes, and accessories.", BannerImage = "fashion_banner.jpg", Icon = "fashion_icon.png", SortOrder = 1 },
            new ShopCategory { Id = 2, Name = "Electronics", Description = "Phones, laptops, and gadgets.", BannerImage = "electronics_banner.jpg", Icon = "electronics_icon.png", SortOrder = 2 },
            new ShopCategory { Id = 3, Name = "Food", Description = "Groceries, restaurants, and snacks.", BannerImage = "food_banner.jpg", Icon = "food_icon.png", SortOrder = 3 }
        };
        }

        public Task<List<ShopCategory>> GetAllCategoriesAsync()
        {
            var ordered = _categories.OrderBy(c => c.SortOrder).ToList();
            return Task.FromResult(ordered);
        }

        public Task<ShopCategory?> GetCategoryByIdAsync(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(category);
        }
    }

}
