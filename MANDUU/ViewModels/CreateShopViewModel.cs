using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.RegexValidation;
using MANDUU.Models;

namespace MANDUU.ViewModels
{
    public partial class CreateShopViewModel : BaseViewModel
    {
        private readonly ShopService _shopService;
        private readonly ShopCategoryService _shopCategoryService;
        private readonly IUserService _userService;

        [ObservableProperty]
        private string _shopName;

        [ObservableProperty]
        private string _shopEmail;

        [ObservableProperty]
        private string _shortLocation;

        [ObservableProperty]
        private string _shopPhoneNumber;

        [ObservableProperty]
        private string _shopDescription;

        [ObservableProperty]
        private string _shopPassword;

        [ObservableProperty]
        private string _longLocation;

        [ObservableProperty]
        private ObservableCollection<ShopCategory> _availableShopCategories = new();

        [ObservableProperty]
        private ShopCategory _selectedShopMainCategory;

        [ObservableProperty]
        private string _locationButtonText = "LOCATE";



        public CreateShopViewModel(
            ShopService shopService, 
            ShopCategoryService shopCategoryService,
            IUserService userService,
            INavigationService navigationService): base (navigationService)
        {
            _shopService = shopService;
            _shopCategoryService = shopCategoryService;
            _userService = userService;
        }

        [RelayCommand]
        public async Task LocationOnMapAsync()
        {
            // request location from map and auto fill with the link
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    ShowToast("Location permission is required to set your shop location.");
                    return;
                }
            }

            // Get current location
            var request = new GeolocationRequest(
                GeolocationAccuracy.Best,
                TimeSpan.FromSeconds(10));

            var location = await Geolocation.Default.GetLocationAsync(request);
            if (location == null)
            {
                ShowToast("Unable to get location. Please try again.");
                return;
            }

            //Generate Google Maps link
            LongLocation = $"https://www.google.com/maps/search/?api=1&query={location.Latitude},{location.Longitude}";
            LocationButtonText = "CAPTURED";
        }
        private bool CanCreateShop()
        {
            return !string.IsNullOrWhiteSpace(ShopName) &&
                   !string.IsNullOrWhiteSpace(ShopEmail) &&
                   !string.IsNullOrWhiteSpace(ShortLocation) &&
                   !string.IsNullOrWhiteSpace(ShopPhoneNumber) &&
                   !string.IsNullOrWhiteSpace(ShopDescription) &&
                   !string.IsNullOrWhiteSpace(ShopPassword) &&
                   !string.IsNullOrWhiteSpace(LongLocation) &&
                   SelectedShopMainCategory != null;
        }

        [RelayCommand]
        public async Task CreateShopAsync()
        {
            if (!CanCreateShop())
            {
               ShowToast(" All fields are required.");
                return;
            }

            if (!InputValidation.IsValidEmail(ShopEmail))
            {
                ShowToast("Email is invalid");
                return;
            }
            if (!InputValidation.IsValidPhoneNumber(ShopPhoneNumber))
            {
                ShowToast("Phone number is invalid");
                return;
            }
            if (!InputValidation.IsValidPassword(ShopPassword))
            {
                ShowToast("Password is weak");
                return;
            }
            var currentUser = await _userService.GetCurrentUserAsync();

            var newShop = new Shop
            {
                Name = ShopName.Trim(),
                Email = ShopEmail.Trim(),
                ShortLocation = ShortLocation.Trim(),
                PhoneNumber = ShopPhoneNumber.Trim(),
                Description = ShopDescription.Trim(),
                UniqueShopPin = ShopPassword.Trim(),
                MainCategoryId = SelectedShopMainCategory.Id,
                MainCategory = new ShopCategory
                {
                    Id = SelectedShopMainCategory.Id,
                    Name = SelectedShopMainCategory.Name,

                },
                LongLocation = LongLocation.Trim(),
                OwnerId = currentUser.Id,
                OwnerName = currentUser.FullName,
                CreatedAt = DateTime.UtcNow
            };

            await _shopService.AddShopAsync(newShop);

            ShowToast("Shop created successfully!");

            await NavigationService.NavigateToAsync("myshoppage");
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await LoadShopCategoriesAsync();
        }

        private async Task LoadShopCategoriesAsync()
        {
            var shopCategories = await _shopCategoryService.GetAllShopCategoriesAsync();
            AvailableShopCategories = new ObservableCollection<ShopCategory>(shopCategories);
        }
    }
}
