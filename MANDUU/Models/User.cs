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
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        // Seller Info
        public bool IsSeller { get; set; } = false;

        // Authentication
        public string HashPassword { get; set; } = string.Empty;
        public bool IsVerified { get; set; }

        // Face Verification
        public bool IsFaceVerified { get; set; }
        public string VerificationStatus { get; set; } = "Pending";
        public DateTime? VerificationDate { get; set; }

        // Profile
        public string? ProfilePicture { get; set; }

        // Favorites 
        public List<FavoriteItem> FavoriteProducts { get; set; } = new List<FavoriteItem>();
        public List<FavoriteItem> FavoriteShops { get; set; } = new List<FavoriteItem>();

        // Shops owned by the user
        public List<Shop> Shops { get; set; } = new List<Shop>();
    }
}