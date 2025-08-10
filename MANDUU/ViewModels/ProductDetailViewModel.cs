using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace MANDUU.ViewModels
{
    public partial class ProductDetailViewModel : ObservableObject
    {
        private readonly ProductService _productService;

        [ObservableProperty]
        private Product product;

        [ObservableProperty]
        private ObservableCollection<Product> suggestedProducts;

        public ProductDetailViewModel(ProductService productService)
        {
            _productService = productService;
            SuggestedProducts = new ObservableCollection<Product>();
        }

        [RelayCommand]
        public async Task LoadProductAsync(int productId)
        {
            // Load main product
            Product = await _productService.GetProductByIdAsync(productId);

            if (Product != null)
            {
                // Get products in the same subcategory, excluding the current product
                var products = await _productService.GetProductsAsync();
                var suggestions = products
                    .Where(p => p.SubCategoryName == Product.SubCategoryName && p.Id != Product.Id)
                    .ToList();

                SuggestedProducts.Clear();
                foreach (var p in suggestions)
                    SuggestedProducts.Add(p);
            }
        }
    }
}
