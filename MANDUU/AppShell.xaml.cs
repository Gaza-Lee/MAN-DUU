﻿using MANDUU.Views;
using MANDUU.Views.AuthenticationPages;
using MANDUU.Views.MainPages;

namespace MANDUU
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("CreateAccountOrSignInPage", typeof(CreateAccountOrSignInPage));
            Routing.RegisterRoute("LandingPage", typeof(LandingPage));
            Routing.RegisterRoute("SignInPage", typeof(SignInPage));
            Routing.RegisterRoute("CreateAccountPage", typeof(CreateAccountPage));
            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("VerificationPage", typeof(VerificationPage));
            Routing.RegisterRoute("ResetPasswordPage", typeof(ResetPasswordPage));
            Routing.RegisterRoute("ResetPswVerificationPage", typeof(ResetPswVerificationPage));
            Routing.RegisterRoute("NewPasswordPage", typeof(NewPasswordPage));
        }
    }
}
