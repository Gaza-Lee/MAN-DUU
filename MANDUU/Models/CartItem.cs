using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public partial class CartItem : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private int productId;

        [ObservableProperty]
        private Product product;

        [ObservableProperty]
        private int quantity;
        
        public decimal TotalPrice => Product?.Price * Quantity ?? 0;

        partial void OnProductChanged(Product value)
        {
            OnPropertyChanged(nameof(TotalPrice));
        }

        partial void OnQuantityChanged(int value)
        {
            OnPropertyChanged(nameof(TotalPrice));
        }
    }
}