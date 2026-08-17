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
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Offer ShopAd { get; set; } = null!; //Shop Advertisement(could later be multiple ads) so might need to be a list later

        // Owner Info
        public int OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string? UniqueShopPin { get; set; }


        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Logo { get; set; }
        public string? CoverImage { get; set; }

        public string ShortLocation { get; set; } = string.Empty;
        public string LongLocation { get; set; } = string.Empty;

        public string ShopProfileImage { get; set; } = string.Empty;

        public int MainCategoryId { get; set; }
        public ShopCategory MainCategory { get; set; } = null!; // The primary category that the shop belongs to

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

