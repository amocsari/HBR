using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Identity.Client;

namespace HbrClient.Auth
{
    public static class AuthenticationProvider
    {
        private static string Tenant = "hbrTenant.onmicrosoft.com";
        private static string ClientId = "85e98718-50dd-409f-ab81-f19dbf52f92a";
        public static string PolicySignUpSignIn = "B2C_1_hbrSignupSignin";
        public static string PolicyEditProfile = "B2C_1_hbrProfileEditing";
        public static string PolicyResetPassword = "B2C_1_hbrPasswordReset";

        public static string[] ApiScopes = { "https://hbrTenant.onmicrosoft.com/api/hbr_read" };
        public static string ApiEndpoint = "https://hbr.azurewebsites.net/api";

        private static string BaseAuthority = "https://hbrTenant.b2clogin.com/tfp/{tenant}/{policy}/oauth2/v2.0/authorize";
        public static string Authority = BaseAuthority.Replace("{tenant}", Tenant).Replace("{policy}", PolicySignUpSignIn);
        public static string AuthorityEditProfile = BaseAuthority.Replace("{tenant}", Tenant).Replace("{policy}", PolicyEditProfile);
        public static string AuthorityResetPassword = BaseAuthority.Replace("{tenant}", Tenant).Replace("{policy}", PolicyResetPassword);

        public static IPublicClientApplication PublicClientApp { get; set; }

        public static void CreatePublicClientApp()
        {
            PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithB2CAuthority(Authority)
                .Build();
        }
    }
}