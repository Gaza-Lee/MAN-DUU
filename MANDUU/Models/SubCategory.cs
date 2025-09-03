using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainCategoryId { get; set; }
        public MainCategory? MainCategory { get; set; }

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
    }
}
