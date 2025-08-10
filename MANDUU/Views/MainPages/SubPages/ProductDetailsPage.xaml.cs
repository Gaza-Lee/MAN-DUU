using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages.SubPages;

public partial class ProductDetailsPage : ContentPage
{
	public ProductDetailsPage(ProductDetailViewModel _productDetailviewModel)
	{
		InitializeComponent();
		BindingContext = _productDetailviewModel;
	}
}