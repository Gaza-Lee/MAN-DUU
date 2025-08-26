using MANDUU.Services;

namespace MANDUU
{
    public partial class App : Application
    {
        private readonly INavigationService _navigationService;
        public App(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }

        protected override async void OnStart()
        {
            base.OnStart();
            var navigationService = Handler.MauiContext.Services.GetService<INavigationService>();
            await navigationService.InitializeAsync();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}