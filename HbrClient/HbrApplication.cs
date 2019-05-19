using System;

using Android.App;
using Android.Runtime;
using Microsoft.Identity.Client;

namespace HbrClient
{
    [Application]
    public class HbrApplication : Application
    {
        #region tmp ki lesz véve
        public static string UserIdentifier { get; set; }
        #endregion


        private static string Tenant = "hbrTenant.onmicrosoft.com";
        private static string ClientId = "85e98718-50dd-409f-ab81-f19dbf52f92a";
        public static string PolicySignUpSignIn = "B2C_1_hbrSignupSignin";
        public static string PolicyEditProfile = "B2C_1_hbrProfileEditing";
        public static string PolicyResetPassword = "B2C_1_hbrPasswordReset";

        public static string[] ApiScopes = { "https://hbrTenant.onmicrosoft.com/api/hbr_read" };
        //public static string ApiEndpoint = "https://hbr.azurewebsites.net/api/Book/QueryBooks";
        public static string ApiEndpoint = "http://localhost:62171/api/Book/GetMissingBooks";

        private static string BaseAuthority = "https://hbrTenant.b2clogin.com/tfp/{tenant}/{policy}/oauth2/v2.0/authorize";
        public static string Authority = BaseAuthority.Replace("{tenant}", Tenant).Replace("{policy}", PolicySignUpSignIn);
        public static string AuthorityEditProfile = BaseAuthority.Replace("{tenant}", Tenant).Replace("{policy}", PolicyEditProfile);
        public static string AuthorityResetPassword = BaseAuthority.Replace("{tenant}", Tenant).Replace("{policy}", PolicyResetPassword);

        public static IPublicClientApplication PublicClientApp { get; private set; }
        public static UIParent ParentActivity { get; set; }

        public HbrApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            PublicClientApp = new PublicClientApplication(ClientId);
                //PublicClientApplicationBuilder.Create(ClientId)
                //.WithB2CAuthority(Authority)
                //.Build();

            //TokenCacheHelper.Bind(PublicClientApp.UserTokenCache);
        }

        public override void OnCreate()
        {
            base.OnCreate();

            //AuthenticationProvider.CreatePublicClientApp();
        }
    }
}