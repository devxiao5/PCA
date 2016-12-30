namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removepicture : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DailyReportPictures", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DailyReportPictures", "Date", c => c.DateTime(nullable: false));
        }
    }
}
