namespace MANDUU.Views.MainPages;

public partial class PurchasesPage : ContentPage
{
	public PurchasesPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Shell.Current.FlyoutIsPresented = true;
    }
}