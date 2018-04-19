namespace DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    sealed class Configuration : DbMigrationsConfiguration<DataModel.SchedulingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchedulingDbContext context)
        {
        }
    }
}
