using System;
using System.Data.Entity;
using RetailPlanningAndForecasting.DomainModel;

namespace RetailPlanningAndForecasting.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }

        public DbSet<DepartmentsDirection> DepartmentsDirections { get; set; }

        public  AppDbContext() : base("AppDbConnection")
        {

        }
    }
}