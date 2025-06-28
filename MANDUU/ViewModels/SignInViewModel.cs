using MANDUU.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MANDUU.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private string _username;
        private string _email;
        private string _phoneNumber;
        private string _password;

        public ICommand ProceedCommand { get; }


        public SignInViewModel()
        {
            ProceedCommand = new Command(async () => await OnProceed());
            _username = string.Empty;
            _email = string.Empty;
            _phoneNumber = string.Empty;
            _password = string.Empty;
           
        }

        #region Properties
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if(_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods
        public async Task OnProceed()
        {
            IsBusy = true;
            await Task.Delay(2000);
            await Shell.Current.GoToAsync("HomePage");
            IsBusy = false;
        }
        #endregion
    }
}
