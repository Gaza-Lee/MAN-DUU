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
    public class SubCategoryGroup
    {
        public string SubCategoryName { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        public SubCategoryGroup(string name, IEnumerable<Product> products)
        {
            SubCategoryName = string.IsNullOrWhiteSpace(name)
                ? name
                : char.ToUpper(name[0]) + name.Substring(1);

            Products = new ObservableCollection<Product>(products);
        }
    }

    public partial class CategoryViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ProductService _productService;
        private readonly ProductCategoryService _categoryService;
        private readonly INavigationService _navigationService;

        [ObservableProperty] private int categoryId;
        [ObservableProperty] private MainCategory selectedMainCategory;
        [ObservableProperty] private ObservableCollection<SubCategoryGroup> subCategoryGroups = new();

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
            var grouped = allProducts
                .GroupBy(p =>
                    SelectedMainCategory.SubCategories
                        .FirstOrDefault(sc => sc.Id == p.SubCategoryId)?.Name ?? "Other")
                .Select(g => new SubCategoryGroup(g.Key, g))
                .ToList();

            SubCategoryGroups = new ObservableCollection<SubCategoryGroup>(grouped);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("CategoryId", out var catId) && catId is int id)
                CategoryId = id;

            await LoadProductsAsync();
        }
    }
}
