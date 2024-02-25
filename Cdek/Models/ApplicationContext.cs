using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cdek.Models
{
    /// <summary>
    /// Класс для соеденения с базой данных
    /// для создании миграции введите
    ///  dotnet ef migrations add Add_NAME_MIGRATIONS 
    /// для синхронизации с базой данных миграций
    ///  dotnet ef database update (visual studio: Update-Database)
    /// </summary>
    public class ApplicationContext : DbContext
    {       
        IConfiguration appConfig;

        public ApplicationContext(IConfiguration config)
        {
            appConfig = config;
        }
        
        public DbSet<Cargo> Cargos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=postgres;Username=postgres;Password=password_bd");
        }
    } 
}
