using MANDUU.ViewModels;
using Microsoft.Maui.Controls;

namespace MANDUU.Views.ShopPages
{
    public partial class MyShopPage : ContentPage
    {
        private readonly MyShopViewModel _myShopViewModel;

        public MyShopPage(MyShopViewModel viewModel)
        {
            InitializeComponent();
            _myShopViewModel = viewModel;
            BindingContext = _myShopViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _myShopViewModel.InitializeAsync();
        }
    }
}