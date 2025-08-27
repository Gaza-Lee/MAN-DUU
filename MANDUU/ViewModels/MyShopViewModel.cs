using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public partial class MyShopViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        private readonly ShopService _shopService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Shop> _userShops = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _hasShops;

        [ObservableProperty]
        private string _userName;

        public MyShopViewModel(IUserService userService, ShopService shopService, INavigationService navigationService)
        {
            _userService = userService;
            _shopService = shopService;
            _navigationService = navigationService;
        }

        public async Task InitializeAsync()
        {
            await LoadUserShopsAsync();
        }

        private async Task LoadUserShopsAsync()
        {
            try
            {
                IsLoading = true;
                UserShops.Clear();

                // Get current user
                var currentUser = await _userService.GetCurrentUserAsync();

                if (currentUser == null)
                {
                    // User not logged in, handle accordingly
                    HasShops = false;
                    UserName = "Guest";
                    return;
                }

                UserName = currentUser.FullName;

                // Get shops owned by the current user
                var userShops = await _userService.GetShopsByUserAsync(currentUser.Id);

                foreach (var shop in userShops)
                {
                    UserShops.Add(shop);
                }

                HasShops = UserShops.Any();
            }
            catch (Exception ex)
            {
                // Handle error
                System.Diagnostics.Debug.WriteLine($"Error loading shops: {ex.Message}");
                HasShops = false;
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task CreateNewShopAsync()
        {
            // Navigate to create shop page
            await Shell.Current.GoToAsync("createshoppage");
        }


        [RelayCommand]
        private async Task SelectedShopAsync(Shop shop)
        {
            if (shop == null) return;

            await _navigationService.NavigateToAsync("shopprofilepage", new Dictionary<string, object>
            {
                { "ShopId", shop.Id },
                { "ShopName", shop.Name }
            });
        }

        [RelayCommand]
        private async Task GoToDashboardAsync(Shop shop)
        {
            // Navigate to dashboard page
            await _navigationService.NavigateToAsync("dashboardpage", new Dictionary<string, object>
            {
                {"ShopId", shop.Id },
                {"ShopName", shop.Name   }
            });
        }
    }
}