using MANDUU.ViewModels;
using MANDUU.Services;

namespace MANDUU.Views.MainPages.SubPages;

public partial class UserProfilePage : ContentPage
{
	private readonly UserProfileViewModel _userProfileViewModel;
	public UserProfilePage(UserProfileViewModel userProfileViewModel)
	{
		InitializeComponent();
		_userProfileViewModel = userProfileViewModel;
        BindingContext = _userProfileViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _userProfileViewModel.InitializeAsync();
    }
}