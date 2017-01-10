namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contracrbcomodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractBudgetChangeOrders",
                c => new
                    {
                        ContractBudgetChangeOrderId = c.Int(nullable: false, identity: true),
                        ContractId = c.Int(nullable: false),
                        BudgetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContractBudgetChangeOrderId)
                .ForeignKey("dbo.Budgets", t => t.BudgetId, cascadeDelete: true)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: false)
                .Index(t => t.ContractId)
                .Index(t => t.BudgetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractBudgetChangeOrders", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.ContractBudgetChangeOrders", "BudgetId", "dbo.Budgets");
            DropIndex("dbo.ContractBudgetChangeOrders", new[] { "BudgetId" });
            DropIndex("dbo.ContractBudgetChangeOrders", new[] { "ContractId" });
            DropTable("dbo.ContractBudgetChangeOrders");
        }
    }
}
