using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.Enumeration;
using MANDUU.Services;

namespace MANDUU.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Offer ShopAd { get; set; } //Shop Advertisement(could later be multiple ads) so might need to be a list later

        // Owner Info
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string? UniqueShopPin { get; set; }


        public string? Email { get; set; }
        public string PhoneNumber { get; set; }

        public string? Logo { get; set; }
        public string? CoverImage { get; set; }

        public string ShortLocation { get; set; }
        public string LongLocation { get; set; }

        public string ShopProfileImage { get; set; }

        public int MainCategoryId { get; set; }
        public ShopCategory MainCategory { get; set; } // The primary category that the shop belongs to

        // Other main categories that the shop can belong to (for shops that sell multiple types of products)
        public List<ShopCategory> AdditionalCategories { get; set; } = new();
        public List<int> AdditionalCategoryIds { get; set; } = new List<int>();

        public CategoryType CategoryType { get; set; } = CategoryType.Shop;

        public List<int> Products { get; set; } = new List<int>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public bool IsVerified { get; set; } = false;



        public bool CanSellInCategory(int categoryId)
        {
            return MainCategoryId == categoryId || AdditionalCategoryIds.Contains(categoryId);
        }
    }

}

