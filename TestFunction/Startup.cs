using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TestFunction;
using TestFunction.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TestFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;

            services.AddDbContext<TutorialDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings:tutorial_db", EnvironmentVariableTarget.Process))
            );

            services.AddTransient<IPlayerVoiceService, PlayerVoiceService>();
            services.AddTransient<IPlayerVoiceRepository, InMemoryPlayerVoiceRepository>();
        }
    }

    public class TutorialDbContext : DbContext
    {
        public TutorialDbContext() { }

        public TutorialDbContext(DbContextOptions<TutorialDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        public DbSet<UserEntity> Users { get; set; }
    }

    public class UserEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }

        public string? Hobby { get; set; }
    }
}
