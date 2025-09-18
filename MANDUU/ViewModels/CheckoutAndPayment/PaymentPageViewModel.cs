using CommunityToolkit.Mvvm.ComponentModel;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using MANDUU.Enumeration;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MANDUU.ViewModels.CheckoutAndPayment
{
    public partial class PaymentPageViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly CartService _cartService;
        public MobileMoneyViewModel MobileMoneyVM { get; }

        [ObservableProperty]
        private int selectedPaymentMethodIndex = 0; // Default Mobile Money

        public PaymentMethod SelectedPaymentMethod => (PaymentMethod)SelectedPaymentMethodIndex;

        [ObservableProperty]
        private string _cartItemsNames = string.Empty;

        [ObservableProperty]
        private decimal _deliveryFee = 20m;

        [ObservableProperty]
        private int _orderNumber = 35;

        [ObservableProperty]
        private decimal _overallTotal;

        [ObservableProperty]
        private string _residentialAddress;

        public PaymentPageViewModel(
            CartService cartService,
            INavigationService navigationService,
            MobileMoneyViewModel mobileMoneyPaymentVM) : base(navigationService)
        {
            _cartService = cartService;
            MobileMoneyVM = mobileMoneyPaymentVM;
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

            // Inject total into mobile money VM
            MobileMoneyVM.OverallTotal = OverallTotal;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Address", out var addressObj) && addressObj is string address)
            {
                ResidentialAddress = address;
            }
        }
    }
}