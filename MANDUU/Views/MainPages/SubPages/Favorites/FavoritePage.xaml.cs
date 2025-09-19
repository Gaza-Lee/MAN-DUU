using MANDUU.ViewModels;
using System.Threading.Tasks;

namespace MANDUU.Views.MainPages.SubPages.Favorites;

public partial class FavoritePage : ContentPage
{
	private readonly FavoritesViewModel _favoritesViewModel;
	public FavoritePage(FavoritesViewModel favoritesViewModel)
	{
		InitializeComponent();
		_favoritesViewModel = favoritesViewModel;
		BindingContext = _favoritesViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		if (!_favoritesViewModel.IsInitialized)
		{
			await _favoritesViewModel.InitializeAsyncCommand.ExecuteAsync(null);		}
    }
}