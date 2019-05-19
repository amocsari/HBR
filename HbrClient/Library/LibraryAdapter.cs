using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using HbrClient.Model.Dto;
using HbrClient.Model.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Serialization;
using static Android.Support.V7.Widget.RecyclerView;

namespace HbrClient.Library
{
    public class LibraryAdapter : Adapter
    {
        public List<ClientBookDto> Library { get; set; }
        public Context Context { get; set; }
        public RecyclerView RecyclerView { get; set; }
        Database _database;

        public LibraryAdapter(List<ClientBookDto> library, Context context)
        {
            Library = library;
            Context = context;
        }

        public LibraryAdapter()
        {
            Library = new List<ClientBookDto>();
            _database = Database.Instance;
        }

        public override int ItemCount => Library.Count;

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            var bookViewHolder = holder as BookViewHolder;
            bookViewHolder.AuthorTextView.Text = Library[position].Author;
            bookViewHolder.TitleTextView.Text = Library[position].Title;
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var libraryItemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_library_item, parent, false);

            BookViewHolder bookViewHolder = new BookViewHolder(libraryItemView);
            bookViewHolder.MenuButtonImageView.Click += OnClick;

            return bookViewHolder;
        }


        public void OnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;

            var position = RecyclerView.GetChildAdapterPosition((View)view.Parent.Parent.Parent);

            PopupMenu popup = new PopupMenu(view.Context, view);
            popup.Inflate(Resource.Menu.menu_library_item);
            popup.MenuItemClick += async (clickedItem, argument) =>
            {
                if (argument.Item.ItemId == Resource.Id.menuItem_libraryItem_delete)
                {
                    var config = new ConfirmConfig
                    {
                        Title = "Törlés megerősítése",
                        Message = "Biztosan törli ezt a könyvet?",
                        OkText = "Törlés",
                        CancelText = "Mégse"
                    };
                    var result = await UserDialogs.Instance.ConfirmAsync(config);

                    if (result)
                    {
                        var dialog = UserDialogs.Instance.Loading("Loading");

                        var book = Library[position];

                        var request = new DeleteBookByIdRequest
                        {
                            BookId = book.BookId,
                            #region tmp ki lesz veve
                            UserIdentifier = HbrApplication.UserIdentifier
                            #endregion
                        };

                        using (var client = new HttpClient())
                        {
                            var response = await client.PostAsJsonAsync($"https://hbr.azurewebsites.net/api/Book/DeleteBookById", request);
                        }

                        _database.RemoveTable(book);
                        Library.RemoveAt(position);
                        NotifyDataSetChanged();

                        dialog.Dispose();
                    }
                }
                else if (argument.Item.ItemId == Resource.Id.menuItem_libraryItem_details)
                {
                    var activity = new Intent(Context, typeof(BookDetailActivity));
                    var dto = Library[position];
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientBookDto));
                    StringWriter sw = new StringWriter();
                    xmlSerializer.Serialize(sw, dto);
                    activity.PutExtra("book", sw.ToString());
                    ((Activity)Context).StartActivityForResult(activity, BookDetailActivity.RequestCode);
                }
                else if (argument.Item.ItemId == Resource.Id.menuItem_libraryItem_updateProgress)
                {
                    var config = new PromptConfig
                    {
                        Title = "Aktiális oldalszám módosítása!",
                        Message = "Adja meg az aktuális oldalszámot",
                        Text = Library[position].PageNumber.ToString(),
                        OkText = "Mentés",
                        CancelText = "Mégse"
                    };
                    var result = await UserDialogs.Instance.PromptAsync(config);

                    if (result.Ok)
                    {
                        var request = new UpdateBookProgressRequest
                        {
                            BookId = Library[position].BookId,
                            #region tmp ki lesz veve
                            UserIdentifier = HbrApplication.UserIdentifier
                            #endregion
                        };
                        try
                        {
                            request.NewProgress = int.Parse(result.Text);
                        }
                        catch (Exception)
                        {
                            await UserDialogs.Instance.AlertAsync(new AlertConfig
                            {
                                Title = "Hibás oldalszám!",
                                Message = "Az oldalszám csak szám lehet, ezért a művelet nem lett végrehajtva!"
                            });

                            return;
                        }

                        var dialog = UserDialogs.Instance.Loading("Loading");
                        using (var client = new HttpClient())
                        {
                            var response = await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/Book/UpdateBookProgress", request);
                            if (response.IsSuccessStatusCode)
                                Library[position].PageNumber = request.NewProgress;
                        }

                        dialog.Dispose();
                    }
                }
            };
            popup.Show();
        }

        public void AddBooks(List<ClientBookDto> dto)
        {
            if (Library == null)
                Library = new List<ClientBookDto>();

            Library.AddRange(dto);
            NotifyDataSetChanged();
        }

        internal void UpdateBook(ClientBookDto returnedBook)
        {
            var oldBook = Library.FirstOrDefault(b => b.BookId == returnedBook.BookId);
            oldBook.Author = returnedBook.Author;
            oldBook.Title = returnedBook.Title;
            oldBook.Isbn = returnedBook.Isbn;
            oldBook.GenreId = returnedBook.GenreId;
            oldBook.Genre = returnedBook.Genre;
            oldBook.Bookmarks = returnedBook.Bookmarks;
            oldBook.PageNumber = returnedBook.PageNumber;
            NotifyDataSetChanged();
        }
    }
}