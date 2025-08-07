using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages;

public partial class HomePage : ContentPage
{

	private readonly HomePageViewModel _homePageViewModel;
	public HomePage()
	{
		InitializeComponent();
		_homePageViewModel = new HomePageViewModel();
		BindingContext = _homePageViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _homePageViewModel.InitializeAsync();
    }
}