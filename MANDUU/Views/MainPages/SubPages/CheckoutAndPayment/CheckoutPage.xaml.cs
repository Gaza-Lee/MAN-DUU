using MANDUU.ViewModels.CheckoutAndPayment;

namespace MANDUU.Views.MainPages.SubPages.CheckoutAndPayment;

public partial class CheckoutPage : ContentPage
{
	public CheckoutPage(CheckoutPageViewModel checkoutPageViewModel)
	{
		InitializeComponent();
		BindingContext = checkoutPageViewModel;
	}
}