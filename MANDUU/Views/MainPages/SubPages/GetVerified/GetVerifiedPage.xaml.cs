using MANDUU.ViewModels;

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
}