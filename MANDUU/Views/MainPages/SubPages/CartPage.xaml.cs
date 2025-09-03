using MANDUU.ViewModels;
using MANDUU.Services;
namespace MANDUU.Views.MainPages.SubPages;

public partial class CartPage : ContentPage
{
	public CartPage(CartPageViewModel cartPageViewModel)
	{
		InitializeComponent();
		BindingContext = cartPageViewModel;
	}
}