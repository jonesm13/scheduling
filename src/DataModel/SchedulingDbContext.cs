namespace DataModel
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Entities;

    public class SchedulingDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<Guid>()
                .Where(x => x.Name == nameof(IEntity.Id))
                .Configure(
                    x =>
                    {
                        x.IsKey();
                        x.HasDatabaseGeneratedOption(
                            DatabaseGeneratedOption.None);
                    });
        }

        public DbSet<Station> Stations { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<Schedule> Schedule { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<LogItem> LogItems { get; set; }
    }
}