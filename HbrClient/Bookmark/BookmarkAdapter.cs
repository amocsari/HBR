﻿using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using static Android.Support.V7.Widget.RecyclerView;
using HbrClient.Model.Dto;
using Acr.UserDialogs;
using System.Net.Http;
using Android.Support.V7.Widget;

namespace HbrClient.Bookmark
{
    public class BookmarkAdapter : Adapter
    {
        public List<BookmarkDto> Bookmarks { get; set; }
        public Context Context { get; set; }
        public RecyclerView RecyclerView { get; set; }

        public override int ItemCount => Bookmarks.Count;

        public BookmarkAdapter()
        {
            Bookmarks = new List<BookmarkDto>();
        }

        public BookmarkAdapter(List<BookmarkDto> bookmarks)
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

                using (var client = new HttpClient())
                {
                    var bookmark = Bookmarks[position];
                    var response = await client.DeleteAsync($"https://hbr.azurewebsites.net/api/Bookmark/DeleteBookMark?bookmarkId={bookmark.BookmarkId}");

                    if (response.IsSuccessStatusCode)
                    {
                        Bookmarks.RemoveAt(position);
                        NotifyDataSetChanged();
                    }
                }

                dialog.Dispose();
            }
        }

        public void AddBookmark(List<BookmarkDto> dtoList)
        {
            Bookmarks.AddRange(dtoList);
            NotifyDataSetChanged();
        }
    }
}