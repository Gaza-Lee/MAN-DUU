using MANDUU.Services;
using MANDUU.ViewModels;

namespace MANDUU.Views.MainPages.SubPages;

public partial class PrintingDetailsPage : ContentPage
{
	private readonly PrintingDetailsViewModel printingDetailsViewModel;
    public PrintingDetailsPage(PrintingStationService printingStationService)
	{
		InitializeComponent();
		printingDetailsViewModel = new PrintingDetailsViewModel(printingStationService);
		BindingContext = printingDetailsViewModel;

    }
}