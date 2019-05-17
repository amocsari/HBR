using HbrClient.Model.Dto;
using SQLite;
using System.Collections.Generic;

namespace HbrClient
{
    public class Database
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Hbr.db")))
                {
                    connection.CreateTable<ClientBookDto>();
                    connection.CreateTable<ClientBookmarkDto>();
                    connection.CreateTable<GenreDto>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public bool AddElement<T>(T newItem) where T : IClientEntity
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Hbr.db")))
                {
                    connection.Insert(newItem);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public List<T> SelectTable<T>() where T : IClientEntity, new()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Hbr.db")))
                {
                    return connection.Table<T>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public bool UpdateTable<T>(T item) where T : IClientEntity, new()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Hbr.db")))
                {
                    if (item is ClientBookDto book)
                    {
                        connection.Query<T>("UPDATE ClientBookDto set BookId=?, Isbn=?, Title=?, Author=?, PageNumber=?, GenreId=? Where ClientId=?", book.BookId, book.Isbn, book.Title, book.Author, book.PageNumber, book.GenreId, book.ClientId);
                    }
                    else if (item is ClientBookmarkDto bookmark)
                    {
                        connection.Query<T>("UPDATE ClientBookmarkDto set BookmarkId=?, BookId=?, PageNumber=? Where ClientId=?", bookmark.BookmarkId, bookmark.BookId, bookmark.PageNumber, bookmark.ClientId);
                    }
                    else if (item is ClientGenreDto genre)
                    {
                        connection.Query<T>("UPDATE ClientGenreDto set GenreId=?, GenreName=? Where ClientId=?", genre.GenreId, genre.GenreName, genre.ClientId);
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public bool RemoveTable<T>(T entity) where T : IClientEntity
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Hbr.db")))
                {
                    connection.Delete(entity);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }
    }
}