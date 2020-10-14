using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EldaCPRFirstAidTraining.Data
{
    public class EldaCPRFirstAidTrainingContext : IdentityDbContext<IdentityUser>
    {
        public EldaCPRFirstAidTrainingContext(DbContextOptions<EldaCPRFirstAidTrainingContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Training> Trainings { get; set; }
        public DbSet <TrainingSchedule> TrainingSchedules { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
