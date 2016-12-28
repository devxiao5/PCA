namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dailyreportpicture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyReportPictures",
                c => new
                    {
                        DailyReportPictureId = c.Int(nullable: false, identity: true),
                        DailyReportId = c.Int(nullable: false),
                        Description = c.String(maxLength: 1000),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DailyReportPictureId)
                .ForeignKey("dbo.DailyReports", t => t.DailyReportId, cascadeDelete: true)
                .Index(t => t.DailyReportId);
            
            AddColumn("dbo.Files", "DailyReportPictureId", c => c.Int());
            CreateIndex("dbo.Files", "DailyReportPictureId");
            AddForeignKey("dbo.Files", "DailyReportPictureId", "dbo.DailyReportPictures", "DailyReportPictureId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "DailyReportPictureId", "dbo.DailyReportPictures");
            DropForeignKey("dbo.DailyReportPictures", "DailyReportId", "dbo.DailyReports");
            DropIndex("dbo.DailyReportPictures", new[] { "DailyReportId" });
            DropIndex("dbo.Files", new[] { "DailyReportPictureId" });
            DropColumn("dbo.Files", "DailyReportPictureId");
            DropTable("dbo.DailyReportPictures");
        }
    }
}
