using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MANDUU.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        #region Observable Properties
        [ObservableProperty]
        private ObservableCollection<MainCategory> mainCategories = new();

        [ObservableProperty]
        private ObservableCollection<Product> bestSellingProducts = new();

        [ObservableProperty]
        private ObservableCollection<Offer> offers = new();

        [ObservableProperty]
        private MainCategory selectedCategory;

        [ObservableProperty]
        private Shop selectedShop;

        [ObservableProperty]
        private ObservableCollection<Shop> _recommendedShops = new();

        [ObservableProperty]
        private bool hasCartItems;

        // Load all products for the home page to be filtered 
        public List<Product> AllProducts { get; private set; } = new();
        #endregion

        #region Services
        private readonly ShopService _shopService;
        private readonly ProductCategoryService _categoryService;
        private readonly ProductService _productService;
        private readonly INavigationService _navigationService;
        private CartService _cartService;
        private FavoritesService _favoritesService;
        #endregion

        #region Commands

        [RelayCommand]
        private async Task ProfileAsync()
        {
            await _navigationService.NavigateToAsync("userprofilepage");
        }

        [RelayCommand]
        private async Task SelectedCategoryAsync(MainCategory selectedCategory)
        {
            if (selectedCategory == null) return;

            await _navigationService.NavigateToAsync("categorypage", new Dictionary<string, object>
            {
                { "CategoryId", selectedCategory.Id }
            });

            SelectedCategory = null;
        }


        [RelayCommand]
        private async Task SelectedProductAsync(Product product)
        {
            if (product == null) return;

            await _navigationService.NavigateToAsync("productdetailpage", new Dictionary<string, object>
            {
                { "ProductId", product.Id }
            });
        }

        [RelayCommand]
        private async Task SelectedShopAsync(Shop shop)
        {
            if (shop == null) return;

            await _navigationService.NavigateToAsync("shopprofilepage", new Dictionary<string, object>
            {
                { "ShopId", shop.Id },
                { "ShopName", shop.Name }
            });

            SelectedShop = null;
        }

        [RelayCommand]
        private async Task GoToCartAsync()
        {
            await _navigationService.NavigateToAsync("cartpage");
        }
        #endregion

        #region Constructor
        public HomePageViewModel(
            ProductCategoryService categoryService,
            ProductService productService,
            INavigationService navigationService,
            ShopService shopService,
            CartService cartService,
            FavoritesService favoritesService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _navigationService = navigationService;
            _shopService = shopService;
            _cartService = cartService;
            _favoritesService = favoritesService;

            _cartService.CartUpdated += OnCartUpdated;
        }
        #endregion

        #region Initialization
        public async Task InitializeAsync()
        {
            await LoadOffersAsync();
            await LoadMainCategoriesAsync();
            await LoadBestSellingProductsAsync();
            await LoadAllProductsAsync();
            await LoadRecommendedShopsAsync();
        }

        private async Task LoadOffersAsync()
        {
            offers.Clear();
            foreach (var offer in Offer.GetOffers())
                offers.Add(offer);
        }

        private async Task LoadMainCategoriesAsync()
        {
            mainCategories.Clear();
            var categories = await _categoryService.GetAllMainCategoriesAsync();
            foreach (var category in categories)
                mainCategories.Add(category);
        }

        private async Task LoadBestSellingProductsAsync()
        {
            bestSellingProducts.Clear();
            var products = await _productService.GetBestSellingProductsAsync(10);
            foreach (var product in products)
                bestSellingProducts.Add(product);
        }

        private async Task LoadAllProductsAsync()
        {
            AllProducts = (await _productService.GetProductsAsync()).ToList();
        }
        private async Task LoadRecommendedShopsAsync()
        {
            RecommendedShops.Clear();
            var shops = await _shopService.GetRecommendedShopsAsync(4);
            foreach (var shop in shops)
            {
                RecommendedShops.Add(shop);
            }
        }


        private void OnCartUpdated(object sender, EventArgs e)
        {
            HasCartItems = _cartService.HasItems();
        }

        [RelayCommand]
        private async Task ToggleProductFavoriteAsync(Product product)
        {
            if (product == null) return;

            await _favoritesService.AddProductToFavoritesAsync(product);


            var toast = 
                Toast.Make($"{product.Name} added to Cart", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
        }

        [RelayCommand]
        private async Task ToggleShopFavoriteAsync(Shop shop)
        {
            if (shop == null) return;

            await _favoritesService.AddShopToFavoritesAsync(shop);

            var toast = Toast.Make($"{shop.Name}added to favorites", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
        }

        [RelayCommand]
        private async Task AddToCartAsync(Product product)
        {
            if (product == null) return;

            await _cartService.AddToCartAsync(product);


            var toast = Toast.Make($"{product.Name} added to Cart", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
        }

        // Dispose method to unsubscribe from events
        public void Dispose()
        {
            _cartService.CartUpdated -= OnCartUpdated;
        }
        #endregion

        #region Helper Methods
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
            => await _productService.GetProductsByMainCategoryAsync(categoryId);

        public async Task<IEnumerable<Product>> GetProductsByShopAsync(int shopId)
            => await _productService.GetProductsByShopAsync(shopId);

        public async Task<IEnumerable<Product>> GetProductsByShopAndCategoryAsync(int shopId, int mainCategoryId)
        {
            var allProducts = await GetProductsByShopAsync(shopId);
            return allProducts.Where(p => p.MainCategoryId == mainCategoryId);
        }
        #endregion
    }
}
