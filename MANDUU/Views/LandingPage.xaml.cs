using MANDUU.ViewModels;

namespace MANDUU.Views;

public partial class LandingPage : ContentPage
{
	public LandingPage(LandingPageViewModel landingPageViewModel)
	{
		InitializeComponent();
		BindingContext = landingPageViewModel;
	}


}