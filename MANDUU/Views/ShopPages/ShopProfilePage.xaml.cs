using MANDUU.ViewModels;
using MANDUU.Services;
namespace MANDUU.Views.ShopPages;

public partial class ShopProfilePage : ContentPage
{
	private readonly ShopProfileViewModel shopProfileViewModel;
    public ShopProfilePage(ShopService shopService, ProductService productService, INavigationService navigationService)
	{
		InitializeComponent();
		shopProfileViewModel = new ShopProfileViewModel(shopService, productService, navigationService);
		BindingContext = shopProfileViewModel;
    }
}