using CommunityToolkit.Mvvm.ComponentModel;
using MANDUU.Models;
using MANDUU.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MANDUU.ViewModels
{
    public partial class CategoryViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ProductService _productService;

        [ObservableProperty]
        private string categoryName;
        [ObservableProperty]
        private ObservableCollection<Product> products = new();
        [ObservableProperty]
        private MainCategory selectedMainCategory;

        public CategoryViewModel(ProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        // subCategoryName is optional — when null/empty we show all products for the main category
        public async Task LoadProductsAsync(string subCategoryName = null)
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
                return;

            var allProducts = await _productService.GetProductsAsync();

            var filtered = allProducts.Where(p =>
                string.Equals(p.MainCategoryName, CategoryName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(subCategoryName))
            {
                filtered = filtered.Where(p => string.Equals(p.SubCategoryName, subCategoryName, StringComparison.OrdinalIgnoreCase));
            }

            Products = new ObservableCollection<Product>(filtered);

            // Load banner / main category info
            SelectedMainCategory = await _productService.GetMainCategoryAsync(CategoryName);
        }

        // called by Shell navigation
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("CategoryName", out var category) && category != null)
                CategoryName = category.ToString();

            string sub = null;
            if (query.TryGetValue("SubCategoryName", out var subCategory) && subCategory != null)
                sub = subCategory.ToString();

            _ = LoadProductsAsync(sub);
        }
    }
}