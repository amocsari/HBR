
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using HbrClient.Bookmark;
using HbrClient.Model.Dto;
using HbrClient.Model.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Serialization;

namespace HbrClient
{
    [Activity(Label = "BookDetailActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class BookDetailActivity : Activity
    {
        public const int RequestCode = 1;

        ClientBookDto Dto { get; set; }
        BookmarkAdapter BookmarkAdapter { get; set; } = new BookmarkAdapter();
        TextView AuthorTextView { get; set; }
        TextView TitleTextView { get; set; }
        TextView IsbnTextView { get; set; }

        List<GenreDto> GenreList { get; set; }

        Database _database;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_book_data_edit);

            _database = Database.Instance;

            AuthorTextView = FindViewById<TextView>(Resource.Id.text_view_book_author);
            var authorEditImageButton = FindViewById<ImageButton>(Resource.Id.image_button_edit_author);
            TitleTextView = FindViewById<TextView>(Resource.Id.text_view_book_title);
            var titleEditImageButton = FindViewById<ImageButton>(Resource.Id.image_button_edit_title);
            IsbnTextView = FindViewById<TextView>(Resource.Id.text_view_book_isbn);
            var isbnEditImageButton = FindViewById<ImageButton>(Resource.Id.image_button_edit_isbn);
            var GenreSpinner = FindViewById<Spinner>(Resource.Id.spinner_book_genre);
            var bookmarkRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_bookmarkList);

            var saveButton = FindViewById<Button>(Resource.Id.button_book_save);
            var cancelButton = FindViewById<Button>(Resource.Id.button_book_cancel);
            var addBookmarkButton = FindViewById<Button>(Resource.Id.button_addBookmark);

            saveButton.Click += OnSaveButtonClick;
            cancelButton.Click += OnCancelButtonClick;
            addBookmarkButton.Click += AddBookMarkButtonClick;

            authorEditImageButton.Click += OnAuthorEditImageButtonClick;
            titleEditImageButton.Click += OnTitleEditImageButtonClick;
            isbnEditImageButton.Click += OnIsbnEditImageButtonClick;

            var dialog = UserDialogs.Instance.Loading("Loading");

            var extra = Intent.GetStringExtra("book");
            if (!string.IsNullOrEmpty(extra))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientBookDto));
                StringReader sr = new StringReader(extra);
                Dto = (ClientBookDto)xmlSerializer.Deserialize(sr);
                var bookmarkList = _database.SelectTable<ClientBookmarkDto>().Where(bm => bm.BookId == Dto.BookId).ToList();
                BookmarkAdapter.AddBookmark(bookmarkList);
            }
            else
            {
                Dto = new ClientBookDto();
            }

            GenreList = _database.GetGenres();

            var adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, GenreList.Select(g => g.GenreName).ToList());
            GenreSpinner.Adapter = adapter;
            GenreSpinner.ItemSelected += OnGenreSpinnerItemSelected;

            if (string.IsNullOrEmpty(Dto.GenreId))
            {
                Dto.Genre = GenreList.FirstOrDefault();
                Dto.GenreId = Dto.Genre.GenreId;
            }
            GenreSpinner.SetSelection(GenreList.IndexOf(Dto.Genre));

            BookmarkAdapter.RecyclerView = bookmarkRecyclerView;
            bookmarkRecyclerView.SetAdapter(BookmarkAdapter);
            bookmarkRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            BookmarkAdapter.Context = this;

            AuthorTextView.Text = Dto.Author;
            TitleTextView.Text = Dto.Title;
            IsbnTextView.Text = Dto.Isbn;

            dialog.Dispose();
        }

        private void OnGenreSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var genre = GenreList[e.Position];
            Dto.Genre = genre;
            Dto.GenreId = genre.GenreId;
        }

        private async void OnTitleEditImageButtonClick(object sender, EventArgs e)
        {
            var config = new PromptConfig
            {
                Title = "Cím szerkesztése",
                Message = "Adja meg a könyv címét!",
                OkText = "Mentés",
                CancelText = "Mégse",
                Text = Dto.Title
            };
            var result = await UserDialogs.Instance.PromptAsync(config);

            if (result.Ok)
            {
                TitleTextView.Text = result.Text;
                Dto.Title = result.Text;
            }
        }

        private async void OnIsbnEditImageButtonClick(object sender, EventArgs e)
        {
            var config = new PromptConfig
            {
                Title = "Isbn szerkesztése",
                Message = "Adja meg a Isbn azonosítóját!",
                OkText = "Mentés",
                CancelText = "Mégse",
                Text = Dto.Isbn
            };
            var result = await UserDialogs.Instance.PromptAsync(config);

            if (result.Ok)
            {
                IsbnTextView.Text = result.Text;
                Dto.Isbn = result.Text;
            }
        }

        private async void OnAuthorEditImageButtonClick(object sender, EventArgs e)
        {
            var config = new PromptConfig
            {
                Title = "Szerző szerkesztése",
                Message = "Adja meg a könyv szerzőjét!",
                OkText = "Mentés",
                CancelText = "Mégse",
                Text = Dto.Author
            };
            var result = await UserDialogs.Instance.PromptAsync(config);

            if (result.Ok)
            {
                AuthorTextView.Text = result.Text;
                Dto.Author = result.Text;
            }
        }

        private async void AddBookMarkButtonClick(object sender, EventArgs e)
        {
            var config = new PromptConfig
            {
                Title = "Könyvjelző hozzáadása",
                Message = "Adja meg a könyvjelző oldalszámát!",
                OkText = "Mentés",
                CancelText = "Mégse"
            };
            var result = await UserDialogs.Instance.PromptAsync(config);

            if (result.Ok)
            {
                try
                {
                    var pageNumber = int.Parse(result.Text);

                    var dialog = UserDialogs.Instance.Loading("Loading");

                    using (var client = new HttpClient())
                    {
                        var bookmark = new ClientBookmarkDto
                        {
                            BookId = Dto.BookId,
                            PageNumber = pageNumber,
                            BookmarkId = new Guid().ToString(),
                            LastUpdated = DateTime.Now,
                            #region tmp ki lesz veve
                            UserIdentifier = HbrApplication.UserIdentifier
                            #endregion
                        };

                        var request = new AddBookmarkRequest
                        {
                            BookmarkId = bookmark.BookmarkId,
                            BookId = bookmark.BookId,
                            PageNumber = bookmark.PageNumber,
                            #region tmp ki lesz veve
                            UserIdentifier = HbrApplication.UserIdentifier
                            #endregion
                        };

                        if (CheckInternetConnection())
                            await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/Bookmark/AddBookmark", request);

                        BookmarkAdapter.AddBookmark(new List<ClientBookmarkDto> { bookmark });
                        _database.AddElement(bookmark);
                    }
                    dialog.Dispose();
                }
                catch (Exception)
                {
                    await UserDialogs.Instance.AlertAsync(new AlertConfig
                    {
                        Title = "Hibás oldalszám!",
                        Message = "Az oldalszám csak szám lehet, ezért a könyvjelző nem lett hozzáadva!"
                    });
                }
            }
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            SetResult(Result.Canceled);
            Finish();
        }

        private async void OnSaveButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Dto.Title) && string.IsNullOrEmpty(Dto.Isbn))
            {
                var config = new AlertConfig
                {
                    Title = "Hiányzó cím vagy ISBN",
                    Message = "A könyv cím vagy ISBN megadása kötelező!",
                    OkText = "Ok",
                };
                await UserDialogs.Instance.AlertAsync(config);
                return;
            }

            var returnIntent = new Intent();

            if (!string.IsNullOrEmpty(Dto.Isbn))
            {
                var config = new ConfirmConfig
                {
                    Title = "ISBN!",
                    Message = "Megadott Isbn azonosítót, szeretné, hogy ez alapján legyenek a könyv adatai kitöltve?",
                    OkText = "Igen",
                    CancelText = "Next"
                };
                var result = await UserDialogs.Instance.ConfirmAsync(config);

                returnIntent.PutExtra("autoComplete", result);
            }

            SetResult(Result.Ok, returnIntent);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientBookDto));
            StringWriter sw = new StringWriter();
            xmlSerializer.Serialize(sw, Dto);
            returnIntent.PutExtra("book", sw.ToString());

            Finish();
        }

        private bool CheckInternetConnection()
        {
            ConnectivityManager cm = (ConnectivityManager)GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = cm.ActiveNetworkInfo;
            return (activeConnection != null) && activeConnection.IsConnected;
        }
    }
}