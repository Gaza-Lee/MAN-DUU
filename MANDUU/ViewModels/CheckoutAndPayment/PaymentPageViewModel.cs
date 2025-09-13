using CommunityToolkit.Mvvm.ComponentModel;
using MANDUU.Models;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using MANDUU.Enumeration;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Linq;

namespace MANDUU.ViewModels.CheckoutAndPayment
{
    public partial class PaymentPageViewModel : BaseViewModel
    {
        private readonly CartService _cartService;

        [ObservableProperty]
        private int selectedPaymentMethodIndex = 0; // Default: MobileMoney = 0

        // Derived property 
        public PaymentMethod SelectedPaymentMethod => (PaymentMethod)SelectedPaymentMethodIndex;

        [ObservableProperty]
        private string cartItemsNames = string.Empty;

        [ObservableProperty]
        private decimal cartTotal;

        [ObservableProperty]
        private decimal deliveryFee = 20m;

        [ObservableProperty]
        private int orderNumber = 35; //

        public PaymentPageViewModel(CartService cartService,
            INavigationService navigationService) : base(navigationService)
        {
            _cartService = cartService;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            var cartItems = _cartService.GetCartItems();
            CartItemsNames = string.Join(", ",
                cartItems.Select(item => item.Product?.Name)
                         .Where(name => !string.IsNullOrWhiteSpace(name)));
            CartTotal = _cartService.GetCartTotal();
        }
    }
}