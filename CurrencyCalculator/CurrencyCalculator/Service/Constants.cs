using CurrencyRateCalculator.Models;
using System;
using System.IO;

namespace CurrencyRateCalculator.Services
{
    /// <summary>
    /// Some Constants for the app
    /// </summary>
    public static class Constants
    {
        public static string RestUrl = "http://api.exchangeratesapi.io/";
        public static string Username = "Xamarin";
        public static string Password = "Pa$$w0rd";
        public static string ServiceId = "ThisServiceId";
        public static string Key = "1ad6899db376a093118d8acd420d2ab8";
        public static string baseCurrency = "EUR";
        public static string symbolCurrency = "USD";
        public static string latest = "v1/latest?";

        public const string DatabaseFilename = "TodoSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }


    }
}
