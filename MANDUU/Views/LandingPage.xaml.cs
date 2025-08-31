using MANDUU.ViewModels;

namespace MANDUU.Views;

public partial class LandingPage : ContentPage
{
	private readonly LandingPageViewModel _landingPageViewModel;
    public LandingPage(LandingPageViewModel landingPageViewModel)
	{
		InitializeComponent();
		_landingPageViewModel = landingPageViewModel;
        BindingContext = _landingPageViewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (!_landingPageViewModel.IsInitialized)
		{
			await _landingPageViewModel.InitializeAsyncCommand.ExecuteAsync(null);
		}
    }
}