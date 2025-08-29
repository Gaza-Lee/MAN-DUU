using CommunityToolkit.Mvvm.ComponentModel;
using MANDUU.Models;
using MANDUU.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels
{
    public partial class DashboardViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ProductService _productService;
        private readonly ShopService _shopService;
        private readonly INavigationService _navigationService;


        [ObservableProperty]
        private ObservableCollection<Product> _shopProducts = new();

        [ObservableProperty]
        private Shop _shop;

        public DashboardViewModel(ProductService productService, ShopService shopService, INavigationService navigationService)
        {
            _productService = productService;
            _shopService = shopService;
            _navigationService = navigationService;
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ShopId", out var id) && id is int shopId)
            {
                await LoadShopAsync(shopId);
            }
        }

        private async Task LoadShopAsync(int shopId)
        {
            Shop = await _shopService.GetShopByIdAsync(shopId);

            if (Shop != null)
            {
                var products = await _productService.GetProductsByShopAsync(shopId);
                ShopProducts.Clear();
                foreach (var product in products)
                    ShopProducts.Add(product);
            }
        }
    }
}
