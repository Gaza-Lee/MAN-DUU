using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MANDUU.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        #region Services
        private readonly ShopService _shopService;
        private readonly ProductCategoryService _categoryService;
        private readonly ProductService _productService;
        private readonly CartService _cartService;
        private readonly FavoritesService _favoritesService;
        #endregion

        #region Observable Properties
        [ObservableProperty]
        private ObservableCollection<MainCategory> _mainCategories = new();

        [ObservableProperty]
        private ObservableCollection<Product> _bestSellingProducts = new();

        [ObservableProperty]
        private ObservableCollection<Offer> _offers = new();

        [ObservableProperty]
        private MainCategory _selectedMainCategory;

        [ObservableProperty]
        private Shop _selectedShop;

        [ObservableProperty]
        private ObservableCollection<Shop> _recommendedShops = new();

        [ObservableProperty]
        private bool _hasCartItems;
        #endregion

        #region Constructor
        public HomePageViewModel(
            ProductCategoryService categoryService,
            ProductService productService,
            INavigationService navigationService,
            ShopService shopService,
            CartService cartService,
            FavoritesService favoritesService) : base(navigationService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _shopService = shopService;
            _cartService = cartService;
            _favoritesService = favoritesService;
            _cartService.CartUpdated += OnCartUpdated;
        }
        #endregion

        #region Commands

        [RelayCommand]
        private async Task GoToProfileAsync()
        {
            await NavigationService.NavigateToAsync("userprofilepage");
        }

        [RelayCommand]
        private async Task GoToSelectedMainCategoryAsync(MainCategory selectedMainCategory)
        {
            if (selectedMainCategory != null)
            {
                await NavigationService.NavigateToAsync("categorypage", new Dictionary<string, object>
                {
                    { "CategoryId", selectedMainCategory.Id }
                });
                selectedMainCategory = null;
            }            
        }

        [RelayCommand]
        private async Task GoToSelectedProductDetailsAsync(Product product)
        {
            if (product == null) return;

            await NavigationService.NavigateToAsync("productdetailpage", new Dictionary<string, object>
            {
                { "ProductId", product.Id }
            });
        }

        [RelayCommand]
        private async Task GoToSelectedShopProfileAsync(Shop shop)
        {
            if (shop == null) return;

            await NavigationService.NavigateToAsync("shopprofilepage", new Dictionary<string, object>
            {
                { "ShopId", shop.Id },
                { "ShopName", shop.Name }
            });

            SelectedShop = null;
        }

        [RelayCommand]
        private async Task GoToCartAsync()
        {
            await NavigationService.NavigateToAsync("cartpage");
        }

        [RelayCommand]
        private async Task ToggleProductFavoriteAsync(Product product)
        {
            if (product == null) return;

            await _favoritesService.AddProductToFavoritesAsync(product);
            ShowToast($"{product.Name} added to favorites");
        }

        [RelayCommand]
        private async Task ToggleShopFavoriteAsync(Shop shop)
        {
            if (shop == null) return;

            await _favoritesService.AddShopToFavoritesAsync(shop);
            ShowToast($"{shop.Name} added to favorites");
        }

        [RelayCommand]
        private async Task AddToCartAsync(Product product)
        {
            if (product == null) return;

            await _cartService.AddToCartAsync(product);
            ShowToast($"{product.Name} added to Cart");
        }
        #endregion

        #region Initialization
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            await IsBusyFor(async () =>
            {
                await LoadOffersAsync();
                await LoadMainCategoriesAsync();
                await LoadBestSellingProductsAsync();
                await LoadRecommendedShopsAsync();
                UpdateCartStatus();
            });
        }

        private async Task LoadOffersAsync()
        {
            try
            {
                Offers.Clear();
                var offers = await Task.Run(() => Offer.GetOffers());
                foreach (var offer in offers)
                {
                    Offers.Add(offer);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading offers: {ex.Message}");
                ShowToast("Failed to load offers");
            }
        }

        private async Task LoadMainCategoriesAsync()
        {
            try
            {
                MainCategories.Clear();
                var categories = await _categoryService.GetAllMainCategoriesAsync();
                foreach (var category in categories)
                {
                    MainCategories.Add(category);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
                ShowToast("Failed to load categories");
            }
        }

        private async Task LoadBestSellingProductsAsync()
        {
            try
            {
                BestSellingProducts.Clear();
                var products = await _productService.GetBestSellingProductsAsync(10);
                foreach (var product in products)
                {
                    BestSellingProducts.Add(product);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading products: {ex.Message}");
                ShowToast("Failed to load products");
            }
        }

        private async Task LoadRecommendedShopsAsync()
        {
            try
            {
                RecommendedShops.Clear();
                var shops = await _shopService.GetRecommendedShopsAsync(4);
                foreach (var shop in shops)
                {
                    RecommendedShops.Add(shop);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading shops: {ex.Message}");
                ShowToast("Failed to load shops");
            }
        }

        private void UpdateCartStatus()
        {
            HasCartItems = _cartService.HasItems();
        }

        private void OnCartUpdated(object sender, EventArgs e)
        {
            UpdateCartStatus();
        }
        #endregion

        #region Cleanup
        public void Dispose()
        {
            _cartService.CartUpdated -= OnCartUpdated;
        }
        #endregion
    }
}