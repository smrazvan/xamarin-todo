using routedapp.Models;
using routedapp.Models.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace routedapp.Services
{
    public class TodosRepository
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null) return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<TodoModel>();
        }

        public static async Task Add(TodoModel todo)
        {
            await Init();
            await db.InsertAsync(todo);
        }
        public static async Task Remove(TodoModel todo)
        {
            await Init();
            await db.DeleteAsync(todo);
        }

        public static async Task<IEnumerable<TodoModel>> GetAll()
        {
            await Init();
            return (await db.Table<TodoModel>().ToListAsync());
        }

        public static async Task<TodoModel> GetById(int id)
        {
            await Init();
            return (await db.Table<TodoModel>().Where(td => td.Id == id).FirstOrDefaultAsync());
        }

        public static async Task Edit(TodoModel todo)
        {
            await Init();
            var todoDb = await db.Table<TodoModel>().Where(td => td.Id == todo.Id).FirstOrDefaultAsync();
            todoDb.Title = todo.Title;
            todoDb.Description = todo.Description;
            todoDb.IsCompleted = todo.IsCompleted;
            await db.UpdateAsync(todo);
        }
    }
}
