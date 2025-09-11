using MANDUU.Services;
using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages;

public partial class ShopCategoryPage : ContentPage
{
	private readonly ShopCategoryViewModel _shopCategoryViewModel;
	public ShopCategoryPage(ShopCategoryViewModel shopCategoryViewModel)
	{
		InitializeComponent();
		_shopCategoryViewModel = shopCategoryViewModel;
		BindingContext = _shopCategoryViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _shopCategoryViewModel.InitializeAsync();
    }
}