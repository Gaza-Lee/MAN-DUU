using System;
using System.Collections.Generic;

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

        // Foreign Keys
        public int MainCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int ShopId { get; set; }

        // Navigation Properties
        public MainCategory? MainCategory { get; set; }
        public SubCategory? SubCategory { get; set; }
        public Shop Shop { get; set; }

        // Denormalized fields for quick access
        public string ShopName { get; set; }
        public string ShopLocation { get; set; }

        // Timestamps
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }




        // Computed Properties

        public string FormattedPrice => $"₵{Price:N2}";

        public string FormattedDate => CreatedDate.ToString("dd/MM/yyyy");
        public string FormattedLastUpdated => LastUpdated.ToString("HH:mm");
    }
}