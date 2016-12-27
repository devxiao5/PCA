namespace PCA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contractrequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contracts", "ContractorSigAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Contracts", "OwnerSigAccountId", "dbo.Accounts");
            DropIndex("dbo.Contracts", new[] { "OwnerSigAccountId" });
            DropIndex("dbo.Contracts", new[] { "ContractorSigAccountId" });
            AlterColumn("dbo.Contracts", "OwnerSigAccountId", c => c.Int());
            AlterColumn("dbo.Contracts", "ContractorSigAccountId", c => c.Int());
            AlterColumn("dbo.Contracts", "OwnerSignDate", c => c.DateTime());
            AlterColumn("dbo.Contracts", "ContractorSignDate", c => c.DateTime());
            CreateIndex("dbo.Contracts", "OwnerSigAccountId");
            CreateIndex("dbo.Contracts", "ContractorSigAccountId");
            AddForeignKey("dbo.Contracts", "ContractorSigAccountId", "dbo.Accounts", "AccountId");
            AddForeignKey("dbo.Contracts", "OwnerSigAccountId", "dbo.Accounts", "AccountId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "OwnerSigAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Contracts", "ContractorSigAccountId", "dbo.Accounts");
            DropIndex("dbo.Contracts", new[] { "ContractorSigAccountId" });
            DropIndex("dbo.Contracts", new[] { "OwnerSigAccountId" });
            AlterColumn("dbo.Contracts", "ContractorSignDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Contracts", "OwnerSignDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Contracts", "ContractorSigAccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.Contracts", "OwnerSigAccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contracts", "ContractorSigAccountId");
            CreateIndex("dbo.Contracts", "OwnerSigAccountId");
            AddForeignKey("dbo.Contracts", "OwnerSigAccountId", "dbo.Accounts", "AccountId", cascadeDelete: true);
            AddForeignKey("dbo.Contracts", "ContractorSigAccountId", "dbo.Accounts", "AccountId", cascadeDelete: true);
        }
    }
}
