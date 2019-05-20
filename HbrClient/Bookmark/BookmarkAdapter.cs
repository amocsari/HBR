using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using static Android.Support.V7.Widget.RecyclerView;
using HbrClient.Model.Dto;
using Acr.UserDialogs;
using System.Net.Http;
using Android.Support.V7.Widget;
using HbrClient.Model.Request;

namespace HbrClient.Bookmark
{
    public class BookmarkAdapter : Adapter
    {
        public List<ClientBookmarkDto> Bookmarks { get; set; }
        public Context Context { get; set; }
        public RecyclerView RecyclerView { get; set; }

        public override int ItemCount => Bookmarks.Count;

        public BookmarkAdapter()
        {
            Bookmarks = new List<ClientBookmarkDto>();
        }

        public BookmarkAdapter(List<ClientBookmarkDto> bookmarks)
        {
            Bookmarks = bookmarks;
        }

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            var bookmarkViewHolder = holder as BookmarkViewHolder;
            bookmarkViewHolder.PageTextView.Text = Bookmarks[position].PageNumber.ToString();
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var libraryItemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_bookmark_item, parent, false);

            BookmarkViewHolder bookmarkViewHolder = new BookmarkViewHolder(libraryItemView);
            bookmarkViewHolder.DeleteImageView.Click += OnDeleteImageViewClickedAsync;

            return bookmarkViewHolder;
        }

        private async void OnDeleteImageViewClickedAsync(object sender, EventArgs e)
        {
            View view = (View)sender;

            var position = RecyclerView.GetChildAdapterPosition((View)view.Parent.Parent.Parent);

            var config = new ConfirmConfig
            {
                Title = "Törlés megerősítése",
                Message = "Biztosan törli ezt a könyvjelzőt?",
                OkText = "Törlés",
                CancelText = "Mégse"
            };
            var result = await UserDialogs.Instance.ConfirmAsync(config);

            if (result)
            {
                var dialog = UserDialogs.Instance.Loading("Loading");
                var bookmark = Bookmarks[position];

                using (var client = new HttpClient())
                {
                    var request = new DeleteBookmarkRequest
                    {
                        BookmarkId = bookmark.BookmarkId,
                        #region tmp ki lesz veve
                        UserIdentifier = HbrApplication.UserIdentifier
                        #endregion
                    };
                    var response = await client.PostAsJsonAsync($"https://hbr.azurewebsites.net/api/Bookmark/DeleteBookMark", request);

                }

                Bookmarks.RemoveAt(position);
                NotifyDataSetChanged();
                Database.Instance.RemoveTable(bookmark);
                dialog.Dispose();
            }
        }

        public void AddBookmark(List<ClientBookmarkDto> dtoList)
        {
            Bookmarks.AddRange(dtoList);
            NotifyDataSetChanged();
        }
    }
}