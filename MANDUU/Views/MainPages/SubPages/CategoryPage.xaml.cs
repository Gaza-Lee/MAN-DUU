using MANDUU.Services;
using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages.SubPages;

public partial class CategoryPage : ContentPage
{
    private readonly CategoryViewModel _categoryViewModel;

    public CategoryPage(CategoryViewModel categoryViewModel)
    {
        InitializeComponent();
        _categoryViewModel = categoryViewModel;
        BindingContext = _categoryViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _categoryViewModel.InitializeAsync();
    }
}
