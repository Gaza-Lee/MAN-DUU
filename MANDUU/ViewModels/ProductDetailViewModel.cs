using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace MANDUU.ViewModels
{
    public partial class ProductDetailViewModel : ObservableObject, IQueryAttributable
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

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ProductId", out var id))
            {
                if (id is int productId)
                    await LoadProductAsync(productId);
            }
        }

        [RelayCommand]
        public async Task LoadProductAsync(int productId)
        {
            Product = await _productService.GetProductByIdAsync(productId);

            if (Product != null)
            {
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
