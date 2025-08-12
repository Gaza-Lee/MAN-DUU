using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using MANDUU.Animations;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace MANDUU.ViewModels
{
    public partial class ProductDetailViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ProductService _productService;
        private bool _isAnimating = false;

        [ObservableProperty]
        private int _currentImageIndex = 0;

        [ObservableProperty]
        private string currentImage;

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
            if (query.TryGetValue("ProductId", out var id) && id is int productId)
            {
                await LoadProductAsync(productId);
            }
        }

        [RelayCommand]
        public async Task NextImageAsync(Image imageView)
        {
            if (_isAnimating || Product?.Gallery == null || Product.Gallery.Count == 0)
                return;

            _isAnimating = true;

            await ImageSlideAnimation.SlideToNextAsync(imageView, () =>
            {
                _currentImageIndex = (_currentImageIndex + 1) % Product.Gallery.Count;
                CurrentImage = Product.Gallery[_currentImageIndex];
            });

            _isAnimating = false;
        }

        [RelayCommand]
        public async Task PreviousImageAsync(Image imageView)
        {
            if (_isAnimating || Product?.Gallery == null || Product.Gallery.Count == 0)
                return;

            _isAnimating = true;

            await ImageSlideAnimation.SlideToPreviousAsync(imageView, () =>
            {
                _currentImageIndex = (_currentImageIndex - 1 + Product.Gallery.Count) % Product.Gallery.Count;
                CurrentImage = Product.Gallery[_currentImageIndex];
            });

            _isAnimating = false;
        }

        [RelayCommand]
        public async Task LoadProductAsync(int productId)
        {
            Product = await _productService.GetProductByIdAsync(productId);

            if (Product == null || string.IsNullOrWhiteSpace(Product.ImageUrl))
                return;

            // Initialize gallery if null
            Product.Gallery ??= new List<string>();

            // Ensure main ImageUrl is always first in gallery
            if (!Product.Gallery.Contains(Product.ImageUrl))
                Product.Gallery.Insert(0, Product.ImageUrl);

            // Start at first image
            _currentImageIndex = 0;
            CurrentImage = Product.Gallery.FirstOrDefault();

            // Load suggested products from same subcategory
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
