namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRuleSetRelationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RuleSet",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rule",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RuleSetId = c.Guid(nullable: false),
                        Name = c.String(),
                        IsAddition = c.Boolean(nullable: false),
                        ClrType = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RuleSet", t => t.RuleSetId, cascadeDelete: true)
                .Index(t => t.RuleSetId);
            
            AddColumn("dbo.Station", "RuleSetId", c => c.Guid());
            AddColumn("dbo.Template", "RuleSetId", c => c.Guid());
            CreateIndex("dbo.Station", "RuleSetId");
            CreateIndex("dbo.Template", "RuleSetId");
            AddForeignKey("dbo.Station", "RuleSetId", "dbo.RuleSet", "Id");
            AddForeignKey("dbo.Template", "RuleSetId", "dbo.RuleSet", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Template", "RuleSetId", "dbo.RuleSet");
            DropForeignKey("dbo.Station", "RuleSetId", "dbo.RuleSet");
            DropForeignKey("dbo.Rule", "RuleSetId", "dbo.RuleSet");
            DropIndex("dbo.Template", new[] { "RuleSetId" });
            DropIndex("dbo.Rule", new[] { "RuleSetId" });
            DropIndex("dbo.Station", new[] { "RuleSetId" });
            DropColumn("dbo.Template", "RuleSetId");
            DropColumn("dbo.Station", "RuleSetId");
            DropTable("dbo.Rule");
            DropTable("dbo.RuleSet");
        }
    }
}
