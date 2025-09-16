using CommunityToolkit.Mvvm.ComponentModel;
using MANDUU.Models;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using MANDUU.Enumeration;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Linq;
using System.Collections.ObjectModel;

namespace MANDUU.ViewModels.CheckoutAndPayment
{
    public partial class PaymentPageViewModel : BaseViewModel
    {
        private readonly CartService _cartService;
        public MobileMoneyViewModel _mobileMoneyPaymentVM { get; }

        [ObservableProperty]
        private int selectedPaymentMethodIndex = 0; // Default: MobileMoney = 0

        // Derived property 
        public PaymentMethod SelectedPaymentMethod => (PaymentMethod)SelectedPaymentMethodIndex;

        [ObservableProperty]
        private string cartItemsNames = string.Empty;

        [ObservableProperty]
        private decimal _overallTotal;

        [ObservableProperty]
        private decimal deliveryFee = 20m;

        [ObservableProperty]
        private int orderNumber = 35; //

        public PaymentPageViewModel(CartService cartService,
            INavigationService navigationService,
            MobileMoneyViewModel mobileMoneyPaymentVM) : base(navigationService)
        {
            _cartService = cartService;
            _mobileMoneyPaymentVM = mobileMoneyPaymentVM;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            var cartItems = _cartService.GetCartItems();
            CartItemsNames = string.Join(", ",
                cartItems.Select(item => item.Product?.Name)
                         .Where(name => !string.IsNullOrWhiteSpace(name)));
            var cartTotal = _cartService.GetCartTotal();
            OverallTotal = cartTotal + DeliveryFee;
        }  
    }
}