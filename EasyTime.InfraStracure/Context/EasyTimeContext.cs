using EasyTime.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyTime.InfraStracure.Context
{
    public class EasyTimeContext : DbContext
    {
        public EasyTimeContext(DbContextOptions<EasyTimeContext> context):base(context)
        {
            
        }
        public DbSet<User> Users { get; set; }

    }
}
