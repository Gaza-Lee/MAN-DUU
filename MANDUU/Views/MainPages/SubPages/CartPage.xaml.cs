using MANDUU.ViewModels;
using MANDUU.Services;
namespace MANDUU.Views.MainPages.SubPages;

public partial class CartPage : ContentPage
{
	private readonly CartPageViewModel _cartPageViewModel;
	public CartPage(CartService cartService)
	{
		InitializeComponent();
		_cartPageViewModel = new CartPageViewModel(cartService);
		BindingContext = _cartPageViewModel;
	}
}