using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using HbrClient.Library;
using HbrClient.Model.Dto;
using HbrClient.Model.Request;
using Microsoft.Identity.Client;

namespace HbrClient
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        private const int get_file_request_code = 1001;
        private const int sign_in_response_code = 2001;
        private LibraryAdapter mAdapter = new LibraryAdapter();
        string AuthorQuery;
        string TitleQuery;
        Database _database;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            UserDialogs.Init(this);
            base.OnCreate(savedInstanceState);

            HbrApplication.ParentActivity = new UIParent(this);

            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_bookList);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.RecyclerView = mRecyclerView;
            mAdapter.Context = this;
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));

            await SynchLocalWithRemoteAsync();
            var bookList = await GetLocalOrRemoteBookListAsync();
            mAdapter.AddBooks(bookList);

            var AuthorQueryTextView = FindViewById<Android.Widget.TextView>(Resource.Id.tv_author_query);
            var TitleQueryTextView = FindViewById<Android.Widget.TextView>(Resource.Id.tv_title_query);
            var QueryButton = FindViewById<Android.Widget.Button>(Resource.Id.button_queryBook);

            AuthorQueryTextView.Click += AuthorQueryTextViewOnClick;
            TitleQueryTextView.Click += TitleQueryTextViewOnClick;
            QueryButton.Click += QueryButtonOnClick;

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton_addBook);
            fab.Click += FabOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_account, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem_account_login:
                    Login();
                    break;
                case Resource.Id.menuItem_account_logout:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private async Task Login()
        {
            var app = HbrApplication.PublicClientApp;
            var accounts = await app.GetAccountsAsync();
            try
            {
                var authresult = await app.AcquireTokenAsync(HbrApplication.ApiScopes, HbrApplication.ParentActivity);
                //AuthenticationResult ar = await app.AcquireTokenInteractive(HbrApplication.ApiScopes)
                //                                        //.WithAccount(GetAccountByPolicy(accounts, HbrApplication.PolicySignUpSignIn))
                //                                        //.WithParentActivityOrWindow(HbrApplication.ParentActivity)
                //                                        .ExecuteAsync();
            }
            catch(Exception e)
            {

            }
        }

        private async Task<List<ClientBookDto>> GetLocalOrRemoteBookListAsync()
        {
            if (CheckInternetConnection())
            {
                return await GetBooksFromServerAsync();
            }
            else
            {
                return _database.SelectTable<ClientBookDto>();
            }
        }

        private async void QueryButtonOnClick(object sender, EventArgs e)
        {
            var bookList = await GetBooksFromServerAsync(authorQuery: AuthorQuery, titleQuery: TitleQuery);
            mAdapter.Library.Clear();
            mAdapter.AddBooks(bookList);

            AuthorQuery = string.Empty;
            TitleQuery = string.Empty;
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

        private async void FabOnClick(object sender, EventArgs eventArgs)
        {
            //var intent = new Intent(Intent.ActionGetContent);
            //intent.AddCategory(Intent.CategoryOpenable);
            //intent.SetType("application/pdf");
            //StartActivityForResult(intent, get_file_request_code);

            StartActivityForResult(new Intent(this, typeof(BookDetailActivity)), BookDetailActivity.RequestCode);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
        }

        private async Task<List<ClientBookDto>> GetBooksFromServerAsync(string authorQuery = "", string titleQuery = "", bool showLoading = true)
        {
            if (!CheckInternetConnection())
                return new List<ClientBookDto>();

            using (var client = new HttpClient())
            {
                var dialog = UserDialogs.Instance.Loading(title: "Loading", show: showLoading);
                try
                {
                    AuthenticationResult authenticationResult;
                    var app = HbrApplication.PublicClientApp;
                    var accounts = await app.GetAccountsAsync();
                    authenticationResult = await app.AcquireTokenSilentAsync(HbrApplication.ApiScopes, GetAccountByPolicy(accounts, HbrApplication.PolicySignUpSignIn));

                    var response = await client.GetAsync($"https://hbr.azurewebsites.net/api/book/getmybooks");

                    if (response.IsSuccessStatusCode)
                    {
                        var bookList = await response.Content.ReadAsAsync<List<ClientBookDto>>();
                        dialog.Dispose();
                        return bookList;
                    }
                }
                catch (MsalUiRequiredException)
                {
                    await UserDialogs.Instance.AlertAsync("Az alakalmazás használatához be kell jelentkezni!");
                }
                catch (Exception e)
                {
                }
                dialog.Dispose();
                return new List<ClientBookDto>();
            }
        }

        protected override async void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case BookDetailActivity.RequestCode:
                    var dialog = UserDialogs.Instance.Loading("Loading");
                    if (resultCode == Result.Ok)
                    {
                        var bookXml = data.GetStringExtra("book");
                        var autoComplete = data.GetBooleanExtra("autoComplete", false);

                        if (!string.IsNullOrEmpty(bookXml))
                        {
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientBookDto));
                            StringReader sr = new StringReader(bookXml);
                            try
                            {
                                var book = (ClientBookDto)xmlSerializer.Deserialize(sr);

                                if (string.IsNullOrEmpty(book.BookId))
                                {
                                    if (CheckInternetConnection())
                                    {
                                        using (var client = new HttpClient())
                                        {
                                            try
                                            {
                                                var request = new AddOrEditBookRequest
                                                {
                                                    BookId = new Guid().ToString(),
                                                    Author = book.Author,
                                                    GenreId = book.GenreId,
                                                    Isbn = book.Isbn,
                                                    PageNumber = book.PageNumber,
                                                    Title = book.Title,
                                                    AutoCompleteData = autoComplete
                                                };

                                                var response = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/book/addnewbook", request);

                                                if (response.IsSuccessStatusCode)
                                                {
                                                    var returnedBook = await response.Content.ReadAsAsync<ClientBookDto>();

                                                    mAdapter.AddBooks(new List<ClientBookDto> { returnedBook });
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mAdapter.AddBooks(new List<ClientBookDto> { book });
                                        book.ModifiedOffline = true;
                                    }

                                    _database.AddElement(book);
                                }
                                else
                                {
                                    if (CheckInternetConnection())
                                    {
                                        using (var client = new HttpClient())
                                        {
                                            try
                                            {
                                                var request = new AddOrEditBookRequest
                                                {
                                                    Author = book.Author,
                                                    BookId = book.BookId,
                                                    GenreId = book.GenreId,
                                                    Isbn = book.Isbn,
                                                    PageNumber = book.PageNumber,
                                                    Title = book.Title,
                                                    AutoCompleteData = autoComplete
                                                };

                                                var response = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/book/updatebook", request);

                                                if (response.IsSuccessStatusCode)
                                                {
                                                    var returnedBook = await response.Content.ReadAsAsync<ClientBookDto>();

                                                    mAdapter.UpdateBook(returnedBook);
                                                }
                                            }
                                            catch (Exception e)
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        mAdapter.AddBooks(new List<ClientBookDto> { book });
                                        book.ModifiedOffline = true;
                                    }

                                    _database.AddElement(book);
                                }
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                    dialog.Dispose();
                    break;

                case get_file_request_code:
                    var uri = data.Data;
                    var stream = ContentResolver.OpenInputStream(uri);
                    byte[] byteArray;
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        byteArray = memoryStream.ToArray();
                    }
                    using (var client = new HttpClient())
                    using (var formData = new MultipartFormDataContent())
                    {
                        var request = new AddOrEditBookRequest
                        {
                            Isbn = "9780316272247",
                        };
                        var a = Encoding.ASCII.GetString(byteArray);
                        var result = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/Book/UploadBook", request);
                        var resultData = await result.Content.ReadAsByteArrayAsync();
                        OpenPfd(resultData);
                        //HttpContent content = new StreamContent(stream);
                        //content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "formFile", FileName = "UploadedFile" };
                        //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                        //formData.Add(content);
                        //var response = await client.PostAsync("https://hbr.azurewebsites.net/api/book/uploadbook", formData);
                        //var str = await response.Content.ReadAsStringAsync();
                    }
                    break;
            }

            if((int)resultCode == sign_in_response_code)
                AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }

        private void OpenPfd(byte[] bytes)
        {
            var externalPath = Android.OS.Environment.ExternalStorageDirectory.Path + "/tmp.pdf";
            File.WriteAllBytes(externalPath, bytes);

            Java.IO.File file = new Java.IO.File(externalPath);
            file.SetReadable(true);
            Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, "application/pdf");
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

            try
            {
                StartActivity(intent);
            }
            catch (Exception)
            {
                Android.Widget.Toast.MakeText(this, "No Application Available to View PDF", Android.Widget.ToastLength.Short).Show();
            }

        }

        private async Task SynchLocalWithRemoteAsync()
        {
            if (!CheckInternetConnection())
                return;

            var dialog = UserDialogs.Instance.Loading("Synchronizing!");
            try
            {
                using (var client = new HttpClient())
                {
                    var localBookList = _database.SelectTable<ClientBookDto>();
                    var localBookIdList = localBookList
                        .Select(lb => lb.BookId)
                        .ToList();

                    var request = new GetMissingRequest
                    {
                        IdList = localBookIdList
                    };

                    var result = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/Book/GetMissingBooks", request);
                    if (result.IsSuccessStatusCode)
                    {
                        var missingBooks = await result.Content.ReadAsAsync<List<ClientBookDto>>();
                        _database.AddElements(missingBooks);
                    }

                    var localModifiedBookList = localBookList
                        .Where(lb => lb.ModifiedOffline)
                        .ToList();

                    var bulkUpdateBookRequest = localModifiedBookList
                        .Select(lb => new AddOrEditBookRequest
                        {
                            BookId = lb.BookId,
                            Title = lb.Title,
                            PageNumber = lb.PageNumber,
                            Isbn = lb.Isbn,
                            Author = lb.Author,
                            GenreId = lb.GenreId
                        }).ToList();

                    result = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/Book/BulkUpdateBooks", bulkUpdateBookRequest);
                    if (result.IsSuccessStatusCode)
                    {
                        foreach (var modifiedBook in localModifiedBookList)
                        {
                            modifiedBook.ModifiedOffline = false;
                            _database.UpdateTable(modifiedBook);
                        }
                    }

                    result = await client.GetAsync("https://hbr.azurewebsites.net/api/Genre/GetAllGenres");
                    if (result.IsSuccessStatusCode)
                    {
                        var genreList = await result.Content.ReadAsAsync<List<GenreDto>>();

                        var localGenreList = _database.GetGenres();

                        var newGenres = genreList.Where(g => !localGenreList.Any(lg => lg.GenreId == g.GenreId)).ToList();

                        _database.AddElements(newGenres);
                    }
                }
            }
            catch (Exception e)
            {

            }
            dialog.Dispose();
        }

        private bool CheckInternetConnection()
        {
            ConnectivityManager cm = (ConnectivityManager)GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = cm.ActiveNetworkInfo;
            return (activeConnection != null) && activeConnection.IsConnected;
        }

        private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        {
            foreach (var account in accounts)
            {
                string accountIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
                if (accountIdentifier.EndsWith(policy.ToLower())) return account;
            }

            return null;
        }
    }
}

