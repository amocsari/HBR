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
using HbrClient.Auth;
using Microsoft.Identity.Client;

namespace HbrClient
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Button button = null;

            button.Click += OnSignInButtonClick;
        }

        private void OnSignInButtonClick(object sender, EventArgs e)
        {
            AuthenticationResult authResult = null;
            var app = AuthenticationProvider.PublicClientApp;
        }
    }
}