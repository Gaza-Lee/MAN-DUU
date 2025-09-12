using MANDUU.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MANDUU.Services
{
    public class CartService
    {
        private ObservableCollection<CartItem> _cartItems = new();
        private int _nextCartItemId = 1;

        public event EventHandler CartUpdated;

        public IReadOnlyList<CartItem> GetCartItems() => _cartItems.ToList().AsReadOnly();

        public async Task AddToCartAsync(Product product, int quantity = 1)
        {
            var existingItem = _cartItems.FirstOrDefault(item => item.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    Id = _nextCartItemId++,
                    ProductId = product.Id,
                    Product = product,
                    Quantity = quantity
                };

                // Subscribe to property changes to update TotalPrice
                newItem.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(CartItem.Quantity) ||
                        e.PropertyName == nameof(CartItem.Product))
                    {
                        CartUpdated?.Invoke(this, EventArgs.Empty);
                    }
                };

                _cartItems.Add(newItem);
            }

            CartUpdated?.Invoke(this, EventArgs.Empty);
            await Task.CompletedTask;
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var item = _cartItems.FirstOrDefault(i => i.Id == cartItemId);
            if (item != null)
            {
                _cartItems.Remove(item);
                CartUpdated?.Invoke(this, EventArgs.Empty);
            }
            await Task.CompletedTask;
        }

        public async Task UpdateQuantityAsync(int cartItemId, int newQuantity)
        {
            var item = _cartItems.FirstOrDefault(i => i.Id == cartItemId);
            if (item != null && newQuantity > 0)
            {
                item.Quantity = newQuantity;
            }
            await Task.CompletedTask;
        }

        public async Task ClearCartAsync()
        {
            _cartItems.Clear();
            CartUpdated?.Invoke(this, EventArgs.Empty);
            await Task.CompletedTask;
        }

        public int GetCartItemCount() => _cartItems.Sum(item => item.Quantity);

        public decimal GetCartTotal() => _cartItems.Sum(item => item.TotalPrice);

        public bool HasItems() => _cartItems.Any();
    }
}