using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Models;


namespace WebApi.Data
{
    public class TodoDb :DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }

        public DbSet<Model> model => Set<Model>();
        public DbSet<Job> job => Set<Job>();

        public DbSet<Expense> expense => Set<Expense>();

    }
}
