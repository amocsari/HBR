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

        public bool UpdateTable<T>(T item) where T : IClientEntity, new()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(dbPath, dbName)))
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