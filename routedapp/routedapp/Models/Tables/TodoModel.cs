using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace routedapp.Models.Tables
{
    public class TodoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
