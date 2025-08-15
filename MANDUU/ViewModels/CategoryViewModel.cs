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

        [ObservableProperty] private string categoryName;
        [ObservableProperty] private MainCategory selectedMainCategory;
        [ObservableProperty] private ObservableCollection<SubCategoryGroup> subCategoryGroups = new();

        public CategoryViewModel(
            ProductService productService,
            INavigationService navigationService)
        {
            _productService = productService;
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
            if (string.IsNullOrWhiteSpace(CategoryName)) return;

            // Load main category
            var allMainCategories = await _categoryService.GetAllMainCategoriesAsync();
            SelectedMainCategory = allMainCategories
                .FirstOrDefault(mc => string.Equals(mc.Name, CategoryName, System.StringComparison.OrdinalIgnoreCase));

            if (SelectedMainCategory == null) return;

            // Get all products in this main category
            var allProducts = await _productService.GetProductsByMainCategoryAsync(SelectedMainCategory.Id);

            // Group by subcategory name
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
            if (query.TryGetValue("CategoryName", out var category) && category is string categoryStr)
                CategoryName = categoryStr;

            await LoadProductsAsync();
        }
    }
}
