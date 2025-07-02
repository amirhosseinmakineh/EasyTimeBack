using EasyTime.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyTime.InfraStracure.Context
{
    public class EasyTimeContext : DbContext
    {
        public EasyTimeContext(DbContextOptions<EasyTimeContext> context) : base(context)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<BusinesCity> BusinesCityes { get; set; }
        public DbSet<BusinesNeighberhood> BusinesNeighberhoodes { get; set; }
        public DbSet<BusinessOwnerDay> BusinesOwnerDays { get; set; }
        public DbSet<BusinessOwnerTime> BusinesOwnerTimes { get; set; }
        public DbSet<BusinesRegion> BusinesRegiones { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Reserve> Reserves { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
