using MANDUU.Services;
using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages;

public partial class HomePage : ContentPage
{

	private readonly HomePageViewModel _homePageViewModel;
	public HomePage(HomePageViewModel homePageViewModel)
	{
		InitializeComponent();
		_homePageViewModel = homePageViewModel;
		BindingContext = _homePageViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		if (!_homePageViewModel.IsInitialized)
		{
			await _homePageViewModel.InitializeAsyncCommand.ExecuteAsync(null);
        }
    }
}