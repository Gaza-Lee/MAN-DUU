using MANDUU.Services;
using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages;

public partial class HomePage : ContentPage
{

	private readonly HomePageViewModel _homePageViewModel;
	public HomePage(ProductCategoryService categoryService, ProductService productService, INavigationService navigationService)
	{
		InitializeComponent();
		_homePageViewModel = new HomePageViewModel(categoryService, productService, navigationService);
		BindingContext = _homePageViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _homePageViewModel.InitializeAsync();
    }
}