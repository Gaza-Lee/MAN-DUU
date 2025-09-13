using MANDUU.Views;
using MANDUU.Views.AuthenticationPages;
using MANDUU.Views.MainPages;
using MANDUU.Views.MainPages.SubPages;
using MANDUU.Views.MainPages.SubPages.CheckoutAndPayment;
using MANDUU.Views.MainPages.SubPages.GetVerified;
using MANDUU.Views.ShopPages;

namespace MANDUU
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("createaccountorsigninpage", typeof(CreateAccountOrSignInPage));
            Routing.RegisterRoute("landingpage", typeof(LandingPage));
            Routing.RegisterRoute("signinpage", typeof(SignInPage));
            Routing.RegisterRoute("createaccountpage", typeof(CreateAccountPage));
            Routing.RegisterRoute("homepage", typeof(HomePage));
            Routing.RegisterRoute("verificationpage", typeof(VerificationPage));
            Routing.RegisterRoute("resetpasswordpage", typeof(ResetPasswordPage));
            Routing.RegisterRoute("resetpswverificationpage", typeof(ResetPswVerificationPage));
            Routing.RegisterRoute("newpasswordpage", typeof(NewPasswordPage));
            Routing.RegisterRoute("productdetailpage", typeof(ProductDetailsPage));
            Routing.RegisterRoute("categorypage", typeof(CategoryPage));
            Routing.RegisterRoute("shopprofilepage", typeof(ShopProfilePage));
            Routing.RegisterRoute("printingdetailspage", typeof(PrintingDetailsPage));
            Routing.RegisterRoute("userprofilepage", typeof(UserProfilePage));
            Routing.RegisterRoute("myshoppage", typeof(MyShopPage));
            Routing.RegisterRoute("createshoppage", typeof(CreateShopPage));
            Routing.RegisterRoute("dashboardpage", typeof(DashboardPage));
            Routing.RegisterRoute("cartpage", typeof(CartPage));
            Routing.RegisterRoute("settingspage", typeof(SettingsPage));
            Routing.RegisterRoute("faqpage", typeof(FAQPage));
            Routing.RegisterRoute("favoritespage", typeof(FavoritesPage));
            Routing.RegisterRoute("checkoutpage", typeof(CheckoutPage));
            Routing.RegisterRoute("paymentpage", typeof(PaymentPage));
            Routing.RegisterRoute("getverifiedpage", typeof(GetVerifiedPage));
        }
    }
}
