using System;

using Android.App;
using Android.Runtime;
using HbrClient.Auth;

namespace HbrClient
{
    [Application]
    public class HbrApplication : Application
    {
        public HbrApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            AuthenticationProvider.CreatePublicClientApp();
        }
    }
}