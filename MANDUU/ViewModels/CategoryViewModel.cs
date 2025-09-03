using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MANDUU.ViewModels
{

    public partial class CategoryViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ProductService _productService;
        private readonly ProductCategoryService _categoryService;
        private readonly INavigationService _navigationService;

        [ObservableProperty] private int categoryId;
        [ObservableProperty] private MainCategory selectedMainCategory;
        [ObservableProperty] private ObservableCollection<SubCategory> subCategories = new();

        public CategoryViewModel(
            ProductService productService,
            ProductCategoryService categoryService,
            INavigationService navigationService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task GoToProductDetailAsync(Product product)
        {
            if (product == null) return;

            await _navigationService.NavigateToAsync("productdetailpage", new Dictionary<string, object>
        {
            { "ProductId", product.Id }
        });
        }

        public async Task LoadProductsAsync()
        {
            if (CategoryId <= 0) return;

            // Load the selected category
            SelectedMainCategory = await _categoryService.GetMainCategoryByIdAsync(CategoryId);
            if (SelectedMainCategory == null) return;

            // Get all products in this category
            var allProducts = await _productService.GetProductsByMainCategoryAsync(CategoryId);

            // Group products by subcategory

            var subCategories = SelectedMainCategory.SubCategories
                .Select(sc => new SubCategory
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    MainCategoryId = sc.MainCategoryId,
                    MainCategory = sc.MainCategory,
                    Products = new ObservableCollection<Product>(allProducts.Where(p => p.SubCategoryId == sc.Id))
                })
                .ToList();

            SubCategories = new ObservableCollection<SubCategory>(subCategories);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("CategoryId", out var catId) && catId is int id)
                CategoryId = id;

            await LoadProductsAsync();
        }
    }
}
