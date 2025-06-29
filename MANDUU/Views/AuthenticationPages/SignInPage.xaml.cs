using MANDUU.ViewModels;
namespace MANDUU.Views.AuthenticationPages;

public partial class SignInPage : ContentPage
{
	public SignInPage(SignInPageViewModel signInViewModel)
	{
		InitializeComponent();
		BindingContext = signInViewModel;
    }
}