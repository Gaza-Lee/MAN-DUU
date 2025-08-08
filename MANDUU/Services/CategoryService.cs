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
        public async ValueTask<IEnumerable<Category>> GetCategoriesAsync()
        {
            if (_categories == null)
            {
                var Categories = new List<Category>();

                var Fashion = new List<Category>
                {
                    new(1, "Fashion", "fashion.png",0),
                    new(2, "Clothing", "",1 ),
                    new(3, "FootWear","", 2),
                    new(4, "Hats","",3),
                    new(5, "Accesories","",4)
                };
                Categories.AddRange(Fashion);

                var Grocery = new List<Category>
                {
                    new(6,"Grocery","grocery.png",0),
                };
                Categories.AddRange(Grocery);

                var Beauty = new List<Category>
                {
                    new Category(7,"Beauty", "beauty.png", 0),
                };
                Categories.AddRange(Beauty);

                var Food = new List<Category>
                {
                    new(8,"Food", "food.png",0)
                };
                Categories.AddRange(Food);

                var Electronics = new List<Category>
                {
                    new(9,"Electronics","electronics.png",0)
                };
                Categories.AddRange(Electronics);

                _categories = Categories;
            }
            
            return _categories;
        }
        public async ValueTask<IEnumerable<Category>> GetMainCategoriesAsync() =>
            (await GetCategoriesAsync()).Where(c => c.ParentId == 0);
    }
}