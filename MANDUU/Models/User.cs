using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public class User
    {
        
        public int Id { get; set; }

        // Basic Info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Seller Info
        public bool IsSeller { get; set; } = false;// By default every user is a buyer and can become a seller later

        // Authentication
        public string HashPassword { get; set; }

        // Profile
        public string? ProfilePicture { get; set; }

        // Favorites (IDs for now, can be navigation later)
        public List<int> FavoriteProducts { get; set; } = new List<int>();
        public List<int> FavoriteShops { get; set; } = new List<int>();

        // Shops owned by the user
        public List<Shop> Shops { get; set; } = new List<Shop>();
    }
}
