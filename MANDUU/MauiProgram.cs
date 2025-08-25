using CommunityToolkit.Maui;
using MANDUU.Services;
using MANDUU.ViewModels;
using MANDUU.Views;
using MANDUU.Views.AuthenticationPages;
using MANDUU.Views.MainPages;
using MANDUU.Views.MainPages.SubPages;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Sharpnado.Tabs;
using MANDUU.Views.ShopPages;

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
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<ShopCategoryService>();
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<ProductCategoryService>();
            builder.Services.AddSingleton<ShopService>();
            builder.Services.AddSingleton<PrintingStationService>();
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
            builder.Services.AddTransient <ProductDetailsPage>();
            builder.Services.AddTransient<CategoryPage>();
            builder.Services.AddTransient<ShopProfilePage>();
            builder.Services.AddTransient<MyShopPage>();

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
            builder.Services.AddTransient<ProductDetailViewModel>();
            builder.Services.AddTransient<CategoryViewModel>();
            builder.Services.AddTransient<ShopCategoryViewModel>();
            builder.Services.AddTransient<ShopProfileViewModel>();
            builder.Services.AddTransient<EPrintingViewModel>();
            builder.Services.AddTransient<PrintingDetailsViewModel>();

            return builder;
        }
    }
}
