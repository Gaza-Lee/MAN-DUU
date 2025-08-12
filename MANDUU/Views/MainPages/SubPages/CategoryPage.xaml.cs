using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages.SubPages;

public partial class CategoryPage : ContentPage
{
	public CategoryPage(CategoryViewModel _categoryViewModel)
	{
		InitializeComponent();
		BindingContext = _categoryViewModel;
	}
}