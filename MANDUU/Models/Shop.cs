using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.Enumeration;

namespace MANDUU.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string OwnerId { get; set; }
        public string? UniqueShopPin { get; set; }

        public string? Email { get; set; }
        public string PhoneNumber { get; set; }

        public string? Logo { get; set; }
        public string? CoverImage { get; set; }

        public string ShortLocation { get; set; }
        public string LongLocation { get; set; }

        public string ShopProfileImage { get; set; }

        // Relationship to ShopCategory
        public int MainCategoryId { get; set; }
        public ShopCategory MainCategory { get; set; }

        // List of additional categories the shop belongs to
        public List<ShopCategory> AdditionalCategories { get; set; } = new();
        public List<int> AdditionalCategoryIds { get; set; } = new List<int>();

        public CategoryType CategoryType { get; set; } = CategoryType.Shop;

        public List<int> Products { get; set; } = new List<int>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public bool CanSellInCategory(int categoryId)
        {
            return MainCategoryId == categoryId || AdditionalCategoryIds.Contains(categoryId);
        }
    }

}

