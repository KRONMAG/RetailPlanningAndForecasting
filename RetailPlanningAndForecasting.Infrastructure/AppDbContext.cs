using System.Data.Entity;
using RetailPlanningAndForecasting.DomainModel;

namespace RetailPlanningAndForecasting.Infrastructure
{
    /// <summary>
    /// Контекст базы данных приложения
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Регионы размещения отделений торговой сети
        /// </summary>
        public DbSet<Region> Regions { get; set; }

        /// <summary>
        /// Направления отделений торговой сети
        /// </summary>
        public DbSet<DepartmentsDirection> DepartmentsDirections { get; set; }

        /// <summary>
        /// Метки отделений торговой сети
        /// </summary>
        public DbSet<DepartmentsLabel> DepartmentsLabels { get; set; }

        /// <summary>
        /// Период планирования товарооборота
        /// </summary>
        public DbSet<PlanningPeriod> PlanningPeriods { get; set; }

        /// <summary>
        /// Создание экземпляра класса: инициализация подключения к базе данных
        /// </summary>
        public  AppDbContext() : base("AppDbConnection")
        {

        }
    }
}