using DoYouBudget.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoYouBudget.API.Data.Context
{
    public class DoYouBudgetContext : DbContext
    {
        public DoYouBudgetContext(DbContextOptions<DoYouBudgetContext> options) : base(options)
        {

        }

        public DbSet<UsersModel> Users { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<CategoryTypeModel> CategoryType { get; set; }
        public DbSet<MonthlyLogModel> MonthlyLog { get; set; }
    }
}
