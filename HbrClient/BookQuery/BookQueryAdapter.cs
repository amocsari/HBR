using System;
using System.Collections.Generic;
using System.Net.Http;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using HbrClient.Model.Dto;
using HbrClient.Model.Request;
using static Android.Support.V7.Widget.RecyclerView;

namespace HbrClient.BookQuery
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
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
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var libraryItemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_library_item, parent, false);

            QueryBookViewHolder queryBookViewHolder = new QueryBookViewHolder(libraryItemView);
            queryBookViewHolder.Adapter = this;
            queryBookViewHolder.RecyclerView = RecyclerView;
            return queryBookViewHolder;
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