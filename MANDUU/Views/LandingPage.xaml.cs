using MANDUU.ViewModels;

namespace MANDUU.Views;

public partial class LandingPage : ContentPage
{
	public LandingPage(LandingPageViewModel _landingPageViewModel)
	{
		InitializeComponent();
		BindingContext = _landingPageViewModel;
	}
}