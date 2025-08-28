using MANDUU.Services;
using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages;

public partial class HomePage : ContentPage
{

	private readonly HomePageViewModel _homePageViewModel;
	public HomePage(ProductCategoryService categoryService,
            ProductService productService,
            INavigationService navigationService,
            ShopService shopService,
            CartService cartService,
            FavoritesService favoritesService)
	{
		InitializeComponent();
		_homePageViewModel = new HomePageViewModel(categoryService, productService, navigationService, shopService, cartService, favoritesService);
		BindingContext = _homePageViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _homePageViewModel.InitializeAsync();
    }
}