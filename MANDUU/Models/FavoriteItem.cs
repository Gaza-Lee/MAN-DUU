using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public class FavoriteItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Shop Shop { get; set; }
        public bool IsProduct => Product != null;
        public bool IsShop => Shop != null;
        public DateTime AddedDate { get; set; } = DateTime.Now;




        public string DisplayName => IsProduct ? Product.Name : IsShop ? Shop.Name : "Unknown";

        public string AddedDateFormatted =>
            AddedDate.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);

        public string AddedTimeFormatted =>
            AddedDate.ToString("h:mm tt", CultureInfo.InvariantCulture);

        public string? ProductPrice
        {
            get
            {
                if (IsProduct && !string.IsNullOrEmpty(Product.FormattedPrice))
                    return Product.FormattedPrice;
                return Product.FormattedPrice;
            }
        }
        public string? ImageUrl
        {
            get
            {
                if (IsProduct && !string.IsNullOrEmpty(Product.ImageUrl))
                    return Product.ImageUrl;

                if (IsShop && !string.IsNullOrEmpty(Shop.ShopProfileImage))
                    return Shop.ShopProfileImage;

                return "placeholder_image.png"; // fallback
            }
        }
    }
}
