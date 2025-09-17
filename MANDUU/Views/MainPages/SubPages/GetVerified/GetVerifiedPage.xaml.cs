using MANDUU.ViewModels;
using System.Threading.Tasks;

namespace MANDUU.Views.MainPages.SubPages.GetVerified;

public partial class GetVerifiedPage : ContentPage
{
	private readonly GetVerifiedPageViewModel _getVerifiedPageViewModel;
	public GetVerifiedPage(GetVerifiedPageViewModel getVerifiedPageViewModel)
	{
		InitializeComponent();
		_getVerifiedPageViewModel = getVerifiedPageViewModel;
		BindingContext = _getVerifiedPageViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		if (!_getVerifiedPageViewModel.IsInitialized)
		{
			await _getVerifiedPageViewModel.InitializeAsyncCommand.ExecuteAsync(null);
		}
    }
}