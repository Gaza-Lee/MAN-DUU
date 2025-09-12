using MANDUU.ViewModels.CheckoutAndPayment;

namespace MANDUU.Views.MainPages.SubPages.CheckoutAndPayment;

public partial class PaymentPage : ContentPage
{
	private readonly PaymentPageViewModel _paymentPageViewModel;
	public PaymentPage(PaymentPageViewModel paymentPageViewModel)
	{
		InitializeComponent();
		_paymentPageViewModel = paymentPageViewModel;
		BindingContext = _paymentPageViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		if (!_paymentPageViewModel.IsInitialized)
		{
			await _paymentPageViewModel.InitializeAsyncCommand.ExecuteAsync(null);
		}
    }
}