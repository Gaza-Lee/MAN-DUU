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
        private ObservableCollection<FavoriteItem> _favoriteItems = new();
        private int _nextFavoriteId = 1;

        public ObservableCollection<FavoriteItem> GetFavorites() => _favoriteItems;

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

        public ObservableCollection<FavoriteItem> GetFavoriteProducts() =>
            new ObservableCollection<FavoriteItem>(_favoriteItems.Where(f => f.IsProduct));

        public ObservableCollection<FavoriteItem> GetFavoriteShops() =>
            new ObservableCollection<FavoriteItem>(_favoriteItems.Where(f => f.IsShop));
    }
}
