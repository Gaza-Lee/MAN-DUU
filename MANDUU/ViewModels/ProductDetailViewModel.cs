using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Animations;
using MANDUU.Models;
using MANDUU.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using static MANDUU.Models.Product;

namespace MANDUU.ViewModels
{
    public partial class ProductDetailViewModel : ObservableObject, IQueryAttributable
    {
        private readonly INavigationService _navigationService;
        private readonly ShopService _shopService;
        private readonly ProductService _productService;
        private readonly CartService _cartService;
        private bool _isAnimating = false;

        [ObservableProperty]
        private int _currentImageIndex = 0;

        [ObservableProperty]
        private string currentImage;

        [ObservableProperty]
        private Product product;

        [ObservableProperty]
        private Shop shop;

        [ObservableProperty]
        private ObservableCollection<Product> suggestedProducts;

        public bool IsCurrentImage(string imageUrl)
               => string.Equals(CurrentImage, imageUrl, StringComparison.OrdinalIgnoreCase);




        public ProductDetailViewModel(ProductService productService,
            INavigationService navigationService,
            ShopService shopService,
            CartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
            _navigationService = navigationService;
            _shopService = shopService;
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
        private async Task AddToCartAsync(Product product)
        {
            if (product == null) return;

            await _cartService.AddToCartAsync(product);


            var toast = Toast.Make($"{Product.Name} added to Cart", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
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

            if (Product == null)
                return;

            // Load the shop that the product belongs to
            if (Product.ShopId > 0)
            {
                Shop = await _shopService.GetShopByIdAsync(Product.ShopId);
            }

            // Ensure gallery is not null
            Product.Gallery ??= new List<string>();

            // If no gallery images but we have a main image, use it
            if (Product.Gallery.Count == 0 && !string.IsNullOrEmpty(Product.ImageUrl))
            {
                Product.Gallery.Add(Product.ImageUrl);
            }

            // Start at first image
            _currentImageIndex = 0;
            CurrentImage = Product.Gallery.FirstOrDefault();

            // Load suggested products from same subcategory
            await LoadSuggestedProductsAsync();
        }



        [RelayCommand]
        private void SelectImage(string imageUrl)
        {
            if (Product?.Gallery == null) return;

            var index = Product.Gallery.IndexOf(imageUrl);
            if (index >= 0)
            {
                _currentImageIndex = index;
                CurrentImage = imageUrl;
            }
        }


        private async Task LoadSuggestedProductsAsync()
        {
            if (Product == null) return;

            try
            {
                var allProductsInCategory = await _productService.GetProductsBySubCategoryAsync(Product.SubCategoryId);

                var suggestions = allProductsInCategory
                                  .Where(p => p.Id != Product.Id)
                                  .Take(6)
                                  .ToList();

                SuggestedProducts.Clear();
                foreach (var p in suggestions)
                {
                    SuggestedProducts.Add(p);
                }
            }
            catch (Exception ex)
            {
                // Handle error or log it
                SuggestedProducts.Clear();
            }
        }

        [RelayCommand]
        private async Task NavigateToShopAsync()
        {
            if (Shop == null) return;

            await _navigationService.NavigateToAsync("shopprofilepage", new Dictionary<string, object>
            {
                { "ShopId", Shop.Id },
                { "ShopName", Shop.Name }
            });
        }

        [RelayCommand]
        private async Task SelectSuggestedProductAsync(Product product)
        {
            if (product == null) return;

            await _navigationService.NavigateToAsync("productdetailpage", new Dictionary<string, object>
            {
                { "ProductId", product.Id }
            });
        }
    }
}