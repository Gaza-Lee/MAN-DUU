using MANDUU.ViewModels;
using MANDUU.Services;
namespace MANDUU.Views.ShopPages;

public partial class ShopProfilePage : ContentPage
{
	private readonly ShopProfileViewModel shopProfileViewModel;
    public ShopProfilePage(ShopService shopService, ProductService productService)
	{
		InitializeComponent();
		shopProfileViewModel = new ShopProfileViewModel(shopService, productService);
		BindingContext = shopProfileViewModel;
    }
}