using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MANDUU.Services;
using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels
{
    public partial class GetVerifiedPageViewModel: BaseViewModel
    {
        private readonly IUserService _userService;

        [ObservableProperty] private int _selectedTabIndex = 0;

        [ObservableProperty] private string _firstName;

        [ObservableProperty] private string _lastName;

        [ObservableProperty] private DateTime _dateOfBirth = DateTime.Today;

        [ObservableProperty] private string _email;

        [ObservableProperty] private string _phoneNumber;

        [ObservableProperty] private string _residentialAddress;
        public GetVerifiedPageViewModel(INavigationService navigationService, 
            IUserService userService): base(navigationService)
        { 
            _userService = userService;
        }        

        [RelayCommand]
        private async Task NextAsycn()
        {
        }
    }
}
