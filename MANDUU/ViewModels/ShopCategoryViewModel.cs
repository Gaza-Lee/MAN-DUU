using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace MANDUU.ViewModels
{
    public partial class ShopCategoryViewModel : ObservableObject
    {
        private readonly ShopCategoryService _categoryService;
        private readonly ShopService _shopService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<ShopCategory> shopCategories = new();

        public ShopCategoryViewModel(ShopCategoryService categoryService, ShopService shopService, INavigationService navigationService)
        {
            _categoryService = categoryService;
            _shopService = shopService;
            _navigationService = navigationService;
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

        
        public async Task InitializeAsync()
        {
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