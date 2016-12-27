namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contractorcontract : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "ContractorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contracts", "ContractorId");
            AddForeignKey("dbo.Contracts", "ContractorId", "dbo.Contractors", "ContractorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.Contracts", new[] { "ContractorId" });
            DropColumn("dbo.Contracts", "ContractorId");
        }
    }
}
