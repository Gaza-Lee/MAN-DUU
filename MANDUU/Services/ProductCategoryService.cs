using MANDUU.Enumeration;
using MANDUU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class ProductCategoryService
    {
        private IEnumerable<MainCategory> _mainCategories;
        private int _nextId = 1;

        private int NextId() => _nextId++;

        public async ValueTask<IEnumerable<MainCategory>> GetAllMainCategoriesAsync()
        {
            if (_mainCategories == null)
            {
                var mainCategories = new List<MainCategory>();

                // Fashion
                var fashionId = NextId();
                var fashion = new MainCategory
                {
                    Id = fashionId,
                    Name = "Fashion",
                    Icon = "fashion.png",
                    BannerImage = "fashion_banner.png",
                    CategoryType = CategoryType.Product
                };
                fashion.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Clothing", MainCategoryId = fashionId });
                fashion.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Footwear", MainCategoryId = fashionId });
                fashion.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Hats", MainCategoryId = fashionId });
                fashion.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Accessories", MainCategoryId = fashionId });
                mainCategories.Add(fashion);

                // Grocery
                var groceryId = NextId();
                var grocery = new MainCategory
                {
                    Id = groceryId,
                    Name = "Grocery",
                    Icon = "grocery.png",
                    BannerImage = "grocery_banner.png",
                    CategoryType = CategoryType.Product
                };
                grocery.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Fruits & Vegetables", MainCategoryId = groceryId });
                grocery.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Dairy Products", MainCategoryId = groceryId });
                mainCategories.Add(grocery);

                // Beauty
                var beautyId = NextId();
                var beauty = new MainCategory
                {
                    Id = beautyId,
                    Name = "Beauty",
                    Icon = "beauty.png",
                    BannerImage = "beauty_banner.png",
                    CategoryType = CategoryType.Product
                };
                beauty.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Makeup", MainCategoryId = beautyId });
                beauty.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Hair Care", MainCategoryId = beautyId });
                beauty.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Skin Care", MainCategoryId = beautyId });
                beauty.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Nails", MainCategoryId = beautyId });
                beauty.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Fragrances", MainCategoryId = beautyId });
                beauty.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Tools & Accessories", MainCategoryId = beautyId });
                beauty.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Bath & Body", MainCategoryId = beautyId });
                beauty.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Health & Wellness", MainCategoryId = beautyId });
                mainCategories.Add(beauty);

                // Food
                var foodId = NextId();
                var food = new MainCategory
                {
                    Id = foodId,
                    Name = "Food",
                    Icon = "food.png",
                    BannerImage = "food_banner.png",
                    CategoryType = CategoryType.Product
                };
                food.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Local Foods", MainCategoryId = foodId });
                food.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Continental", MainCategoryId = foodId });
                food.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Drinks", MainCategoryId = foodId });
                food.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Pastries", MainCategoryId = foodId });
                food.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Snacks", MainCategoryId = foodId });
                mainCategories.Add(food);

                // Electronics
                var electronicsId = NextId();
                var electronics = new MainCategory
                {
                    Id = electronicsId,
                    Name = "Electronics",
                    Icon = "electronics.png",
                    BannerImage = "electronics_banner.png",
                    CategoryType = CategoryType.Product
                };
                electronics.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Mobile Phones", MainCategoryId = electronicsId });
                electronics.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Laptops", MainCategoryId = electronicsId });
                electronics.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Accessories", MainCategoryId = electronicsId });
                electronics.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Appliances", MainCategoryId = electronicsId });
                electronics.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Gaming", MainCategoryId = electronicsId });
                electronics.SubCategories.Add(new SubCategory { Id = NextId(), Name = "Tablets", MainCategoryId = electronicsId });
                mainCategories.Add(electronics);

                _mainCategories = mainCategories;
            }

            return _mainCategories;
        }

        public async ValueTask<IEnumerable<SubCategory>> GetAllSubCategoriesAsync()
        {
            var mainCategories = await GetAllMainCategoriesAsync();
            return mainCategories.SelectMany(mc => mc.SubCategories);
        }

        public async ValueTask<MainCategory?> GetMainCategoryByIdAsync(int id)
        {
            return (await GetAllMainCategoriesAsync()).FirstOrDefault(mc => mc.Id == id);
        }

        public async ValueTask<IEnumerable<SubCategory>> GetSubCategoriesByMainCategoryIdAsync(int mainCategoryId)
        {
            return (await GetAllMainCategoriesAsync())
                .FirstOrDefault(mc => mc.Id == mainCategoryId)?.SubCategories
                ?? Enumerable.Empty<SubCategory>();
        }
    }
}
