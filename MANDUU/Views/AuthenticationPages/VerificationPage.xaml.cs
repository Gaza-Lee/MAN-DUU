using MANDUU.ViewModels;

namespace MANDUU.Views.AuthenticationPages;

public partial class VerificationPage : ContentPage
{
	public VerificationPage(VerificationPageViewModel _verificationPageViewModel)
	{
		InitializeComponent();
		BindingContext = _verificationPageViewModel;
    }
}