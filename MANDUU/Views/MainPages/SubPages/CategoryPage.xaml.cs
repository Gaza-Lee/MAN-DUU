using MANDUU.Services;
using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages.SubPages;

public partial class CategoryPage : ContentPage
{
    private readonly CategoryViewModel _categoryViewModel;

    public CategoryPage(ProductService productService, ProductCategoryService categoryService, INavigationService navigationService)
    {
        InitializeComponent();
        _categoryViewModel = new CategoryViewModel(productService, categoryService, navigationService);
        BindingContext = _categoryViewModel;
    }
}
