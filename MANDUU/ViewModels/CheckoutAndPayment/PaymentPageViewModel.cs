using CommunityToolkit.Mvvm.ComponentModel;
using MANDUU.Models;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using MANDUU.Enumeration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace MANDUU.ViewModels.CheckoutAndPayment
{
    public partial class PaymentPageViewModel: BaseViewModel
    {
        private readonly CartService _cartService;

        [ObservableProperty]
        private PaymentMethod _selectedPaymentMethod = PaymentMethod.MobileMoney; //Default

        [ObservableProperty]
        private string _cartItemsNames;

        [ObservableProperty]
        private decimal _cartTotal;

        [ObservableProperty]
        private decimal _deliveryFee = 20m; //For now set to 20

        [ObservableProperty]
        private int _orderNumber = 00035; // set to value for now

        public PaymentPageViewModel(CartService cartService,
            INavigationService navigationService) : base(navigationService)
        {
            _cartService = cartService;
        }

        [RelayCommand]
        private void SelectedPaymentMethodAsync(PaymentMethod method)
        {
            Debug.WriteLine($"Tapped: {method}");
            SelectedPaymentMethod = method;
        }
        public override async Task InitializeAsync()
        {
           await base.InitializeAsync();
            var cartItems = _cartService.GetCartItems();
            CartItemsNames = string.Join(",",
                cartItems.Select(item => item.Product?.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                );
            CartTotal = _cartService.GetCartTotal();
        }

    }
}
