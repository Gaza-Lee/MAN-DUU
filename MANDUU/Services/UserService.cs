using MANDUU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users;
        private readonly ShopService _shopService;

        public UserService(ShopService shopService)
        {
            _shopService = shopService;
            _users = InitializeUsersWithShops();
        }

        private List<User> InitializeUsersWithShops()
        {
            // Get all shops first
            var allShops = _shopService.GetAllShopsAsync().Result.ToList();

            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "userone@gmail.com",
                    PhoneNumber = "+1234567890",
                    HashPassword = "Password1!",
                    FirstName = "User",
                    LastName = "One",
                    ProfilePicture = "profile_picture.jpg",
                    Shops = allShops.Where(s => s.Id == 3 || s.Id == 4).ToList() // FashionFix (id:3) and Emart (id:4)
                },
                new User
                {
                    Id = 2,
                    Email = "usertwo@gmail.com",
                    PhoneNumber = "+0987654321",
                    HashPassword = "password2!",
                    FirstName = "User",
                    LastName = "Two",
                    Shops = allShops.Where(s => s.Id == 6).ToList() // TechGadgets Hub (id:6)
                }
            };
        }

        public async Task<bool> IsUserAuthenticatedAsync()
        {
            await Task.Delay(100);
            var userId = Preferences.Default.Get("CurrentUserId", -1);
            return userId != -1;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var userId = Preferences.Default.Get("CurrentUserId", -1);
            if (userId == -1)
            {
                return null;
            }
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public async Task<bool> LoginAsync(string emailOrPhone, string password)
        {
            await Task.Delay(100); // Simulate async operation

            // Try to find user by email
            var userByEmail = _users.FirstOrDefault(u =>
                u.Email.Equals(emailOrPhone, StringComparison.OrdinalIgnoreCase) &&
                u.HashPassword == password);

            if (userByEmail != null)
            {
                Preferences.Default.Set("CurrentUserId", userByEmail.Id);
                return true;
            }

            // Try to find user by phone number (remove any non-digit characters first)
            var cleanPhone = new string(emailOrPhone.Where(char.IsDigit).ToArray());
            var userByPhone = _users.FirstOrDefault(u =>
                u.PhoneNumber != null &&
                new string(u.PhoneNumber.Where(char.IsDigit).ToArray()) == cleanPhone &&
                u.HashPassword == password);

            if (userByPhone != null)
            {
                Preferences.Default.Set("CurrentUserId", userByPhone.Id);
                return true;
            }

            return false;
        }


        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            await Task.Delay(100); // Simulate async operation

            // Check if email already exists
            if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // Email already registered
            }

            // Check if phone number already exists
            var cleanPhone = phoneNumber; // Keep the format as provided
            if (_users.Any(u => u.PhoneNumber != null && u.PhoneNumber.Equals(cleanPhone, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // Phone number already registered
            }

            // Create new user
            var newUser = new User
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1, // Add user to the end of the list
                Email = email,
                PhoneNumber = phoneNumber,
                HashPassword = password, // Should be hashed in Production
                FirstName = firstName,
                LastName = lastName,
                ProfilePicture = null, // Can be set later
                Shops = new List<Shop>() // New user starts with no shops
            };

            // Add user to the list of users
            _users.Add(newUser);

            //automatically log in the user after registration
            Preferences.Default.Set("CurrentUserId", newUser.Id);

            return true;
        }

        //check if email or phone exists
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            await Task.Delay(10);
            return _users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> IsPhoneExistsAsync(string phoneNumber)
        {
            await Task.Delay(10);
            var cleanPhone = new string(phoneNumber.Where(char.IsDigit).ToArray());
            return _users.Any(u => u.PhoneNumber != null &&
                new string(u.PhoneNumber.Where(char.IsDigit).ToArray()) == cleanPhone);
        }

        // Logout
        public async Task<bool> LogoutAsync()
        {
            Preferences.Default.Remove("CurrentUserId");
            return true;
        }

        // Shops belonging to current User
        public async Task<List<Shop>> GetShopsByUserAsync(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            return user?.Shops ?? new List<Shop>();
        }

        //if user owns a specific shop
        public async Task<bool> DoesUserOwnShopAsync(int userId, int shopId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            return user?.Shops.Any(s => s.Id == shopId) ?? false;
        }

        //Add shop to user's ownership
        public async Task<bool> AddShopToUserAsync(int userId, Shop shop)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null && !user.Shops.Any(s => s.Id == shop.Id))
            {
                user.Shops.Add(shop);
                return true;
            }
            return false;
        }
    }
}