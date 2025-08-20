using MANDUU.Services;
using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages;

public partial class ShopCategoryPage : ContentPage
{
	private readonly ShopCategoryViewModel shopCategoryViewModel;
	public ShopCategoryPage(ShopCategoryService categoryService, ShopService shopService, INavigationService navigationService)
	{
		InitializeComponent();
		shopCategoryViewModel = new ShopCategoryViewModel(categoryService, shopService, navigationService);
		BindingContext = shopCategoryViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await shopCategoryViewModel.InitializeAsync();
    }
}