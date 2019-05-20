using HbrClient.Model.Dto;
using SQLite;
using System.Collections.Generic;

namespace HbrClient
{
    public class Database
    {
        private static string dbPath = "/data/data/HbrClient.HbrClient/";

        private static string dbName = "Hbr.db";

        private static Database __instance;
        public static Database Instance
        {
            get
            {
                if(__instance == null)
                {
                    __instance = new Database();
                }

                return __instance;
            }
        }

        public Database()
        {
            CreateDatabase();
        }

        public bool CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(dbPath, dbName)))
                {
                    var a = connection.CreateTable<ClientBookDto>();
                    var b = connection.CreateTable<ClientBookmarkDto>();
                    var c = connection.CreateTable<GenreDto>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public bool AddElements<T>(IEnumerable<T> newItems)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(dbPath, dbName)))
                {
                    connection.InsertAll(newItems);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public bool AddElement<T>(T newItem)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(dbPath, dbName)))
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

        public List<T> SelectTable<T>() where T : new()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(dbPath, dbName)))
                {
                    return connection.Table<T>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public List<GenreDto> GetGenres()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(dbPath, dbName)))
                {
                    return connection.Table<GenreDto>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public bool UpdateTable<T>(T item) where T : new()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(dbPath, dbName)))
                {
                    if (item is ClientBookDto book)
                    {
                        connection.Query<T>("UPDATE BookDto set Isbn=?, Title=?, Author=?, PageNumber=?, GenreId=?, LastUpdated=? Where BookId=?", book.Isbn, book.Title, book.Author, book.PageNumber, book.GenreId, book.LastUpdated, book.BookId);
                    }
                    else if (item is ClientBookmarkDto bookmark)
                    {
                        connection.Query<T>("UPDATE BookmarkDto set BookId=?, PageNumber=?, LastUpdated=? Where BookmarkId=?", bookmark.BookId, bookmark.PageNumber, bookmark.LastUpdated, bookmark.BookmarkId);
                    }
                    else if (item is GenreDto genre)
                    {
                        connection.Query<T>("UPDATE GenreDto set GenreName=? Where GenreId=?", genre.GenreName, genre.GenreId);
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

        public bool RemoveTable<T>(T entity)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(dbPath, dbName)))
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