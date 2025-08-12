using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.Models;

namespace MANDUU.Services
{
    public class CategoryService
    {
        private IEnumerable<Category> _categories;
        private short _nextId = 1;

        private short NextId() => _nextId++;

        public async ValueTask<IEnumerable<Category>> GetCategoriesAsync()
        {
            if (_categories == null)
            {
                var categories = new List<Category>();

                // Fashion
                var fashionId = NextId();
                categories.Add(new Category(fashionId, "Fashion", "fashion.png", 0));
                categories.Add(new Category(NextId(), "Clothing", "", fashionId));
                categories.Add(new Category(NextId(), "FootWear", "", fashionId));
                categories.Add(new Category(NextId(), "Hats", "", fashionId));
                categories.Add(new Category(NextId(), "Accessories", "", fashionId));

                // Grocery
                var groceryId = NextId();
                categories.Add(new Category(groceryId, "Grocery", "grocery.png", 0));
                categories.Add(new Category(NextId(), "Fruits & Vegetables", "", groceryId));
                categories.Add(new Category(NextId(), "Dairy Products", "", groceryId));


                // Beauty
                var beautyId = NextId();
                categories.Add(new Category(beautyId, "Beauty", "beauty.png", 0));
                categories.Add(new Category(NextId(), "Makeup", "", beautyId));
                categories.Add(new Category(NextId(), "Hair Care", "", beautyId));
                categories.Add(new Category(NextId(), "Skin Care", "", beautyId));
                categories.Add(new Category(NextId(), "Nails", "", beautyId));
                categories.Add(new Category(NextId(), "Fragrances", "", beautyId));
                categories.Add(new Category(NextId(), "Tools & Accessories", "", beautyId));
                categories.Add(new Category(NextId(), "Bath & Body", "", beautyId));
                categories.Add(new Category(NextId(), "Health & Wellness", "", beautyId));

                // Food
                var foodId = NextId();
                categories.Add(new Category(foodId, "Food", "food.png", 0));
                categories.Add(new Category(NextId(), "Local Foods", "", foodId));
                categories.Add(new Category(NextId(), "Continental", "", foodId));
                categories.Add(new Category(NextId(), "Drinks", "", foodId));
                categories.Add(new Category(NextId(), "Pastries", "", foodId));
                categories.Add(new Category(NextId(), "Snacks", "", foodId));


                // Electronics
                var electronicsId = NextId();
                categories.Add(new Category(electronicsId, "Electronics", "electronics.png", 0));
                categories.Add(new Category(NextId(), "Mobile Phones", "", electronicsId));
                categories.Add(new Category(NextId(), "Laptops", "", electronicsId));
                categories.Add(new Category(NextId(), "Accessories", "", electronicsId));
                categories.Add(new Category(NextId(), "Appliances", "", electronicsId));
                categories.Add(new Category(NextId(), "Gaming", "", electronicsId));
                categories.Add(new Category(NextId(), "Tablets", "", electronicsId));

                _categories = categories;
            }

            return _categories;
        }

        public async ValueTask<IEnumerable<Category>> GetMainCategoriesAsync() =>
            (await GetCategoriesAsync()).Where(c => c.ParentId == 0);
    }
}