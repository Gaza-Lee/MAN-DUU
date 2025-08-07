using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MANDUU.Models;
using MANDUU.Services;

namespace MANDUU.ViewModels
{
    public partial class HomePageViewModel: ObservableObject
    {
        #region Variables
        public ObservableCollection<Offer> Offers { get; set; }
        public ObservableCollection<Category> MainCategories { get; set; }
        #endregion

        private readonly CategoryService _categoryService;
        public HomePageViewModel()
        {
            Offers = new ObservableCollection<Offer>();
            MainCategories = new ObservableCollection<Category>();
            _categoryService = new CategoryService();
        }

        #region Properties
        public async Task InitializeAsync()
        {
            foreach(var offer in Offer.GetOffers())
            {
                Offers.Add(offer);
            }

            var mainCategories = await _categoryService.GetMainCategoriesAsync();
            foreach(var category in mainCategories)
            {
                MainCategories.Add(category);
            }
        }
        #endregion

    }
}
