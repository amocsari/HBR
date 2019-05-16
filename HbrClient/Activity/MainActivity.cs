using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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
        private LibraryAdapter mAdapter;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            UserDialogs.Init(this);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            await GetBooksFromServer();

            var mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_bookList);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.RecyclerView = mRecyclerView;
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton_addBook);
            fab.Click += FabOnClick;
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            StartActivityForResult(new Intent(this, typeof(BookDetailActivity)), BookDetailActivity.RequestCode);
        }

        private async Task GetBooksFromServer()
        {
            using (var client = new HttpClient())
            {
                var dialog = UserDialogs.Instance.Loading("Loading");
                try
                {
                    var response = await client.GetAsync("https://hbr.azurewebsites.net/api/book/getmybooks");

                    if (response.IsSuccessStatusCode)
                    {
                        var bookList = await response.Content.ReadAsAsync<List<ClientBookDto>>();

                        mAdapter = new LibraryAdapter(bookList, this);
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
            }
        }
    }
}

