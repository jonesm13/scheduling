namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingJobs1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Job", "StationId");
            AddForeignKey("dbo.Job", "StationId", "dbo.Station", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Job", "StationId", "dbo.Station");
            DropIndex("dbo.Job", new[] { "StationId" });
        }
    }
}
