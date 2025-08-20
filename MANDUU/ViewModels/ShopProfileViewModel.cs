using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MANDUU.ViewModels
{
    public partial class ShopProfileViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ShopService _shopService;
        private readonly ProductService _productService;

        [ObservableProperty] private Shop shop;
        [ObservableProperty] private ObservableCollection<Product> shopProducts = new();

        public ShopProfileViewModel(ShopService shopService, ProductService productService)
        {
            _shopService = shopService;
            _productService = productService;
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
