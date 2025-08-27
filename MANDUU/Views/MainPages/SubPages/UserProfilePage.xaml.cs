using MANDUU.ViewModels;
using MANDUU.Services;

namespace MANDUU.Views.MainPages.SubPages;

public partial class UserProfilePage : ContentPage
{
	private readonly UserProfileViewModel _userProfileViewModel;
	public UserProfilePage(IUserService userService, INavigationService navigationService)
	{
		InitializeComponent();
		_userProfileViewModel = new UserProfileViewModel(userService, navigationService);
		BindingContext = _userProfileViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _userProfileViewModel.InitializeAsync();
    }
}