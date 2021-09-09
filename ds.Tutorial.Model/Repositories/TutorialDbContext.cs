using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ds.Tutorial.Model.Repositories
{
    public class TutorialDbContext : DbContext
    {
        public TutorialDbContext() { }

        public TutorialDbContext(DbContextOptions<TutorialDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<TaskModel> TaskList { get; set; }

    }
}
