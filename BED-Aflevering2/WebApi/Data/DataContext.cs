using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Models;


namespace WebApi.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Model> model => Set<Model>();
        public DbSet<Job> job => Set<Job>();

        public DbSet<Expense> expense => Set<Expense>();

    }
}
