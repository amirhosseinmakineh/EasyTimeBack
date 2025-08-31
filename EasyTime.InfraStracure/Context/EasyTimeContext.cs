using EasyTime.Model.Models;
using Microsoft.EntityFrameworkCore;
using BusinessDay = EasyTime.Model.Models.BusinessDay;

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
        public DbSet<UserBusinessOwner> UserBusinessOwners { get; set; }
        public DbSet<BusinessDay> BusinessDays { get; set; }
        public DbSet<BusinessTime> BusinessTimes { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<BusinessOwnerPlan> BusinessOwnerPlans { get; set; }
        public DbSet<PlansInformation> PlansInformation { get; set; }
        public DbSet<PlanTime> PlanTimes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BusinessServices> BusinessServices { get; set; }
        public DbSet<Achievements> Achievements { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
