using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
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

        // All products loaded for filtering, searching, etc.
        public List<Product> AllProducts { get; private set; } = new();
        #endregion

        #region Services
        private readonly ProductCategoryService _categoryService;
        private readonly ProductService _productService;
        private readonly INavigationService _navigationService;
        #endregion

        #region Commands
        [RelayCommand]
        private async Task SelectedCategoryAsync(MainCategory category)
        {
            if (category == null) return;

            await _navigationService.NavigateToAsync("categorypage", new Dictionary<string, object>
            {
                { "CategoryId", category.Id }
            });

            SelectedCategory = null; // Reset selection for UI
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
        #endregion

        #region Constructor
        public HomePageViewModel(
            ProductCategoryService categoryService,
            ProductService productService,
            INavigationService navigationService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _navigationService = navigationService;
        }
        #endregion

        #region Initialization
        public async Task InitializeAsync()
        {
            await LoadOffersAsync();
            await LoadMainCategoriesAsync();
            await LoadBestSellingProductsAsync();
            await LoadAllProductsAsync();
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
