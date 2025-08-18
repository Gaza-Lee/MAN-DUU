using MANDUU.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class ShopCategoryService
    {
        private List<ShopCategory> _shopCategories = new();

        public ShopCategoryService()
        {
            InitializeShopCategories();
        }

        private void InitializeShopCategories()
        {
            _shopCategories = new List<ShopCategory>
            {
                new ShopCategory
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Shops selling electronic devices",
                    Icon = "electronics.png",
                    BannerImage = "electronics_banner.png"
                },
                new ShopCategory
                {
                    Id = 2,
                    Name = "Fashion",
                    Description = "Clothing and accessories shops",
                    Icon = "fashion.png",
                    BannerImage = "fashion_banner.png"
                },
                new ShopCategory
                {
                    Id = 3,
                    Name = "Beauty",
                    Description = "Beauty and cosmetics shops",
                    Icon = "beauty.png",
                    BannerImage = "beauty_banner.png"
                },
                new ShopCategory
                {
                    Id = 4,
                    Name = "Food & Grocery",
                    Description = "Food and grocery shops",
                    Icon = "grocery.png",
                    BannerImage = "grocery_banner.png"
                }
            };
        }

        public async Task<IEnumerable<ShopCategory>> GetAllShopCategoriesAsync()
        {
            return await Task.FromResult(_shopCategories);
        }

        public async Task<ShopCategory> GetShopCategoryByIdAsync(int id)
        {
            return await Task.FromResult(_shopCategories.FirstOrDefault(sc => sc.Id == id));
        }
    }
}