using System;
using System.Collections.Generic;
using System.Net.Http;
using Acr.UserDialogs;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using HbrClient.Model.Dto;
using HbrClient.Model.Request;
using static Android.Support.V7.Widget.RecyclerView;

namespace HbrClient.BookQuery
{
    class BookQueryAdapter : Adapter
    {
        public List<ClientBookDto> QueryResult { get; set; }
        public Context Context { get; set; }
        public RecyclerView RecyclerView { get; set; }
        private Database _database;

        public BookQueryAdapter()
        {
            QueryResult = new List<ClientBookDto>();
            _database = Database.Instance;
        }
        public override int ItemCount => QueryResult.Count;

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            var queryBookViewHolder = holder as QueryBookViewHolder;
            queryBookViewHolder.AuthorTextView.Text = QueryResult[position].Author;
            queryBookViewHolder.TitleTextView.Text = QueryResult[position].Title;
            queryBookViewHolder.ItemView.Click += OnClick;
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var libraryItemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_library_item, parent, false);

            QueryBookViewHolder queryBookViewHolder = new QueryBookViewHolder(libraryItemView);

            return queryBookViewHolder;
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
                var book = QueryResult[position];

                var request = new AddBookToShelfRequest
                {
                    BookId = book.BookId,
                    Progress = 0,
                    #region tmp ki lesz veve
                    UserIdentifier = HbrApplication.UserIdentifier,
                    #endregion
                };
                await client.PostAsJsonAsync("https://hbr.azurewebsites.net/api/book/addbooktoshelf", request);

                _database.AddElement(book);

                QueryResult.RemoveAt(position);
                NotifyDataSetChanged();
                dialog.Dispose();
            }
        }

        public void AddBooks(List<ClientBookDto> dto)
        {
            if (QueryResult == null)
                QueryResult = new List<ClientBookDto>();

            QueryResult.AddRange(dto);
            NotifyDataSetChanged();
        }

        public void Clear()
        {
            QueryResult.Clear();
            NotifyDataSetChanged();
        }
    }
}