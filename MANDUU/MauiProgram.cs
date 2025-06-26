using Microsoft.Extensions.Logging;
using MANDUU.Views;
using MANDUU.ViewModels;
using MANDUU.Views.AuthenticationPages;

namespace MANDUU
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("ADLaMDisplay-Regular.ttf", "ADLaMDisplay");
                    fonts.AddFont("Findlandica-Regular.ttf", "Finlandica");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.RegisterAppServices();
            builder.RegisterViews();
            builder.RegisterViewModels();

            return builder.Build();
        }

        //Register application services
        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
        {
            return builder;
        }

        public static MauiAppBuilder RegisterViews (this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LandingPage>();
            builder.Services.AddSingleton<CreateAccountOrSignInPage>();

            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LandingPageViewModel>();

            return builder;
        }
    }
}
