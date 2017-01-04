namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moretimestampz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyReportPictures", "Timestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkItems", "Timestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkItems", "Timestamp");
            DropColumn("dbo.DailyReportPictures", "Timestamp");
        }
    }
}
