using MANDUU.ViewModels;

namespace MANDUU.Views.AuthenticationPages;

public partial class ResetPasswordPage : ContentPage
{
	public ResetPasswordPage(ResetPasswordPageViewModel _resetPasswordPageViewModel)
	{
		InitializeComponent();
		BindingContext = _resetPasswordPageViewModel;
	}
}