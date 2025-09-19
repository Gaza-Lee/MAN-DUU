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
            var allShops = _shopService.GetAllShopsAsync().Result.ToList();

            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "bernardobenganim@gmail.com",
                    PhoneNumber = "+1234567890",
                    HashPassword = "Password1!",
                    FirstName = "User",
                    LastName = "One",
                    ProfilePicture = "profile_picture.jpg",
                    IsFaceVerified = true,
                    VerificationStatus = "Completed",
                    VerificationDate = DateTime.UtcNow.AddDays(-1),
                    Shops = allShops.Where(s => s.Id == 3 || s.Id == 4).ToList()
                },
                new User
                {
                    Id = 2,
                    Email = "usertwo@gmail.com",
                    PhoneNumber = "+0987654321",
                    HashPassword = "password2!",
                    FirstName = "User",
                    LastName = "Two",
                    IsFaceVerified = false,
                    VerificationStatus = "Pending",
                    Shops = allShops.Where(s => s.Id == 6).ToList()
                }
            };
        }

        public async Task<bool> UpdateFaceVerificationStatusAsync(int userId, bool isVerified, string status)
        {
            await Task.Delay(100);

            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.IsFaceVerified = isVerified;
                user.VerificationStatus = status;
                user.VerificationDate = isVerified ? DateTime.UtcNow : null;
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            await Task.Delay(100);

            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                // Update all properties
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.IsFaceVerified = user.IsFaceVerified;
                existingUser.VerificationStatus = user.VerificationStatus;
                existingUser.VerificationDate = user.VerificationDate;
                existingUser.IsSeller = user.IsSeller;
                existingUser.ProfilePicture = user.ProfilePicture;

                return true;
            }

            return false;
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
            await Task.Delay(100);

            var userByEmail = _users.FirstOrDefault(u =>
                u.Email.Equals(emailOrPhone, StringComparison.OrdinalIgnoreCase) &&
                u.HashPassword == password);

            if (userByEmail != null)
            {
                Preferences.Default.Set("CurrentUserId", userByEmail.Id);
                return true;
            }

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
            await Task.Delay(100);

            if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            var cleanPhone = phoneNumber;
            if (_users.Any(u => u.PhoneNumber != null && u.PhoneNumber.Equals(cleanPhone, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            var newUser = new User
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                Email = email,
                PhoneNumber = phoneNumber,
                HashPassword = password,
                FirstName = firstName,
                LastName = lastName,
                ProfilePicture = null,
                IsFaceVerified = false,
                VerificationStatus = "Pending",
                Shops = new List<Shop>()
            };

            _users.Add(newUser);
            Preferences.Default.Set("CurrentUserId", newUser.Id);

            return true;
        }

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

        public async Task<bool> LogoutAsync()
        {
            Preferences.Default.Remove("CurrentUserId");
            return true;
        }

        public async Task<List<Shop>> GetShopsByUserAsync(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            return user?.Shops ?? new List<Shop>();
        }

        public async Task<bool> DoesUserOwnShopAsync(int userId, int shopId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            return user?.Shops.Any(s => s.Id == shopId) ?? false;
        }

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