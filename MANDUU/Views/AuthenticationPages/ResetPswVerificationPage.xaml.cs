using MANDUU.ViewModels;

namespace MANDUU.Views.AuthenticationPages;

public partial class ResetPswVerificationPage : ContentPage
{
	public ResetPswVerificationPage(ResetPswVerificationViewModel _resetPswVerificationPage)
	{
		InitializeComponent();
		BindingContext = _resetPswVerificationPage;
	}
}