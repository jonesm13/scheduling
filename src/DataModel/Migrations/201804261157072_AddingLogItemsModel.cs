namespace DataModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddingLogItemsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StationId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Station", t => t.StationId, cascadeDelete: true)
                .Index(t => t.StationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogItem", "StationId", "dbo.Station");
            DropIndex("dbo.LogItem", new[] { "StationId" });
            DropTable("dbo.LogItem");
        }
    }
}
