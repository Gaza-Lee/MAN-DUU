using CommunityToolkit.Maui;
using MANDUU.Services;
using MANDUU.ViewModels;
using MANDUU.Views;
using MANDUU.Views.AuthenticationPages;
using MANDUU.Views.MainPages;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Sharpnado.Tabs;

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
                .UseSharpnadoTabs(loggerEnable:false)
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("ADLaMDisplay-Regular.ttf", "ADLaMDisplay");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcon");
                    fonts.AddFont("Finlandica-Regular.ttf", "Finlandica");
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
            builder.Services.AddSingleton<CategoryService>();
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
            builder.Services.AddTransient<NewPasswordPage>();
            builder.Services.AddTransient<ResetPswVerificationPage>();
            builder.Services.AddTransient<HomePage>();

            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<LandingPageViewModel>();
            builder.Services.AddSingleton<CreateAccountOrSignInPageViewModel>();
            builder.Services.AddTransient<SignInPageViewModel>();
            builder.Services.AddTransient<CreateAccountPageViewModel>();
            builder.Services.AddTransient<VerificationPageViewModel>();
            builder.Services.AddTransient<ResetPasswordPageViewModel>();
            builder.Services.AddTransient<ResetPswVerificationViewModel>();
            builder.Services.AddTransient<NewPasswordPageViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();

            return builder;
        }
    }
}
