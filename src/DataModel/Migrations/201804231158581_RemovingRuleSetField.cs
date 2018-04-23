namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingRuleSetField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rule", "IsAddition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rule", "IsAddition", c => c.Boolean(nullable: false));
        }
    }
}
