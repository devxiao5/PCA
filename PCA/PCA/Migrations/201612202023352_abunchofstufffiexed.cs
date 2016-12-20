namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abunchofstufffiexed : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.WorkItems", "DailyReportId");
            CreateIndex("dbo.WorkItems", "ContractorId");
            AddForeignKey("dbo.WorkItems", "ContractorId", "dbo.Contractors", "ContractorId", cascadeDelete: true);
            AddForeignKey("dbo.WorkItems", "DailyReportId", "dbo.DailyReports", "DailyReportId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItems", "DailyReportId", "dbo.DailyReports");
            DropForeignKey("dbo.WorkItems", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.WorkItems", new[] { "ContractorId" });
            DropIndex("dbo.WorkItems", new[] { "DailyReportId" });
        }
    }
}
