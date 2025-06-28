using MANDUU.ViewModels;
namespace MANDUU.Views.AuthenticationPages;

public partial class SignInPage : ContentPage
{
	public SignInPage(SignInViewModel signInViewModel)
	{
		InitializeComponent();
		BindingContext = signInViewModel;
    }
}