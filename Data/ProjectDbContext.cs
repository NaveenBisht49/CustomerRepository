using Microsoft.EntityFrameworkCore;
using Project_1.Models;

namespace Project_1.Data
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext>option):base(option)
        {
                
        }

        public DbSet<CustomerModel> CustomerMaster { get; set; }
        public DbSet<OrderModel> Ordertable { get; set; }
    }
}
