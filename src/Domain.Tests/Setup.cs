namespace Domain.Tests
{
    using System.Data.Entity;
    using DataModel;
    using DataModel.Migrations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Setup : IntegrationTestBase
    {
        [AssemblyInitialize]
        public static void SetupTestRun(TestContext testContext)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchedulingDbContext, Configuration>());

            using (SchedulingDbContext db = SchedulingDbContext.Create())
            {
                db.Database.Initialize(true);
            }
        }
    }
}