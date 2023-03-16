using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApi.Models.Expense;
using WebApi.Models.Job;
using WebApi.Models.Model;

namespace WebApi.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Model> Models => Set<Model>();
        public DbSet<Job> Jobs => Set<Job>();

        public DbSet<Expense> Expenses => Set<Expense>();

    }
}
