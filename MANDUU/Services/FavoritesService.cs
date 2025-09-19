using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.Models;

namespace MANDUU.Services
{
    public class FavoritesService
    {
        private readonly ObservableCollection<FavoriteItem> _favoriteItems = new();
        private readonly ObservableCollection<FavoriteItem> _favoriteProducts = new();
        private readonly ObservableCollection<FavoriteItem> _favoriteShops = new();

        private int _nextFavoriteId = 1;

        public ObservableCollection<FavoriteItem> FavoriteProducts => _favoriteProducts;
        public ObservableCollection<FavoriteItem> FavoriteShops => _favoriteShops;
        public ObservableCollection<FavoriteItem> AllFavorites => _favoriteItems;

        public FavoritesService()
        {
            // Sync filtered collections whenever main list changes
            _favoriteItems.CollectionChanged += OnFavoritesChanged;
        }

        private void OnFavoritesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Rebuild both lists efficiently
            SyncFilteredCollections();
        }

        private void SyncFilteredCollections()
        {
            // Clear and refill; simple and effective for moderate-sized lists
            _favoriteProducts.Clear();
            foreach (var item in _favoriteItems.Where(f => f.IsProduct))
                _favoriteProducts.Add(item);

            _favoriteShops.Clear();
            foreach (var item in _favoriteItems.Where(f => f.IsShop))
                _favoriteShops.Add(item);
        }

        public async Task AddProductToFavoritesAsync(Product product)
        {
            if (!_favoriteItems.Any(f => f.IsProduct && f.Product.Id == product.Id))
            {
                _favoriteItems.Add(new FavoriteItem
                {
                    Id = _nextFavoriteId++,
                    Product = product,
                    AddedDate = DateTime.Now
                });
            }
            await Task.CompletedTask;
        }

        public async Task AddShopToFavoritesAsync(Shop shop)
        {
            if (!_favoriteItems.Any(f => f.IsShop && f.Shop.Id == shop.Id))
            {
                _favoriteItems.Add(new FavoriteItem
                {
                    Id = _nextFavoriteId++,
                    Shop = shop,
                    AddedDate = DateTime.Now
                });
            }
            await Task.CompletedTask;
        }

        public async Task RemoveFromFavoritesAsync(int favoriteId)
        {
            var item = _favoriteItems.FirstOrDefault(f => f.Id == favoriteId);
            if (item != null)
            {
                _favoriteItems.Remove(item);
            }
            await Task.CompletedTask;
        }

        public async Task<bool> IsProductFavoriteAsync(int productId)
        {
            return await Task.FromResult(_favoriteItems.Any(f => f.IsProduct && f.Product.Id == productId));
        }

        public async Task<bool> IsShopFavoriteAsync(int shopId)
        {
            return await Task.FromResult(_favoriteItems.Any(f => f.IsShop && f.Shop.Id == shopId));
        }
    }
}
