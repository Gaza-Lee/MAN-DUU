using MANDUU.ViewModels;

namespace MANDUU.Views.AuthenticationPages;

public partial class CreateAccountOrSignInPage : ContentPage
{
	public CreateAccountOrSignInPage(CreateAccountOrSignInPageViewModel createAccountOrSignInPageViewModel)
	{
		InitializeComponent();
		BindingContext = createAccountOrSignInPageViewModel;
	}
}