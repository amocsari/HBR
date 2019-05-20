using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using HbrClient.BookQuery;
using HbrClient.Model.Dto;
using HbrClient.Model.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HbrClient
{
    [Activity(Label = "BookQueryActivity")]
    public class BookQueryActivity : AppCompatActivity
    {
        string AuthorQuery;
        string TitleQuery;
        BookQueryAdapter mAdapter = new BookQueryAdapter();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_book_query);

            var AuthorQueryTextView = FindViewById<Android.Widget.TextView>(Resource.Id.tv_author_query);
            var TitleQueryTextView = FindViewById<Android.Widget.TextView>(Resource.Id.tv_title_query);
            var QueryButton = FindViewById<Android.Widget.Button>(Resource.Id.button_queryBook);

            var mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_bookList);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.RecyclerView = mRecyclerView;
            mAdapter.Context = this;
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));

            AuthorQueryTextView.Click += AuthorQueryTextViewOnClick;
            TitleQueryTextView.Click += TitleQueryTextViewOnClick;
            QueryButton.Click += QueryButtonOnClick;
        }

        private async void QueryButtonOnClick(object sender, EventArgs e)
        {
            mAdapter.Clear();
            var bookList = await QueryBooksFromServerAsync();
            mAdapter.AddBooks(bookList);

            AuthorQuery = string.Empty;
            TitleQuery = string.Empty;
        }

        private async Task<List<ClientBookDto>> QueryBooksFromServerAsync()
        {
            using (var client = new HttpClient())
            using (var dialog = UserDialogs.Instance.Loading("Loading"))
            {
                var request = new QueryBooksRequest
                {
                    Author = AuthorQuery,
                    Title = TitleQuery,
                    #region tmp ki lesz veve
                    UserIdentifier = HbrApplication.UserIdentifier
                    #endregion
                };

                var result = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/book/querybooks", request);

                if (!result.IsSuccessStatusCode)
                {
                    var config = new AlertConfig
                    {
                        Message = "Nem sikerült kapcsolatot teremteni szerverrel.",
                        Title = "Sikertelen kapcsolódás!",
                    };
                    await UserDialogs.Instance.AlertAsync(config);
                    Finish();
                    return new List<ClientBookDto>();
                }

                return await result.Content.ReadAsAsync<List<ClientBookDto>>();
            }
        }

        private async void AuthorQueryTextViewOnClick(object sender, EventArgs e)
        {
            var config = new PromptConfig
            {
                Title = "Szerző",
                Text = AuthorQuery,
                OkText = "Rendben",
                CancelText = "Mégse"
            };
            var result = await UserDialogs.Instance.PromptAsync(config);

            if (result.Ok)
                AuthorQuery = result.Text;
        }

        private async void TitleQueryTextViewOnClick(object sender, EventArgs e)
        {
            var config = new PromptConfig
            {
                Title = "Cím",
                Text = TitleQuery,
                OkText = "Rendben",
                CancelText = "Mégse"
            };
            var result = await UserDialogs.Instance.PromptAsync(config);

            if (result.Ok)
                TitleQuery = result.Text;
        }

        private bool CheckInternetConnection()
        {
            ConnectivityManager cm = (ConnectivityManager)GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = cm.ActiveNetworkInfo;
            return (activeConnection != null) && activeConnection.IsConnected;
        }
    }
}