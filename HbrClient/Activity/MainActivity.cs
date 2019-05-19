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
using HbrClient.Model.Response;
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

            SetContentView(Resource.Layout.activity_main);

            //HbrApplication.ParentActivity = new UIParent(this);
            _database = Database.Instance;

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_bookList);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.RecyclerView = mRecyclerView;
            mAdapter.Context = this;
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));

            //TODO: még nincs token cache
            //await SynchronizeWithServer();

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
                    Logout();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private async Task Login()
        {
            #region tmp ki lesz veve
            var loginConfig = new LoginConfig
            {
                CancelText = "Mégse",
                OkText = "Bejelentkezés",
                Title = "Bejelentkezés",
                LoginPlaceholder = "Felhasználónév",
                PasswordPlaceholder = "Jelszó",
                Message = "Adja meg a bejelentkezési adatait"
            };
            var loginResult = await UserDialogs.Instance.LoginAsync(loginConfig);

            using (var client = new HttpClient())
            using (var dialog = UserDialogs.Instance.Loading("Logging in!"))
            {
                var request = new LoginRequest
                {
                    Password = loginResult.Password,
                    UserName = loginResult.LoginText
                };

                var result = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/account/loginuser", request);
                if (!result.IsSuccessStatusCode)
                {
                    dialog.Dispose();
                    await UserDialogs.Instance.AlertAsync("Hibás felhasználónév, vagy jelszó!", "Sikertelen bejelentkezés!");
                    return;
                }

                HbrApplication.UserIdentifier = await result.Content.ReadAsStringAsync();
            }
            await SynchronizeWithServer();
            var localBooks = _database.SelectTable<BookDto>();
            mAdapter.AddBooks(localBooks);
            #endregion

            //var app = HbrApplication.PublicClientApp;
            //var accounts = await app.GetAccountsAsync();
            //try
            //{
            //    var authresult = await app.AcquireTokenAsync(HbrApplication.ApiScopes, HbrApplication.ParentActivity);
            //    //AuthenticationResult ar = await app.AcquireTokenInteractive(HbrApplication.ApiScopes)
            //    //                                        //.WithAccount(GetAccountByPolicy(accounts, HbrApplication.PolicySignUpSignIn))
            //    //                                        //.WithParentActivityOrWindow(HbrApplication.ParentActivity)
            //    //                                        .ExecuteAsync();
            //}
            //catch (Exception e)
            //{

            //}
        }

        private async Task Logout()
        {
            #region tmp ki lesz veve
            using (var dialog = UserDialogs.Instance.Loading("Logging out!"))
            {
                HbrApplication.UserIdentifier = string.Empty;
                mAdapter.Clear();
            }
            #endregion
        }

        private async Task SynchronizeWithServer()
        {
            if (!CheckInternetConnection())
                return;

            if (string.IsNullOrEmpty(HbrApplication.UserIdentifier))
            {
                await UserDialogs.Instance.AlertAsync("Az alkalmazás használatához be kell jelentkezni!", "Kijelentkezve");
                return;
            }

            using (var dialog = UserDialogs.Instance.Loading("Synchronizing!")) ;
            {
                await SyncGenresWithServer();
                await SyncBookmarksWithServer();
                await SyncBooksWithServer();
            }
        }

        private async void QueryButtonOnClick(object sender, EventArgs e)
        {
            //var bookList = await GetBooksFromServerAsync(authorQuery: AuthorQuery, titleQuery: TitleQuery);
            //mAdapter.Library.Clear();
            //mAdapter.AddBooks(bookList);

            //AuthorQuery = string.Empty;
            //TitleQuery = string.Empty;
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
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BookDto));
                            StringReader sr = new StringReader(bookXml);
                            try
                            {
                                var book = (BookDto)xmlSerializer.Deserialize(sr);

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
                                                    AutoCompleteData = autoComplete,
                                                    #region tmp ki lesz veve
                                                    UserIdentifier = HbrApplication.UserIdentifier
                                                    #endregion
                                                };

                                                var response = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/book/addnewbook", request);

                                                if (response.IsSuccessStatusCode)
                                                {
                                                    var returnedBook = await response.Content.ReadAsAsync<BookDto>();

                                                    mAdapter.AddBooks(new List<BookDto> { returnedBook });
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mAdapter.AddBooks(new List<BookDto> { book });
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
                                                    AutoCompleteData = autoComplete,
                                                    #region tmp ki lesz veve
                                                    UserIdentifier = HbrApplication.UserIdentifier
                                                    #endregion
                                                };

                                                var response = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/book/updatebook", request);

                                                if (response.IsSuccessStatusCode)
                                                {
                                                    var returnedBook = await response.Content.ReadAsAsync<BookDto>();

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
                                        mAdapter.AddBooks(new List<BookDto> { book });
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
                            #region tmp ki lesz veve
                            UserIdentifier = HbrApplication.UserIdentifier
                            #endregion
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

            if ((int)resultCode == sign_in_response_code)
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

        private async Task SyncBooksWithServer()
        {
            using (var client = new HttpClient())
            {
                var localBookList = _database.SelectTable<BookDto>();
                var localBookIdList = localBookList.Select(lb => lb.BookId).ToList();

                var request = new GetMissingRequest
                {
                    IdList = localBookIdList,
                    #region tmp ki lesz veve
                    UserIdentifier = HbrApplication.UserIdentifier
                    #endregion
                };

                var result = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/Book/GetMissingBooks", request);
                if (!result.IsSuccessStatusCode)
                    return;

                var response = await result.Content.ReadAsAsync<GetMissingResponse<BookDto>>();
                _database.AddElements(response.RemoteDtoList);

                var booksToSend = localBookList
                    .Where(lb => response.MissingIdList.Contains(lb.BookId))
                    .Select(lb => new AddOrEditBookRequest
                    {
                        BookId = lb.BookId,
                        Author = lb.Author,
                        AutoCompleteData = false,
                        GenreId = lb.GenreId,
                        Isbn = lb.Isbn,
                        PageNumber = lb.PageNumber,
                        Title = lb.Title,
                        #region tmp ki lesz veve
                        UserIdentifier = HbrApplication.UserIdentifier
                        #endregion
                    }).ToList();
            }
        }

        private async Task SyncBookmarksWithServer()
        {
            using (var client = new HttpClient())
            {
                var localBookmarkList = _database.SelectTable<BookmarkDto>();
                var localBookmarkIdList = localBookmarkList.Select(lbm => lbm.BookmarkId).ToList();

                var request = new GetMissingRequest
                {
                    IdList = localBookmarkIdList,
                    #region tmp ki lesz veve
                    UserIdentifier = HbrApplication.UserIdentifier
                    #endregion
                };

                var result = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/Bookmark/GetMissingBookmarks", request);
                if (!result.IsSuccessStatusCode)
                    return;

                var response = await result.Content.ReadAsAsync<GetMissingResponse<BookmarkDto>>();
                _database.AddElements(response.RemoteDtoList);

                var bookmarksToSend = localBookmarkList
                    .Where(lbm => response.MissingIdList.Contains(lbm.BookmarkId))
                    .Select(lbm => new AddBookmarkRequest
                    {
                        BookId = lbm.BookId,
                        BookmarkId = lbm.BookmarkId,
                        PageNumber = lbm.PageNumber,
                        #region tmp ki lesz veve
                        UserIdentifier = HbrApplication.UserIdentifier
                        #endregion
                    }).ToList();

                await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/Book/BulkUpdateBooks", bookmarksToSend);
            }
        }

        private async Task SyncGenresWithServer()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync("https://hbr.azurewebsites.net/api/Genre/GetAllGenres");
                if (result.IsSuccessStatusCode)
                    return;

                var genreList = await result.Content.ReadAsAsync<List<GenreDto>>();
                var localGenreList = _database.GetGenres();
                var newGenres = genreList.Where(g => !localGenreList.Any(lg => lg.GenreId == g.GenreId)).ToList();
                _database.AddElements(newGenres);
            }
        }
    }
}

