using MANDUU.ViewModels;

namespace MANDUU.Views.ShopPages;

public partial class CreateShopPage : ContentPage
{
	private readonly CreateShopViewModel _createShopViewModel;
	public CreateShopPage(CreateShopViewModel createShopViewModel)
	{
		InitializeComponent();
		_createShopViewModel = createShopViewModel;
        BindingContext = _createShopViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!_createShopViewModel.IsInitialized)
        {
            await _createShopViewModel.InitializeAsyncCommand.ExecuteAsync(null);
        }
    }
}