namespace MANDUU.Controls;

public partial class ProfileView : ContentView
{
	public ProfileView()
	{
		InitializeComponent();
        BindingContext = this;
	}
    public async Task ShowAsync()
    {
        if (!IsVisible)
        {
            TranslationX = -Width;
            IsVisible = true;
            await this.TranslateTo(0, 0, 250, Easing.CubicOut);
        }
    }

    public async Task HideAsync()
    {
        await this.TranslateTo(-Width, 0, 250, Easing.CubicIn);
        IsVisible = false;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await HideAsync();
    }


}