using Acr.UserDialogs;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using HbrClient.Model.Request;
using System;
using System.Net.Http;
using static Android.Support.V7.Widget.RecyclerView;

namespace HbrClient.BookQuery
{
    class QueryBookViewHolder : ViewHolder
    {
        public TextView TitleTextView { get; set; }
        public TextView AuthorTextView { get; set; }
        public BookQueryAdapter Adapter { get; set; }
        public RecyclerView RecyclerView { get; set; }

        public QueryBookViewHolder(View view) : base(view)
        {
            TitleTextView = view.FindViewById<TextView>(Resource.Id.tv_title);
            AuthorTextView = view.FindViewById<TextView>(Resource.Id.tv_author);
            view.Click += OnClick;
        }

        private async void OnClick(object sender, EventArgs e)
        {
            var config = new ConfirmConfig
            {
                CancelText = "Mégse",
                OkText = "Igen",
                Title = "Könyv felvétele",
                Message = "Felveszi ezt a könyvet a személyet polcára?"
            };
            var result = await UserDialogs.Instance.ConfirmAsync(config);

            if (!result)
                return;

            using (var client = new HttpClient())
            {
                var dialog = UserDialogs.Instance.Loading("Loading");
                var view = (View)sender;
                var position = RecyclerView.GetChildAdapterPosition(view);
                var book = Adapter.QueryResult[position];

                var request = new AddBookToShelfRequest
                {
                    BookId = book.BookId,
                    Progress = 1,
                    #region tmp ki lesz veve
                    UserIdentifier = HbrApplication.UserIdentifier,
                    #endregion
                };
                await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/book/addbooktoshelf", request);

                Database.Instance.AddElement(book);

                Adapter.QueryResult.RemoveAt(position);
                Adapter.NotifyDataSetChanged();
                dialog.Dispose();
            }
        }
    }
}