using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Models;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels
{
    public partial class FavoritesViewModel : BaseViewModel
    {
        private readonly ShopService _shopService;
        private readonly ProductService _productService;
        private readonly FavoritesService _favoritesService;

        [ObservableProperty]
        private ObservableCollection<FavoriteItem> _favoriteShops;

        [ObservableProperty]
        private ObservableCollection<FavoriteItem> _favoriteProducts;

        [ObservableProperty]
        private ObservableCollection<FavoriteItem> _allFavoriteItems;

        [ObservableProperty]
        private int _selectedTabIndex = 0;


        public FavoritesViewModel(INavigationService navigationService,
            ShopService shopService,
            ProductService productService,
            FavoritesService favoritesService): base(navigationService)
        {
            _shopService = shopService;
            _productService = productService;
            _favoritesService = favoritesService;
        }

        [RelayCommand]
        private async Task RemoveFromFavorites(FavoriteItem item)
        {
            if (item == null) return;
            await _favoritesService.RemoveFromFavoritesAsync(item.Id);
        }

        private void LoadFavorites()
        {
            FavoriteShops = _favoritesService.FavoriteShops;
            FavoriteProducts = _favoritesService.FavoriteProducts;
            AllFavoriteItems = _favoritesService.AllFavorites;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            LoadFavorites();
        }
    }
}
