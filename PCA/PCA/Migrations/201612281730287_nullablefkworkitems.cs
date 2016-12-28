namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullablefkworkitems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkItems", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.WorkItems", "DailyReportId", "dbo.DailyReports");
            DropIndex("dbo.WorkItems", new[] { "DailyReportId" });
            DropIndex("dbo.WorkItems", new[] { "ContractorId" });
            AlterColumn("dbo.WorkItems", "DailyReportId", c => c.Int());
            AlterColumn("dbo.WorkItems", "ContractorId", c => c.Int());
            CreateIndex("dbo.WorkItems", "DailyReportId");
            CreateIndex("dbo.WorkItems", "ContractorId");
            AddForeignKey("dbo.WorkItems", "ContractorId", "dbo.Contractors", "ContractorId");
            AddForeignKey("dbo.WorkItems", "DailyReportId", "dbo.DailyReports", "DailyReportId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkItems", "DailyReportId", "dbo.DailyReports");
            DropForeignKey("dbo.WorkItems", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.WorkItems", new[] { "ContractorId" });
            DropIndex("dbo.WorkItems", new[] { "DailyReportId" });
            AlterColumn("dbo.WorkItems", "ContractorId", c => c.Int(nullable: false));
            AlterColumn("dbo.WorkItems", "DailyReportId", c => c.Int(nullable: false));
            CreateIndex("dbo.WorkItems", "ContractorId");
            CreateIndex("dbo.WorkItems", "DailyReportId");
            AddForeignKey("dbo.WorkItems", "DailyReportId", "dbo.DailyReports", "DailyReportId", cascadeDelete: true);
            AddForeignKey("dbo.WorkItems", "ContractorId", "dbo.Contractors", "ContractorId", cascadeDelete: true);
        }
    }
}
