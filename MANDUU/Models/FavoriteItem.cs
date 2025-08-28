using System;
using System.Collections.Generic;
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
    }
}
