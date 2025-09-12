using MANDUU.ViewModels.CheckoutAndPayment;

namespace MANDUU.Views.MainPages.SubPages.CheckoutAndPayment;

public partial class CheckoutPage : ContentPage
{
	private readonly CheckoutPageViewModel _checkoutPageViewModel;
	public CheckoutPage(CheckoutPageViewModel checkoutPageViewModel)
	{
		InitializeComponent();
		_checkoutPageViewModel = checkoutPageViewModel;
		BindingContext = _checkoutPageViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		if (!_checkoutPageViewModel.IsInitialized)
		{
			await _checkoutPageViewModel.InitializeAsyncCommand.ExecuteAsync(null);
		}
    }
}