namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1192017dailyreportdelay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyReports", "IsDelay", c => c.Boolean(nullable: false));
            AddColumn("dbo.DailyReports", "DelayType", c => c.String(maxLength: 25));
            AddColumn("dbo.DailyReports", "DelayDescription", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyReports", "DelayDescription");
            DropColumn("dbo.DailyReports", "DelayType");
            DropColumn("dbo.DailyReports", "IsDelay");
        }
    }
}
