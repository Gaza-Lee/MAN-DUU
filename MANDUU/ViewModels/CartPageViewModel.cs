using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MANDUU.Services;
using MANDUU.Models;
using Microsoft.Maui.Controls;

namespace MANDUU.ViewModels
{
    public partial class CartPageViewModel : ObservableObject
    {
        private readonly CartService _cartService;

        [ObservableProperty]
        private ObservableCollection<CartItem> cartItems;

        [ObservableProperty]
        private decimal cartTotal;

        [ObservableProperty]
        private int cartItemCount;

        public CartPageViewModel(CartService cartService)
        {
            _cartService = cartService;
            _cartService.CartUpdated += OnCartUpdated;
            LoadCartItems();
        }

        private void OnCartUpdated(object sender, EventArgs e)
        {
            LoadCartItems();
        }

        private void LoadCartItems()
        {
            CartItems = _cartService.GetCartItems();
            CartTotal = _cartService.GetCartTotal();
            CartItemCount = _cartService.GetCartItemCount();
        }

        [RelayCommand]
        private async Task IncreaseQuantityAsync(CartItem item)
        {
            await _cartService.UpdateQuantityAsync(item.Id, item.Quantity + 1);
        }

        [RelayCommand]
        private async Task DecreaseQuantityAsync(CartItem item)
        {
            if (item.Quantity > 1)
            {
                await _cartService.UpdateQuantityAsync(item.Id, item.Quantity - 1);
            }
        }

        [RelayCommand]
        private async Task RemoveItemAsync(CartItem item)
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Remove Item",
                $"Are you sure you want to remove {item.Product.Name} from your cart?",
                "Yes, Remove",
                "Cancel"
            );

            if (confirm)
            {
                await _cartService.RemoveFromCartAsync(item.Id);
            }
        }

        [RelayCommand]
        private async Task ProceedToCheckoutAsync()
        {
            if (!_cartService.HasItems())
            {
                await Shell.Current.DisplayAlert("Cart Empty", "Your cart is empty. Add some items first.", "OK");
                return;
            }

            // Navigate to checkout page
            await Shell.Current.DisplayAlert("Checkout", "Proceeding to checkout...", "OK");
            // await Shell.Current.GoToAsync("//checkout");
        }

        public void Dispose()
        {
            _cartService.CartUpdated -= OnCartUpdated;
        }
    }
}