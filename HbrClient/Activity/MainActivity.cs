using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using HbrClient.Library;
using HbrClient.Model.Dto;
using HbrClient.Model.Request;

namespace HbrClient
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        private const int get_file_request_code = 1001;
        private LibraryAdapter mAdapter = new LibraryAdapter();
        string AuthorQuery;
        string TitleQuery;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            UserDialogs.Init(this);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_bookList);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.RecyclerView = mRecyclerView;
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));

            await GetBooksFromServer();

            var AuthorQueryTextView = FindViewById<Android.Widget.TextView>(Resource.Id.tv_author_query);
            var TitleQueryTextView = FindViewById<Android.Widget.TextView>(Resource.Id.tv_title_query);
            var QueryButton = FindViewById<Android.Widget.Button>(Resource.Id.button_queryBook);

            AuthorQueryTextView.Click += AuthorQueryTextViewOnClick;
            TitleQueryTextView.Click += TitleQueryTextViewOnClick;
            QueryButton.Click += QueryButtonOnClick;

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton_addBook);
            fab.Click += FabOnClick;
        }

        private async void QueryButtonOnClick(object sender, EventArgs e)
        {
            await GetBooksFromServer();
            AuthorQuery = string.Empty;
            TitleQuery = string.Empty;
        }

        private async void TitleQueryTextViewOnClick(object sender, EventArgs e)
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

        private async void AuthorQueryTextViewOnClick(object sender, EventArgs e)
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

        private async Task GetBooksFromServer()
        {
            using (var client = new HttpClient())
            {
                var dialog = UserDialogs.Instance.Loading("Loading");
                try
                {
                    var response = await client.GetAsync($"https://hbr.azurewebsites.net/api/book/querybooks?Title={TitleQuery}&Author={AuthorQuery}");

                    if (response.IsSuccessStatusCode)
                    {
                        var bookList = await response.Content.ReadAsAsync<List<ClientBookDto>>();

                        mAdapter.Library.Clear();
                        mAdapter.AddBook(bookList);
                    }
                }
                catch (Exception e)
                {
                }
                dialog.Dispose();
            }
        }

        protected override async void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case BookDetailActivity.RequestCode:

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

                                if (!book.BookId.HasValue)
                                {
                                    using (var client = new HttpClient())
                                    {
                                        var dialog = UserDialogs.Instance.Loading("Loading");
                                        try
                                        {
                                            var request = new AddNewBookRequest
                                            {
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

                                                mAdapter.AddBook(new List<ClientBookDto> { returnedBook });
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                        }
                                        dialog.Dispose();
                                    }
                                }
                                else
                                {
                                    using (var client = new HttpClient())
                                    {
                                        var dialog = UserDialogs.Instance.Loading("Loading");
                                        try
                                        {
                                            var request = new UpdateBookRequest
                                            {
                                                Author = book.Author,
                                                BookId = book.BookId.Value,
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
                                        dialog.Dispose();
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
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
                        var request = new AddNewBookRequest
                        {
                            Isbn = "9780316272247",
                            File = byteArray
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
    }
}

