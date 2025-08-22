using MANDUU.Controls;
using MANDUU.ViewModels;
namespace MANDUU.Views.MainPages;

public partial class EPrintingPage : ContentPage
{
	public EPrintingPage(EPrintingViewModel ePrintingViewModel)
	{
		InitializeComponent();
		BindingContext = ePrintingViewModel;
	}
}