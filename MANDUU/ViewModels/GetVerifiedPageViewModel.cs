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


        public GetVerifiedPageViewModel(INavigationService navigationService): base(navigationService)
        { }        

    }
}
