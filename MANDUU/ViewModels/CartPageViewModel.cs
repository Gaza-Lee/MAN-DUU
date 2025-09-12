using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MANDUU.Services;
using MANDUU.Models;
using Microsoft.Maui.Controls;
using MANDUU.ViewModels.Base;
using Microsoft.Maui.Controls.Handlers.Compatibility;

namespace MANDUU.ViewModels
{
    public partial class CartPageViewModel : BaseViewModel
    {
        private readonly CartService _cartService;

        [ObservableProperty]
        private ObservableCollection<CartItem> _cartItems;

        [ObservableProperty]
        private decimal _cartTotal;

        [ObservableProperty]
        private int _cartItemCount;

        public CartPageViewModel(CartService cartService, INavigationService navigationService)
            : base(navigationService)
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

            await NavigationService.NavigateToAsync("checkoutpage");
        }

        public void Dispose()
        {
            _cartService.CartUpdated -= OnCartUpdated;
        }
    }
}