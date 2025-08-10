using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages.SubPages;

public partial class ProductsByCategoryPage : ContentPage
{
	public ProductsByCategoryPage(ProductByCategoryViewModel _productByCategoryViewModel)
	{
		InitializeComponent();
		BindingContext = _productByCategoryViewModel;
    }
}