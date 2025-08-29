using MANDUU.ViewModels;

namespace MANDUU.Views.ShopPages;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel dashboardViewModel)
	{
		InitializeComponent();
		BindingContext = dashboardViewModel;
    }
}