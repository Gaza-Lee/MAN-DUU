

using MANDUU.ViewModels;

namespace MANDUU.Views.AuthenticationPages;

public partial class CreateAccountPage : ContentPage
{
	public CreateAccountPage(CreateAccountPageViewModel _createAccountViewModel)
	{
		InitializeComponent();
		BindingContext = _createAccountViewModel;
    }
}