using CommunityToolkit.Mvvm.ComponentModel;
using MANDUU.Models;
using MANDUU.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.ViewModels.Base;

namespace MANDUU.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        private readonly ShopService _shopService;


        [ObservableProperty]
        private ObservableCollection<Product> _shopProducts = new();

        [ObservableProperty]
        private Shop _shop;

        public DashboardViewModel(ProductService productService, ShopService shopService, INavigationService navigationService) : base(navigationService)
        {
            _productService = productService;
            _shopService = shopService;
        }
        public override async void ApplyQueryAttributes(IDictionary<string, object> query)
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
