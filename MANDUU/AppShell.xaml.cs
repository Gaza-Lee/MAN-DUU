using MANDUU.Views;
using MANDUU.Views.AuthenticationPages;

namespace MANDUU
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("CreateAccountOrSignInPage", typeof(CreateAccountOrSignInPage));
            Routing.RegisterRoute("LandingPage", typeof(LandingPage));
        }
    }
}
