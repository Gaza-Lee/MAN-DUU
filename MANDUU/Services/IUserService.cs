using MANDUU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public interface IUserService
    {
        Task<bool> IsUserAuthenticatedAsync();
        Task<User> GetCurrentUserAsync();
        Task<bool> LoginAsync(string emailOrPhone, string password);
        Task<bool> LogoutAsync();

        Task<List<Shop>> GetShopsByUserAsync(int userId);
        Task<bool> DoesUserOwnShopAsync(int userId, int shopId);
        Task<bool> AddShopToUserAsync(int userId, Shop shop);
        Task<bool> RegisterAsync(string firstName, string lastName, string email, string phoneNumber, string password);
    }
}