using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.ViewModels.Base
{
    public interface IBaseViewModel
    {
        bool IsBusy { get; }
        Task InitializeAsync();
    }
}
