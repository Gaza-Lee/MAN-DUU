using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommunityToolkit.Maui.Alerts;

namespace MANDUU.ViewModels
{

    public partial class CategoryViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ProductService _productService;
        private readonly ProductCategoryService _categoryService;
        private readonly INavigationService _navigationService;
        private readonly CartService _cartService;

        [ObservableProperty] private int categoryId;
        [ObservableProperty] private MainCategory selectedMainCategory;
        [ObservableProperty] private ObservableCollection<SubCategory> subCategories = new();
        [ObservableProperty] private bool _hasCartItems;

        public CategoryViewModel(
            ProductService productService,
            ProductCategoryService categoryService,
            INavigationService navigationService,
            CartService cartService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _navigationService = navigationService;
            _cartService = cartService;
            _cartService.CartUpdated += OnCartUpdated;
        }

        [RelayCommand]
        private async Task GoToSelectedProductDetailsAsync(Product product)
        {
            if (product == null) return;

            await _navigationService.NavigateToAsync("productdetailpage", new Dictionary<string, object>
        {
            { "ProductId", product.Id }
        });
        }

        [RelayCommand]
        private async Task AddToCartAsync(Product product)
        {
            if (product != null)
            {
                await _cartService.AddToCartAsync(product);
                var addToCartToast = Toast.Make($"{product.Name} added to cart", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
                await addToCartToast.Show();
            }
        }

        [RelayCommand]
        private async Task GoToCartAsync()
        {
            await _navigationService.NavigateToAsync("cartpage");
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

            var subCategories = SelectedMainCategory.SubCategories
                .Select(sc => new SubCategory
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    MainCategoryId = sc.MainCategoryId,
                    MainCategory = sc.MainCategory,
                    Products = new ObservableCollection<Product>(allProducts.Where(p => p.SubCategoryId == sc.Id))
                })
                .ToList();

            SubCategories = new ObservableCollection<SubCategory>(subCategories);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("CategoryId", out var catId) && catId is int id)
                CategoryId = id;
        }

        private void UpdateCartStatus()
        {
            HasCartItems = _cartService.HasItems();
        }

        private void OnCartUpdated(object sender, EventArgs e)
        {
            UpdateCartStatus();
        }

        public async Task InitializeAsync()
        {
            await LoadProductsAsync();
            UpdateCartStatus();
        }
    }
}
