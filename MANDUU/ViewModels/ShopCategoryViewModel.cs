using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;

namespace MANDUU.ViewModels
{
    public partial class ShopCategoryViewModel : ObservableObject
    {
        private readonly ShopCategoryService _categoryService;
        private readonly ShopService _shopService;
        private readonly INavigationService _navigationService;
        private readonly CartService _cartService;

        [ObservableProperty]
        private bool _hasCartItems;

        [ObservableProperty]
        private ObservableCollection<ShopCategory> shopCategories = new();

        public ShopCategoryViewModel(ShopCategoryService categoryService, 
            ShopService shopService, 
            INavigationService navigationService,
            CartService cartService)
        {
            _categoryService = categoryService;
            _shopService = shopService;
            _navigationService = navigationService;
            _cartService = cartService;
        }

        [RelayCommand]
        private async Task GoToMyShopAsync()
        {
            await _navigationService.NavigateToAsync("myshoppage");
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
        private async Task GoToProfileAsync()
        {
            await _navigationService.NavigateToAsync("userprofilepage");
        }

        [RelayCommand]
        private async Task GoToCartAsync()
        {
            await _navigationService.NavigateToAsync("cartpage");
        }


        public async Task InitializeAsync()
        {
            HasCartItems = _cartService.HasItems();
            ShopCategories.Clear();
            var categories = await _categoryService.GetAllShopCategoriesAsync();

            foreach (var category in categories)
            {
                var shops = await _shopService.GetShopsByMainCategoryAsync(category.Id);

                if (shops.Any())
                {
                    category.Shops = shops.ToList();
                    ShopCategories.Add(category);
                }
            }
        }

        
    }
}