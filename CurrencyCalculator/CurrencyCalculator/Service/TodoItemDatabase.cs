using CurrencyRateCalculator.Models;
using CurrencyRateCalculator.Services;
using NuGet.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRateCalculator.Service
{
    /// <summary>
    /// Create local database with save, delete and retrive functions, using the storage in Mobile
    /// </summary>
    public class TodoItemDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<TodoItemDatabase> Instance = new AsyncLazy<TodoItemDatabase>(async () =>
        {
            var instance = new TodoItemDatabase();
            CreateTableResult result = await Database.CreateTableAsync<LocalRateInfo>();
            return instance;
        });

        public TodoItemDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }


        public Task<List<LocalRateInfo>> GetItemsAsync()
        {
            return Database.Table<LocalRateInfo>().ToListAsync();
        }

        public Task<List<LocalRateInfo>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<LocalRateInfo>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<LocalRateInfo> GetItemAsync()
        {
            return Database.Table<LocalRateInfo>().FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(LocalRateInfo item)
        {

                return Database.InsertAsync(item);
            
        }

        public Task<int> SaveItemAsync(LocalRateInfo item, Type type)
        {

            return Database.InsertAsync(item);

        }

        public Task<int> DeleteItemAsync(LocalRateInfo item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<int> DeleteAllItemAsync()
        {
            return Database.DeleteAllAsync<LocalRateInfo>();
        }

        public class AsyncLazy<T>
        {
            readonly Lazy<Task<T>> instance;

            public AsyncLazy(Func<T> factory)
            {
                instance = new Lazy<Task<T>>(() => Task.Run(factory));
            }

            public AsyncLazy(Func<Task<T>> factory)
            {
                instance = new Lazy<Task<T>>(() => Task.Run(factory));
            }

            public TaskAwaiter<T> GetAwaiter()
            {
                return instance.Value.GetAwaiter();
            }
        }

        //...
    }
}
