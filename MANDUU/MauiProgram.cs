using CommunityToolkit.Maui;
using MANDUU.ViewModels;
using MANDUU.Views;
using MANDUU.Views.AuthenticationPages;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace MANDUU
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("ADLaMDisplay-Regular.ttf", "ADLaMDisplay");
                    fonts.AddFont("Findlandica-Regular.ttf", "Finlandica");
                });

            // Customize Entry handler to remove underline on Android
            EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if ANDROID || WINDOWS
                handler.PlatformView.Background = null;
#endif
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
            builder.Services.AddTransient<SignInPage>();
            builder.Services.AddTransient<CreateAccountPage>();
            builder.Services.AddTransient<VerificationPage>();
            builder.Services.AddTransient<ResetPasswordPage>();

            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LandingPageViewModel>();
            builder.Services.AddSingleton<CreateAccountOrSignInPageViewModel>();
            builder.Services.AddTransient<SignInPageViewModel>();
            builder.Services.AddTransient<CreateAccountPageViewModel>();
            builder.Services.AddTransient<VerificationPageViewModel>();

            return builder;
        }
    }
}
