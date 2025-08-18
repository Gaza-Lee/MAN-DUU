using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public class Product
    {
            public int Id { get; set; }

            public string Name { get; set; }
            public string Description { get; set; }

            public string ImageUrl { get; set; }
            public List<string> Gallery { get; set; } = new List<string>();

            public decimal Price { get; set; }
            public int TotalSold { get; set; }

            public int MainCategoryId { get; set; }
            public MainCategory? MainCategory { get; set; }

            public int SubCategoryId { get; set; }
            public SubCategory SubCategory { get; set; }

            // Shop that owns the product
            public int ShopId { get; set; }
            public Shop? Shop { get; set; }


            public string FormattedPrice => $"₵{Price:N2}";
    }
}
