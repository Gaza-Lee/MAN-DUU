using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models.PaymentModels;
using MANDUU.RegexValidation;
using MANDUU.Services;
using MANDUU.Services.PaymentService;
using MANDUU.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MANDUU.ViewModels.CheckoutAndPayment
{
    public partial class MobileMoneyViewModel : BaseViewModel
    {
        private readonly CartService _cartService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;

        [ObservableProperty]
        private ObservableCollection<string> _networkProviders = new()
        {
            "MTN Mobile Money",
            "Vodafone Cash",
            "AirtelTigo Money"
        };

        [ObservableProperty]
        private string _phoneNumber;

        [ObservableProperty]
        private string _selectedProvider;

        [ObservableProperty]
        private decimal _overallTotal;


        public MobileMoneyViewModel(
            INavigationService navigationService,
            CartService cartService,
            IUserService userService,
            IPaymentService paymentService) : base(navigationService)
        {
            _cartService = cartService;
            _userService = userService;
            _paymentService = paymentService;
        }

        private bool CanMakePayment()
        {
            return !string.IsNullOrWhiteSpace(PhoneNumber) &&
                   !string.IsNullOrWhiteSpace(SelectedProvider);
        }

        [RelayCommand]
        private async Task MakePaymentAsync()
        {
            if (!CanMakePayment())
            {
                ShowToast("Phone and provider required");
                return;
            }

            if (!InputValidation.IsValidPhoneNumber(PhoneNumber))
            {
                ShowToast("Enter a valid Phone number");
                return;
            }

            var confirm = await Shell.Current.DisplayAlert(
                "Confirm Payment",
                $"Pay {OverallTotal:C} with {SelectedProvider}?",
                "Confirm", "Cancel");

            if (!confirm) return;

            try
            {
                await IsBusyFor(async () =>
                {
                    var user = await _userService.GetCurrentUserAsync();
                    var userEmail = user?.Email;
                    if (string.IsNullOrEmpty(userEmail))
                    {
                        ShowToast("No valid user email found.");
                        return;
                    }

                    var result = await _paymentService.InitializeMobileMoneyPayment(
                        OverallTotal,
                        userEmail,
                        FormatPhoneNumber(PhoneNumber),
                        MapProviderCode(SelectedProvider)
                    );

                    if (result?.Status == true && result.Data != null)
                    {
                        Debug.WriteLine($"Payment initialized. Ref: {result.Data.Reference}");

                        // ✅ Real UX: open authorization URL or instruct provider
                        ShowToast("Please approve payment in your mobile money app.");

                        await _cartService.ClearCartAsync();
                        await NavigationService.NavigateToAsync("//main/home");
                    }
                    else
                    {
                        ShowToast(result?.Message ?? "Payment failed to start");
                    }
                });
            }
            catch (PaystackApiException ex)
            {
                Debug.WriteLine(ex.Message);
                ShowToast("Payment request failed. Try again.");
            }
        }

        // Normalize Ghana numbers → 233XXXXXXXXX
        private string FormatPhoneNumber(string phone)
        {
            phone = phone.Trim().Replace(" ", "");
            if (phone.StartsWith("+233")) return phone[1..];
            if (phone.StartsWith("0")) return "233" + phone[1..];
            if (phone.StartsWith("233")) return phone;
            return "233" + phone; // If user typed without zero
        }
        // Map human names to provider codes understood by Paystack.
        private string MapProviderCode(string providerName)
        {
            return providerName switch
            {
                "MTN Mobile Money" => "mtn",
                "Vodafone Cash" => "vod",
                "AirtelTigo Money" => "atl", 
                _ => "mtn"
            };
        }
    }
}