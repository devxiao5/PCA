namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timestampdailyreport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyReports", "Timestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyReports", "Timestamp");
        }
    }
}
