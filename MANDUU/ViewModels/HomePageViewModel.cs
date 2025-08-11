using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.Models;
using MANDUU.Services;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Views.MainPages.SubPages;
using System.Diagnostics;

namespace MANDUU.ViewModels
{
    public partial class HomePageViewModel: ObservableObject
    {
        #region Observable properties
        [ObservableProperty]
        ObservableCollection<Offer> offers;

        [ObservableProperty]
        ObservableCollection<Category> mainCategories;

        [ObservableProperty]
        ObservableCollection<Product> bestSellingProducts;

        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;
        private readonly INavigationService _navigationService;

        public List<Product> AllProducts { get; set; }
        #endregion

        #region Commands


        //Navigation to Product Detail Page
        [RelayCommand]
        private async Task SelectedProductAsync(Product selectedProduct)
        {
            if (selectedProduct == null)
                return;

            await _navigationService.NavigateToAsync("productdetailpage", new Dictionary<string, object>
           {
               {"ProductId",selectedProduct.Id}
           });
        }

        //Navigation to Category Page
        [RelayCommand]
        private async Task SelectedCategoryAsync(Category selectedCategory)
        {
            if (selectedCategory == null)
                return;

            await _navigationService.NavigateToAsync("categorypage", new Dictionary<string, object>
            {
                {"categoryName", selectedCategory.Name }
            });
        }
        #endregion


        public HomePageViewModel(
            CategoryService categoryService,
            ProductService productService,
            INavigationService navigationService)
        { 
            offers = [];
            mainCategories = [];
            bestSellingProducts = [];

            _productService = productService;
            _categoryService = categoryService;
            _navigationService = navigationService;
        }

        #region Properties



        public async Task InitializeAsync()
        {
            //Load Offers or Adds
            offers.Clear();
            foreach (var offer in Offer.GetOffers())
            {
                offers.Add(offer);
            }

            //Load Main Categories
            mainCategories.Clear();
            var categories = await _categoryService.GetMainCategoriesAsync();
            foreach(var category in categories)
            {
                mainCategories.Add(category);
            }

            //Load Best Selling Products
            bestSellingProducts.Clear();
            var products = await _productService.GetBestSellingProductsAsync(10);
            foreach (var product in products)
            {
                bestSellingProducts.Add(product);
            }
        }
        #endregion

    }
}
