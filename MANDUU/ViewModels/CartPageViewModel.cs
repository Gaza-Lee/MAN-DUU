using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.Services;
using MANDUU.Models;

namespace MANDUU.ViewModels
{
    public partial class CartPageViewModel : ObservableObject
    {
        private readonly CartService _cartService;

        [ObservableProperty]
        private ObservableCollection<CartItem> cartItems;

        [ObservableProperty]
        private decimal cartTotal;

        public CartPageViewModel(CartService cartService)
        {
            _cartService = cartService;
            LoadCartItems();
        }

        private void LoadCartItems()
        {
            CartItems = _cartService.GetCartItems();
            CartTotal = _cartService.GetCartTotal();
        }

        [RelayCommand]
        private async Task RemoveItemAsync(CartItem item)
        {
            await _cartService.RemoveFromCartAsync(item.Id);
            LoadCartItems(); // Refresh
        }

        
    }
}
