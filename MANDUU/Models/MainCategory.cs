using MANDUU.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public class MainCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string BannerImage { get; set; } = string.Empty;
        public CategoryType CategoryType { get; set; }

        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
