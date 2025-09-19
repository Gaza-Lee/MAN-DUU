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
            var services = Current.Handler.MauiContext.Services;
            var userService = services.GetService<IUserService>() as UserService;
            var shopService = services.GetService<ShopService>();
            var navigationService = Handler.MauiContext.Services.GetService<INavigationService>();

            if (userService != null && shopService != null)
            {
                await userService.InitializeAsync(shopService);
            }            
            await navigationService.InitializeAsync();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}