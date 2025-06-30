using MANDUU.ViewModels;

namespace MANDUU.Views.AuthenticationPages;

public partial class NewPasswordPage : ContentPage
{
	public NewPasswordPage(NewPasswordPageViewModel _newPasswordViewModel)
	{
		InitializeComponent();
		BindingContext = _newPasswordViewModel;
	}
}