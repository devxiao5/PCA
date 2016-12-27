namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1227test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractGeneralConditions",
                c => new
                    {
                        ContractGeneralConditionId = c.Int(nullable: false, identity: true),
                        ContractId = c.Int(nullable: false),
                        ScopeOfWork = c.String(),
                        CommencementDate = c.DateTime(nullable: false),
                        WorkingDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContractGeneralConditionId)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractGeneralConditions", "ContractId", "dbo.Contracts");
            DropIndex("dbo.ContractGeneralConditions", new[] { "ContractId" });
            DropTable("dbo.ContractGeneralConditions");
        }
    }
}
